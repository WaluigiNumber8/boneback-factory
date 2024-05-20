using System;
using RedRats.Core;
using Rogium.Systems.Input;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// Keeps track of all actions that have been executed in the game.
    /// </summary>
    public static class ActionHistorySystem
    {
        public static event Action OnUpdateUndoHistory;
        public static event Action OnUpdateRedoHistory;
        
        private static CurrentAssetDetector assetDetector => CurrentAssetDetector.Instance;
        private static readonly ObservableStack<IAction> undoHistory = new();
        private static readonly ObservableStack<IAction> redoHistory = new();

        private static IAction lastAction;
        private static GroupAction currentGroup;
        private static bool canCreateGroups = true;

        static ActionHistorySystem()
        {
            undoHistory.OnChange += () => OnUpdateUndoHistory?.Invoke();
            redoHistory.OnChange += () => OnUpdateRedoHistory?.Invoke();
            assetDetector.OnAssetChange += ClearHistory;

            InputSystem.GetInstance().UI.Click.OnPress += ForceBeginGrouping;
            InputSystem.GetInstance().UI.ClickAlternative.OnPress += ForceBeginGrouping;
            InputSystem.GetInstance().UI.Click.OnRelease += ForceEndGrouping;
            InputSystem.GetInstance().UI.ClickAlternative.OnRelease += ForceEndGrouping;
        }

        /// <summary>
        /// Adds an action to the history and executes it.
        /// </summary>
        /// <param name="action">The action to add & execute</param>
        /// <param name="blockGrouping">Excerpt this action from grouping with similar ones.</param>
        public static void AddAndExecute(IAction action, bool blockGrouping = false)
        {
            if (action == null) return;
            if (action.NothingChanged()) return;
            if (assetDetector.WasAssetChanged) undoHistory.Clear();
            
            action.Execute();
            DecideUndoStatusFor(action, blockGrouping);
            redoHistory.Clear();
            lastAction = action;
            
            assetDetector.MarkAsEdited();
        }

        public static void UndoLast()
        {
            //If there is a group open, add it to undo
            if (currentGroup != null) AddCurrentGroupToUndo();
            if (undoHistory.Count == 0) return;

            IAction newestAction = undoHistory.Pop();
            
            // //If action's construct is null, keep popping until a valid action is found
            // while (newestAction.AffectedConstruct == null)
            // {
            //     if (undoHistory.Count == 0) return;
            //     newestAction = undoHistory.Pop();
            // }
            
            redoHistory.Push(newestAction);
            newestAction.Undo();
        }

        public static void RedoLast()
        {
            if (redoHistory.Count == 0) return;

            IAction newestAction = redoHistory.Pop();
            undoHistory.Push(newestAction);
            newestAction.Execute();
        }

        private static void ClearHistory()
        {
            undoHistory.Clear();
            redoHistory.Clear();
            lastAction = null;
            currentGroup = null;
        }

        #region Group Processing

        /// <summary>
        /// Forces the system to allow grouping of actions.
        /// </summary>
        public static void ForceBeginGrouping() => canCreateGroups = true;
        
        /// <summary>
        /// Forces the system to add the current group to undo history.
        /// </summary>
        public static void ForceEndGrouping()
        {
            canCreateGroups = false;
            AddCurrentGroupToUndo();
        }
        
        private static void DecideUndoStatusFor(IAction action, bool blockGrouping = false)
        {
            // If in group mode
            if (!blockGrouping && canCreateGroups)
            {
                //Init group if it doesn't exist
                if (currentGroup == null)
                {
                    currentGroup = new GroupAction();
                    currentGroup.AddAction(action);
                    return;
                }
                
                //Add to group if action is on the same construct
                if (action.AffectedConstruct == lastAction?.AffectedConstruct)
                {
                    currentGroup.AddAction(action);
                    return;
                }
                
                //End grouping if action is different
                AddCurrentGroupToUndo();
            }

            undoHistory.Push(action);
        }

        private static void AddCurrentGroupToUndo()
        {
            if (currentGroup == null) return;
            if (!currentGroup.NothingChanged()) undoHistory.Push(currentGroup);
            currentGroup = null;
        }
        
        #endregion
        
        public static int UndoCount => undoHistory.Count;
        public static int RedoCount => redoHistory.Count;
    }
}
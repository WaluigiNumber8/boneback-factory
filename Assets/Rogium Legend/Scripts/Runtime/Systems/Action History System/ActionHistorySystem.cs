using System.Collections.Generic;
using Rogium.Systems.Input;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// Keeps track of all actions that have been executed in the game.
    /// </summary>
    public static class ActionHistorySystem
    {
        private static CurrentAssetDetector assetDetector => CurrentAssetDetector.Instance;
        private static readonly Stack<IAction> undoHistory = new();
        private static readonly Stack<IAction> redoHistory = new();

        private static IAction lastAction;
        private static GroupAction currentGroup;
        private static bool canCreateGroups = true;

        static ActionHistorySystem()
        {
            assetDetector.OnAssetChange += ClearHistory;

            InputSystem.GetInstance().UI.Click.OnPress += ForceBeginGrouping;
            InputSystem.GetInstance().UI.ClickAlternative.OnPress += ForceBeginGrouping;
            InputSystem.GetInstance().UI.Click.OnRelease += KillGroupingProcess;
            InputSystem.GetInstance().UI.ClickAlternative.OnRelease += KillGroupingProcess;
        }

        /// <summary>
        /// Adds an action to the history and executes it.
        /// </summary>
        /// <param name="action">The action to add & execute</param>
        /// <param name="blockGrouping">Excerpt this action from grouping with similar ones.</param>
        public static void AddAndExecute(IAction action, bool blockGrouping = false)
        {
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

        private static void KillGroupingProcess()
        {
            canCreateGroups = false;
            AddCurrentGroupToUndo();
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
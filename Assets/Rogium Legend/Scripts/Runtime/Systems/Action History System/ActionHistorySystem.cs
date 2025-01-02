using System;
using RedRats.Core;

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
        private static GroupActionBase currentGroup;
        private static bool canCreateGroups = true;
        private static bool ignoreConstructs = false;

        static ActionHistorySystem()
        {
            undoHistory.OnChange += () => OnUpdateUndoHistory?.Invoke();
            redoHistory.OnChange += () => OnUpdateRedoHistory?.Invoke();
            assetDetector.OnAssetChange += ClearHistory;
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

        public static void Undo()
        {
            //If there is a group open, add it to undo
            if (currentGroup != null) AddCurrentGroupToUndo();
            if (undoHistory.Count == 0) return;

            IAction newestAction = undoHistory.Pop();
            redoHistory.Push(newestAction);
            newestAction.Undo();
        }

        public static void Redo()
        {
            if (redoHistory.Count == 0) return;

            IAction newestAction = redoHistory.Pop();
            undoHistory.Push(newestAction);
            newestAction.Execute();
        }

        public static void ClearHistory()
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
        public static void ForceBeginGrouping()
        {
            if (canCreateGroups) ForceEndGrouping();
            canCreateGroups = true;
        }

        /// <summary>
        /// Forces the system to add the current group to undo history.
        /// </summary>
        public static void ForceEndGrouping()
        {
            canCreateGroups = false;
            AddCurrentGroupToUndo();
        }

        /// <summary>
        /// The next actions will gruped regardless of the construct.
        /// </summary>
        public static void ForceGroupAllActions()
        {
            ignoreConstructs = true;
            ForceBeginGrouping();
        }

        /// <summary>
        /// Ends the forced grouping of all actions.
        /// </summary>
        public static void ForceGroupAllActionsEnd()
        {
            ignoreConstructs = false;
            ForceEndGrouping();
        }
        
        private static void DecideUndoStatusFor(IAction action, bool blockGrouping = false)
        {
            // If in group mode
            if (!blockGrouping && canCreateGroups)
            {
                //Init group if it doesn't exist
                if (currentGroup == null)
                {
                    currentGroup = (ignoreConstructs) ? new MixedGroupAction() : new GroupAction();
                    currentGroup.AddAction(action);
                    return;
                }
                
                //Add to group if action is on the same construct
                if (ignoreConstructs || action.AffectedConstruct == lastAction?.AffectedConstruct)
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
        
        public static GroupActionBase CurrentGroup { get => currentGroup; }
    }
}
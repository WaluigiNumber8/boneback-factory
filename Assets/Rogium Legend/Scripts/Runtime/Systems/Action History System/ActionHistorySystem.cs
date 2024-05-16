using System.Collections.Generic;
using RedRats.Core;
using Rogium.Systems.Input;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// Keeps track of all actions that have been executed in the game.
    /// </summary>
    public class ActionHistorySystem : MonoSingleton<ActionHistorySystem>
    {
        private CurrentAssetDetector assetDetector => CurrentAssetDetector.Instance;
        private readonly Stack<IAction> undoHistory = new();
        private readonly Stack<IAction> redoHistory = new();

        private IAction lastAction;
        private GroupAction currentGroup;
        private bool canCreateGroups = true;

        private void Start()
        {
            assetDetector.OnAssetChange += ClearHistory;

            InputSystem.GetInstance().UI.Click.OnPress += StartGroupingProcess;
            InputSystem.GetInstance().UI.ClickAlternative.OnPress += StartGroupingProcess;
            InputSystem.GetInstance().UI.Click.OnRelease += KillGroupingProcess;
            InputSystem.GetInstance().UI.ClickAlternative.OnRelease += KillGroupingProcess;
        }

        public void AddAndExecute(IAction action)
        {
            if (action.NothingChanged()) return;
            if (assetDetector.WasAssetChanged) undoHistory.Clear();

            action.Execute();
            DecideUndoStatusFor(action);
            redoHistory.Clear();
            lastAction = action;
            
            assetDetector.MarkAsEdited();
        }

        public void UndoLast()
        {
            //If there is a group open, add it to undo
            if (currentGroup != null) AddCurrentGroupToUndo();

            if (undoHistory.Count == 0) return;

            IAction newestAction = undoHistory.Pop();
            redoHistory.Push(newestAction);
            newestAction.Undo();
        }

        public void RedoLast()
        {
            if (redoHistory.Count == 0) return;

            IAction newestAction = redoHistory.Pop();
            undoHistory.Push(newestAction);
            newestAction.Execute();
        }

        private void ClearHistory()
        {
            undoHistory.Clear();
            redoHistory.Clear();
            lastAction = null;
            currentGroup = null;
        }

        #region Group Processing

        private void DecideUndoStatusFor(IAction action)
        {
            // If in group mode
            if (canCreateGroups)
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
                undoHistory.Push(action);
                return;
            }
            
            undoHistory.Push(action);
        }

        private void StartGroupingProcess()
        {
            // Debug.Log("ON");
            canCreateGroups = true;
        }

        private void KillGroupingProcess()
        {
            // Debug.Log("OFF");
            canCreateGroups = false;
            AddCurrentGroupToUndo();
        }
        
        private void AddCurrentGroupToUndo()
        {
            if (currentGroup == null) return;
            if (!currentGroup.NothingChanged()) undoHistory.Push(currentGroup);
            currentGroup = null;
        }
        
        #endregion
        
        public int UndoCount => undoHistory.Count;
        public int RedoCount => redoHistory.Count;
    }
}
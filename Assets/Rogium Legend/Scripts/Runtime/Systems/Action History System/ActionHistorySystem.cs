using System.Collections.Generic;
using RedRats.Core;
using RedRats.Systems.Clocks;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// Keeps track of all actions that have been executed in the game.
    /// </summary>
    public class ActionHistorySystem : MonoSingleton<ActionHistorySystem>
    {
        private const float CanCreateGroupTime = 1.0f;
        private const float GroupLifeTime = 0.2f;
        
        private CurrentAssetDetector assetDetector => CurrentAssetDetector.Instance;
        private readonly Stack<IAction> undoHistory = new();
        private readonly Stack<IAction> redoHistory = new();

        private IAction lastAction;
        private GroupAction currentGroup;
        private CountdownTimer canCreateGroupTimer;
        private CountdownTimer groupLifeTimer;

        protected override void Awake()
        {
            base.Awake();
            canCreateGroupTimer = new CountdownTimer();
            groupLifeTimer = new CountdownTimer(AddCurrentGroupToUndo);
            assetDetector.OnAssetChange += ClearHistory;
        }

        private void Update()
        {
            canCreateGroupTimer.Tick();
            groupLifeTimer.Tick();
        }

        public void AddAndExecute(IAction action)
        {
            if (action.NothingChanged()) return;
            if (assetDetector.WasAssetChanged) undoHistory.Clear();

            action.Execute();
            AddToUndo(action);
            redoHistory.Clear();
            lastAction = action;
            
            assetDetector.MarkAsEdited();
        }

        public void UndoLastAction()
        {
            //If there is a group open, add it to undo
            if (currentGroup != null) AddCurrentGroupToUndo();

            if (undoHistory.Count == 0) return;

            IAction newestAction = undoHistory.Pop();
            redoHistory.Push(newestAction);
            newestAction.Undo();
        }

        public void RedoLastAction()
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

        private void AddToUndo(IAction action)
        {
            // Create a group if actions are on the same construct
            if (action.AffectedConstruct == lastAction?.AffectedConstruct && canCreateGroupTimer.TimeLeft > 0)
            {
                if (currentGroup == null) InitGroup();
                currentGroup!.AddAction(action);
                
                //Start group life ticker
                groupLifeTimer.Set(GroupLifeTime);
                return;
            }
            
            //If a group exists but action is on a different construct, add the group to undo
            if (currentGroup != null)
            {
                AddCurrentGroupToUndo();
                undoHistory.Push(action);
                return;
            }
            
            undoHistory.Push(action);
            
            //Set time during which it is possible to create a group
            canCreateGroupTimer.Set(CanCreateGroupTime);
        }

        private void InitGroup()
        {
            currentGroup = new GroupAction();
            if (undoHistory.Count > 0 && undoHistory.Peek() is not GroupAction) undoHistory.Pop();
            currentGroup.AddAction(lastAction);
        }

        private void AddCurrentGroupToUndo()
        {
            if (currentGroup == null) return;
            
            undoHistory.Push(currentGroup);
            // Debug.Log("Add active group to undo");
            groupLifeTimer.Clear();
            currentGroup = null;
        }
        
        #endregion
        
        public int UndoCount => undoHistory.Count;
        public int RedoCount => redoHistory.Count;
    }
}
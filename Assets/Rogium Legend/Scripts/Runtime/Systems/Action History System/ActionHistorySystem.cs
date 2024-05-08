using System.Collections.Generic;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// Keeps track of all actions that have been executed in the game.
    /// </summary>
    public static class ActionHistorySystem
    {
        private static readonly Stack<IAction> undoHistory = new();
        private static readonly Stack<IAction> redoHistory = new();
        
        public static void AddAndExecute(IAction action)
        {
            if (action.NothingChanged()) return;
            
            action.Execute();
            undoHistory.Push(action);
            redoHistory.Clear();
        }

        public static void UndoLastAction()
        {
            if (undoHistory.Count == 0) return;
            
            IAction lastAction = undoHistory.Pop();
            lastAction.Undo();
            redoHistory.Push(lastAction);
        }

        public static void RedoLastAction()
        {
            if (redoHistory.Count == 0) return;
            
            IAction lastAction = redoHistory.Pop();
            lastAction.Execute();
            undoHistory.Push(lastAction);
        }
    }
}
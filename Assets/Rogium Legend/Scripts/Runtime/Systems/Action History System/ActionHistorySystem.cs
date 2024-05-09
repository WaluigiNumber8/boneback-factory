using System.Collections.Generic;

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

        public static void AddAndExecute(IAction action)
        {
            if (action.NothingChanged()) return;
            if (assetDetector.WasAssetChanged) undoHistory.Clear();

            action.Execute();
            undoHistory.Push(action);
            redoHistory.Clear();
            
            assetDetector.MarkAsEdited();
        }

        public static void UndoLastAction()
        {
            if (undoHistory.Count == 0) return;

            IAction lastAction = undoHistory.Pop();
            redoHistory.Push(lastAction);
            lastAction.Undo();
        }

        public static void RedoLastAction()
        {
            if (redoHistory.Count == 0) return;

            IAction lastAction = redoHistory.Pop();
            undoHistory.Push(lastAction);
            lastAction.Execute();
        }

        public static int UndoCount => undoHistory.Count;
        public static int RedoCount => redoHistory.Count;
    }
}
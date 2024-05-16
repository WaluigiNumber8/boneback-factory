using System;
using System.Collections.Generic;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// Groups multiple actions into one.
    /// </summary>
    public class GroupAction : IAction
    {
        private readonly IList<IAction> actions = new List<IAction>();

        public void Execute()
        {
            foreach (IAction action in actions)
            {
                action.Execute();
            }
        }

        public void Undo()
        {
            for (int i = actions.Count - 1; i >= 0; i--)
            {
                actions[i].Undo();
            }
        }

        public void AddAction(IAction action) => actions.Add(action);

        public bool NothingChanged() => actions[0].LastValue.Equals(actions[^1].Value);
        
        public object AffectedConstruct => actions;
        public object Value { get => -1; }
        public object LastValue { get => -1; }

        public override string ToString() => $"{actions[0].AffectedConstruct} x {actions.Count}";
    }
}
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Rogium.Systems.Input
{
    public static class BindingRemover
    {
        public static void RemoveEmptyBindings(RogiumInputActions input)
        {
            foreach (InputActionMap map in input.asset.actionMaps)
            {
                foreach (InputAction action in map.actions)
                {
                    bool isInsideComposite = false;
                    int bindingsInComposite = 0;
                    IList<int> emptyIndexes = new List<int>();

                    for (int i = action.bindings.Count - 1; i >= 0; i--)
                    {
                        InputBinding binding = action.bindings[i];

                        if (binding.isComposite)
                        {
                            ProcessCompositeHeader(action, i, emptyIndexes, ref isInsideComposite, ref bindingsInComposite);
                            continue;
                        }

                        if (binding.isPartOfComposite) 
                        {
                            ProcessCompositePart(binding, i, emptyIndexes, ref isInsideComposite, ref bindingsInComposite);
                            continue;
                        }

                        ProcessNormalBinding(action, binding, i);
                    }
                }
            }
        }

        private static void ProcessCompositeHeader(InputAction action, int i, IList<int> emptyIndexes, ref bool isInsideComposite, ref int bindingsInComposite)
        {
            if (!isInsideComposite) return;
            
            isInsideComposite = false;

            //Reach last part of composite
            if (bindingsInComposite == emptyIndexes.Count)
            {
                //All parts of composite are empty
                action.ChangeBinding(i).Erase();
                isInsideComposite = false;
                return;
            }

            //Not all parts of composite are empty
            foreach (int index in emptyIndexes)
            {
                action.ChangeBinding(index).Erase();
            }
        }

        private static void ProcessCompositePart(InputBinding binding, int i, IList<int> emptyIndexes, ref bool isInsideComposite, ref int bindingsInComposite)
        {
            //Every other part of composite
            if (isInsideComposite)
            {
                bindingsInComposite++;
                if (binding.effectivePath == "") emptyIndexes.Add(i);
                return;
            }

            //First part of composite
            isInsideComposite = true;
            bindingsInComposite = 0;
            emptyIndexes.Clear();

            bindingsInComposite++;
            if (binding.effectivePath == "") emptyIndexes.Add(i);
        }

        private static void ProcessNormalBinding(InputAction action, InputBinding binding, int i)
        {
            if (binding.effectivePath != "") return;
            action.ChangeBinding(i).Erase();
        }
    }
}
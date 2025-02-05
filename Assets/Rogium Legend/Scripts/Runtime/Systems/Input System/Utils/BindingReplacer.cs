using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Rogium.Systems.Input
{
    /// <summary>
    /// Replaces TwoOptionalModifierComposites with built-in modifiers.
    /// </summary>
    public static class BindingReplacer
    {
        public static void ReplaceBindings(RogiumInputActions actions)
        {
            foreach (InputActionMap map in actions.asset.actionMaps)
            {
                foreach (InputAction action in map.actions)
                {
                    bool isInsideComposite = false;
                    IList<InputBinding> compositeBindings = new List<InputBinding>();

                    for (int i = action.bindings.Count - 1; i >= 0; i--)
                    {
                        InputBinding binding = action.bindings[i];

                        if (binding.isComposite)
                        {
                            isInsideComposite = false;
                            
                            if (binding.IsTwoOptionalModifiersComposite())
                            {
                                switch (compositeBindings.Count)
                                {
                                    case 1:
                                        action.AddBinding(compositeBindings[0].effectivePath);
                                        break;
                                    case 2:
                                        action.AddCompositeBinding("OneModifier").With("Modifier", compositeBindings[1].effectivePath).With("Binding", compositeBindings[0].effectivePath);
                                        break;
                                    case 3:
                                        action.AddCompositeBinding("TwoModifiers").With("Modifier1", compositeBindings[2].effectivePath).With("Modifier2", compositeBindings[1].effectivePath).With("Binding", compositeBindings[0].effectivePath);
                                        break;
                                }
                                
                                action.ChangeBinding(i).Erase();
                            }
                            continue;
                        }

                        if (binding.isPartOfComposite) 
                        {
                            if (isInsideComposite)
                            {
                                if (!string.IsNullOrEmpty(binding.effectivePath)) compositeBindings.Add(binding);
                                continue;   
                            }
                            
                            isInsideComposite = true;
                            compositeBindings.Clear();
                            if (!string.IsNullOrEmpty(binding.effectivePath)) compositeBindings.Add(binding);
                            continue;
                        }
                        
                        if (!string.IsNullOrEmpty(binding.effectivePath)) continue;
                        action.ChangeBinding(i).Erase();
                    }
                }
            }
        }
    }
}
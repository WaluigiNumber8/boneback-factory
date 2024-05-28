using Rogium.UserInterface.Interactables.Properties;
using UnityEditor;
using UnityEngine;

namespace Rogium.Tests.UI.Interactables
{
    /// <summary>
    /// Creates interactable properties for testing.
    /// </summary>
    public static class InteractablePropertyCreator
    {
        private static readonly InteractablePropertyToggle toggleProperty = AssetDatabase.LoadAssetAtPath<InteractablePropertyToggle>("Assets/Rogium Legend/Prefabs/UI/Interactables/Properties/Horizontal/pref_Property_Toggle_Blue.prefab");

        
        public static InteractablePropertyToggle CreateAndInitToggle(bool value = false)
        {
            InteractablePropertyToggle toggle = Object.Instantiate(toggleProperty, Vector3.zero, Quaternion.identity);
            toggle.Construct("Test Toggle", value, null);
            return toggle;
        }
    }
}
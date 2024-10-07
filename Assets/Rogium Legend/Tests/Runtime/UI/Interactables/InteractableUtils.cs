using RedRats.UI.ModalWindows;
using Rogium.UserInterface.ModalWindows;
using UnityEngine;

namespace Rogium.Tests.UI.Interactables
{
    public static class InteractableUtils
    {
        public static AssetPickerWindow FindFirstAssetPickerWindow() => Object.FindFirstObjectByType<AssetPickerWindow>(FindObjectsInactive.Include);

        public static ColorPickerWindow FindFirstColorPickerWindow() => Object.FindFirstObjectByType<ColorPickerWindow>(FindObjectsInactive.Include);
        public static ModalWindow FindFirstModalWindow() => Object.FindFirstObjectByType<ModalWindow>(FindObjectsInactive.Include);
    }
}
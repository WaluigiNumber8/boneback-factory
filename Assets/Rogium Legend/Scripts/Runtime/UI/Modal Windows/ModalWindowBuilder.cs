using RedRats.Core;
using Rogium.Systems.ThemeSystem;
using Rogium.UserInterface.Editors.AssetSelection.PickerVariant;
using Rogium.UserInterface.Editors.PropertyModalWindows;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.UserInterface.ModalWindows
{
    /// <summary>
    /// Builds modal windows.
    /// </summary>
    public class ModalWindowBuilder : MonoSingleton<ModalWindowBuilder>
    {
        [Title("Parent")]
        [SerializeField] private Transform windowParent;
        [Title("Modal Window Prefabs")]
        [SerializeField] private AssetPickerWindow pickerWindow;
        [SerializeField] private SoundPickerModalWindow soundPickerWindow;

        public AssetPickerWindow BuildAssetPickerWindow()
        {
            AssetPickerWindow window = Instantiate(pickerWindow, windowParent);
            ThemeUpdaterRogium.UpdateAssetPickerWindow(window);
            return window;
        }
    }
}
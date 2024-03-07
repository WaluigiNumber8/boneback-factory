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
        [SerializeField] private AssetPickerWindow assetPickerWindow;
        [SerializeField] private SoundPickerModalWindow soundPickerWindow;
        
        private AssetPickerWindow cachedAssetPickerWindow;

        protected override void Awake()
        {
            base.Awake();
            cachedAssetPickerWindow = Instantiate(assetPickerWindow, windowParent);
        }

        public AssetPickerWindow BuildAssetPickerWindow()
        {
            AssetPickerWindow window = (cachedAssetPickerWindow.IsOpen) ? Instantiate(assetPickerWindow, windowParent) : cachedAssetPickerWindow;
            ThemeUpdaterRogium.UpdateAssetPickerWindow(window);
            return window;
        }
    }
}
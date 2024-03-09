using System;
using RedRats.Core;
using RedRats.UI.ModalWindows;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Sounds;
using Rogium.Systems.ThemeSystem;
using Rogium.UserInterface.Editors.AssetSelection.PickerVariant;
using Rogium.UserInterface.Editors.PropertyModalWindows;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.UserInterface.ModalWindows
{
    /// <summary>
    /// Builds and opens modal windows.
    /// </summary>
    public class ModalWindowBuilder : MonoSingleton<ModalWindowBuilder>
    {
        [Title("Parent")]
        [SerializeField] private Transform windowParent;
        [Title("Modal Window Prefabs")]
        [SerializeField] private AssetPickerWindow assetPickerWindow;
        [SerializeField] private SoundPickerModalWindow soundPickerWindow;
        
        private ModalWindowOverseerMono windowOverseer;
        
        private AssetPickerWindow cachedAssetPickerWindow;
        private SoundPickerModalWindow cachedSoundPickerWindow;

        protected override void Awake()
        {
            base.Awake();
            windowOverseer = ModalWindowOverseerMono.GetInstance();
            
            cachedAssetPickerWindow = Instantiate(assetPickerWindow, windowParent);
            cachedAssetPickerWindow.Close();
            cachedSoundPickerWindow = Instantiate(soundPickerWindow, windowParent);
            cachedSoundPickerWindow.Close();
        }

        public void OpenMessageWindow(MessageWindowInfo data)
        {
            windowOverseer.OpenWindow(data);
        }
        
        public void OpenPropertyWindowColumns1(PropertyWindowInfo data, string key, out Transform column1)
        {
            windowOverseer.OpenWindow(data, key);
            column1 = windowOverseer.GetColumn1(key);
        }
        
        public void OpenPropertyWindowColumns2(PropertyWindowInfo data, string key, out Transform column1, out Transform column2)
        {
            windowOverseer.OpenWindow(data, key);
            column1 = windowOverseer.GetColumn1(key);
            column2 = windowOverseer.GetColumn2(key);
        }
        
        /// <summary>
        /// Opens the asset picker window.
        /// </summary>
        /// <param name="type">What types of assets to grab.</param>
        /// <param name="whenAssetPicked">The method that runs when the asset is picked.</param>
        /// <param name="preselectedAsset">The asset that will be selected on window open.</param>
        public void OpenAssetPickerWindow(AssetType type, Action<IAsset> whenAssetPicked, IAsset preselectedAsset = null)
        {
            AssetPickerWindow window = (cachedAssetPickerWindow.IsOpen) ? Instantiate(assetPickerWindow, windowParent) : cachedAssetPickerWindow;
            window.Construct(type, whenAssetPicked, preselectedAsset);
            ThemeUpdaterRogium.UpdateAssetPickerWindow(window);
            window.Open();
        }
        
        /// <summary>
        /// Opens the sound picker window.
        /// </summary>
        /// <param name="onChangeSound">Method that runs when the currently edited sound is changed.</param>
        /// <param name="onChangeAnyValue">Method that runs when any property in the window is updated.</param>
        /// <param name="value">Which data to load up into the window.</param>
        public void OpenSoundPickerWindow(Action<SoundAsset> onChangeSound, Action<AssetData> onChangeAnyValue, AssetData value)
        {
            SoundPickerModalWindow window = (cachedSoundPickerWindow.IsOpen) ? Instantiate(soundPickerWindow, windowParent) : cachedSoundPickerWindow;
            window.Construct(onChangeSound, onChangeAnyValue, value);
            ThemeUpdaterRogium.UpdateSoundPickerWindow(window);
            window.Open();
        }
       
    }
}
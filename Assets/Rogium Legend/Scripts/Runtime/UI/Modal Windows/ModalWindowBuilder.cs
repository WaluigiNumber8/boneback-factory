using System;
using RedRats.Core;
using RedRats.UI.ModalWindows;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Sounds;
using Rogium.Systems.ThemeSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.UserInterface.ModalWindows
{
    /// <summary>
    /// Builds and opens modal windows.
    /// </summary>
    public class ModalWindowBuilder : MonoSingleton<ModalWindowBuilder>
    {
        [Title("Settings")] 
        [SerializeField] private ModalWindowGenerator windowGenerator;
        [SerializeField] private Transform windowParent;
        
        [Title("Modal Window Prefabs")]
        [SerializeField] private AssetPickerWindow assetPickerWindow;
        [SerializeField] private SoundPickerModalWindow soundPickerWindow;
        
        private AssetPickerWindow cachedAssetPickerWindow;
        private SoundPickerModalWindow cachedSoundPickerWindow;

        protected override void Awake()
        {
            base.Awake();
            cachedAssetPickerWindow = Instantiate(assetPickerWindow, windowParent);
            cachedAssetPickerWindow.Close();
            cachedSoundPickerWindow = Instantiate(soundPickerWindow, windowParent);
            cachedSoundPickerWindow.Close();
        }

        /// <summary>
        /// Opens a new message window.
        /// </summary>
        /// <param name="data">Data to load the window with.</param>
        public void OpenMessageWindow(MessageWindowInfo data)
        {
            windowGenerator.Open(data);
        }
        
        /// <summary>
        /// Opens a property window with a single column.
        /// </summary>
        /// <param name="data">The data to load the window with.</param>
        /// <param name="key">Unique key that will identify the window.</param>
        /// <param name="column1">Transform of the property column.</param>
        public void OpenPropertyWindowColumns1(PropertyWindowInfo data, string key, out Transform column1)
        {
            windowGenerator.Open(data, key);
            column1 = windowGenerator.GetColumn1(key);
        }
        
        /// <summary>
        /// Opens a property window with two columns.
        /// </summary>
        /// <param name="data">The data to load the window with.</param>
        /// <param name="key">Unique key that will identify the window.</param>
        /// <param name="column1">Transform of the 1st column.</param>
        /// <param name="column2">Transform of the 2nd column.</param>
        public void OpenPropertyWindowColumns2(PropertyWindowInfo data, string key, out Transform column1, out Transform column2)
        {
            windowGenerator.Open(data, key);
            column1 = windowGenerator.GetColumn1(key);
            column2 = windowGenerator.GetColumn2(key);
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
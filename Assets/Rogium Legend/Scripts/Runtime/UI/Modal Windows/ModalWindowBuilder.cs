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
        
        [Title("Modal Window Prefabs")]
        [SerializeField] private AssetPickerWindow assetPickerWindow;
        [SerializeField] private SoundPickerWindow soundPickerWindow;
        [SerializeField] private ColorPickerWindow colorPickerWindow;
        
        private AssetPickerWindow cachedAssetPickerWindow;
        private SoundPickerWindow cachedSoundPickerWindow;
        private ColorPickerWindow cachedColorPickerWindow;

        protected override void Awake()
        {
            base.Awake();
            cachedAssetPickerWindow = Instantiate(assetPickerWindow, WindowParent);
            cachedAssetPickerWindow.Close();
            cachedSoundPickerWindow = Instantiate(soundPickerWindow, WindowParent);
            cachedSoundPickerWindow.Close();
            cachedColorPickerWindow = Instantiate(colorPickerWindow, WindowParent);
            cachedColorPickerWindow.Close();
        }

        /// <summary>
        /// Opens a new generic modal window.
        /// </summary>
        /// <param name="data">Data to load the window with.</param>
        public void OpenWindow(ModalWindowData data)
        {
            data.UpdateLayout(ModalWindowLayoutType.Message);
            windowGenerator.Open(data);
        }
        
        /// <summary>
        /// Opens a new generic window.
        /// </summary>
        /// <param name="data">The data to load the window with.</param>
        /// <param name="key">Unique key that will identify the window.</param>
        /// <param name="column1">Transform of the property column.</param>
        public void OpenWindow(ModalWindowData data, string key, out Transform column1)
        {
            data.UpdateLayout(ModalWindowLayoutType.Columns1);
            windowGenerator.Open(data, key);
            column1 = windowGenerator.GetColumn1(key);
        }
        
        /// <summary>
        /// Opens a new generic window.
        /// </summary>
        /// <param name="data">The data to load the window with.</param>
        /// <param name="key">Unique key that will identify the window.</param>
        /// <param name="column1">Transform of the 1st column.</param>
        /// <param name="column2">Transform of the 2nd column.</param>
        public void OpenWindow(ModalWindowData data, string key, out Transform column1, out Transform column2)
        {
            data.UpdateLayout(ModalWindowLayoutType.Columns2);
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
        public void OpenAssetPickerWindow(AssetType type, Action<IAsset> whenAssetPicked, IAsset preselectedAsset = null, bool canSelectEmpty = false)
        {
            AssetPickerWindow window = (cachedAssetPickerWindow.IsOpen) ? Instantiate(assetPickerWindow, WindowParent) : cachedAssetPickerWindow;
            window.Construct(type, whenAssetPicked, preselectedAsset, canSelectEmpty);
            ThemeUpdaterRogium.UpdateAssetPickerWindow(window);
            SendToFront(window);
            window.Open();
        }
        
        /// <summary>
        /// Opens the sound picker window.
        /// </summary>
        /// <param name="whenSoundChanged">Method that runs when the currently edited sound is changed.</param>
        /// <param name="onChangeAnyValue">Method that runs when any property in the window is updated.</param>
        /// <param name="value">Which data to load up into the window.</param>
        public void OpenSoundPickerWindow(Action<SoundAsset> whenSoundChanged, Action<AssetData> onChangeAnyValue, AssetData value)
        {
            SoundPickerWindow window = (cachedSoundPickerWindow.IsOpen) ? Instantiate(soundPickerWindow, WindowParent) : cachedSoundPickerWindow;
            window.Construct(whenSoundChanged, onChangeAnyValue, value);
            ThemeUpdaterRogium.UpdateSoundPickerWindow(window);
            SendToFront(window);
            window.Open();
        }
        
        /// <summary>
        /// Opens a new color picker window.
        /// </summary>
        /// <param name="whenColorChanged">Method that runs when the edited color is changed.</param>
        /// <param name="preselectedColor">Which data to load up into the window.</param>
        public void OpenColorPickerWindow(Action<Color> whenColorChanged, Color preselectedColor)
        {
            ColorPickerWindow window = (cachedColorPickerWindow.IsOpen) ? Instantiate(colorPickerWindow, WindowParent) : cachedColorPickerWindow;
            window.Construct(whenColorChanged, preselectedColor);
            ThemeUpdaterRogium.UpdateColorPickerWindow(window);
            SendToFront(window);
            window.Open();
        }

        private void SendToFront(ModalWindowBase window) => window.transform.SetAsLastSibling();

        private Transform WindowParent => windowGenerator.PoolParent;
        
        public int GenericActiveWindows => windowGenerator.ActiveWindows;
    }
}
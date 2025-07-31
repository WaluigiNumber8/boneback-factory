using RedRats.Core;
using Rogium.Options.OptionControllers;
using Rogium.Systems.Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Overseers the Options Menu.
    /// </summary>
    public class OptionsMenuOverseerMono : MonoSingleton<OptionsMenuOverseerMono>
    {
        [SerializeField, FoldoutGroup("Controllers")] private AudioOptionsController audioOptions;
        [SerializeField, FoldoutGroup("Controllers")] private GraphicsOptionsController graphicsOptions;
        
        [SerializeField, FoldoutGroup("Columns")] private Transform audioColumn;
        [SerializeField, FoldoutGroup("Columns")] private Transform graphicsColumn;
        [SerializeField, FoldoutGroup("Columns")] private Transform shortcutKeyboardColumn;
        [SerializeField, FoldoutGroup("Columns")] private Transform shortcutGamepadColumn;
        [SerializeField, FoldoutGroup("Columns")] private Transform inputKeyboardColumn;
        [SerializeField, FoldoutGroup("Columns")] private Transform inputGamepadColumn;
        
        private OptionsAudioPropertyBuilder audioPropertyBuilder;
        private OptionsGraphicsPropertyBuilder graphicsPropertyBuilder;
        private OptionsShortcutPropertyBuilder shortcutPropertyBuilder;
        private OptionsInputPropertyBuilder inputPropertyBuilder;
        
        private OptionsMenuOverseer editor;
        
        protected override void Awake()
        {
            base.Awake();
            editor = OptionsMenuOverseer.Instance;
            audioPropertyBuilder = new OptionsAudioPropertyBuilder(audioColumn, audioOptions);
            graphicsPropertyBuilder = new OptionsGraphicsPropertyBuilder(graphicsColumn, graphicsOptions);
            shortcutPropertyBuilder = new OptionsShortcutPropertyBuilder(shortcutKeyboardColumn, shortcutGamepadColumn);
            inputPropertyBuilder = new OptionsInputPropertyBuilder(inputKeyboardColumn, inputGamepadColumn);
        }

        private void OnEnable()
        {
            editor.OnAssignAsset += PrepareEditor;
            editor.OnApplySettings += ApplyAllOptions;
        }

        private void OnDisable()
        {
            editor.OnAssignAsset -= PrepareEditor;
            editor.OnApplySettings -= ApplyAllOptions;
        }
        
        /// <summary>
        /// Applies all settings from a specific <see cref="PreferencesAsset"/>.
        /// </summary>
        /// <param name="asset">The data to apply to settings.</param>
        public void ApplyAllOptions(GameDataAsset asset)
        {
            audioOptions.UpdateMasterVolume(asset.Preferences.MasterVolume);
            audioOptions.UpdateMusicVolume(asset.Preferences.MusicVolume);
            audioOptions.UpdateSoundVolume(asset.Preferences.SoundVolume);
            audioOptions.UpdateUIVolume(asset.Preferences.UIVolume);
            
            graphicsOptions.UpdateResolution(asset.Preferences.GetResolution());
            graphicsOptions.UpdateScreen(asset.Preferences.ScreenMode);
            graphicsOptions.UpdateVSync(asset.Preferences.VSync);
            
            ShortcutToAssetConverter.Load(asset.ShortcutBindings);
            InputToAssetConverter.Load(asset.InputBindings);
        }

        /// <summary>
        /// Dispose all properties from the options menu.
        /// </summary>
        public void DisposeProperties()
        {
            audioPropertyBuilder.Clear();
            graphicsPropertyBuilder.Clear();
            shortcutPropertyBuilder.Clear();
            inputPropertyBuilder.Clear();
        }
        
        /// <summary>
        /// Prepares the Options menu for the user. 
        /// </summary>
        private void PrepareEditor(GameDataAsset asset)
        {
            audioPropertyBuilder.Build(asset.Preferences);
            graphicsPropertyBuilder.Build(asset.Preferences);
            shortcutPropertyBuilder.Build(asset.ShortcutBindings);
            inputPropertyBuilder.Build(asset.InputBindings);
        }
    }
}
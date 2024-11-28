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
        [SerializeField, FoldoutGroup("Columns")] private Transform inputKeyboardColumn;
        [SerializeField, FoldoutGroup("Columns")] private Transform inputGamepadColumn;
        
        private OptionsAudioPropertyBuilder audioPropertyBuilder;
        private OptionsGraphicsPropertyBuilder graphicsPropertyBuilder;
        private OptionsInputPropertyBuilder inputPropertyBuilder;
        
        private OptionsMenuOverseer editor;
        
        protected override void Awake()
        {
            base.Awake();
            editor = OptionsMenuOverseer.Instance;
            audioPropertyBuilder = new OptionsAudioPropertyBuilder(audioColumn, audioOptions);
            graphicsPropertyBuilder = new OptionsGraphicsPropertyBuilder(graphicsColumn, graphicsOptions);
            inputPropertyBuilder = new OptionsInputPropertyBuilder(inputKeyboardColumn, inputGamepadColumn);
        }

        private void OnEnable()
        {
            editor.OnAssignAsset += PrepareEditor;
            editor.OnApplySettings += ApplyAllSettings;
        }

        private void OnDisable()
        {
            editor.OnAssignAsset -= PrepareEditor;
            editor.OnApplySettings -= ApplyAllSettings;
        }
        
        /// <summary>
        /// Applies all settings from a specific <see cref="PreferencesAsset"/>.
        /// </summary>
        /// <param name="asset">The data to apply to settings.</param>
        public void ApplyAllSettings(GameDataAsset asset)
        {
            audioOptions.UpdateMasterVolume(asset.Preferences.MasterVolume);
            audioOptions.UpdateMusicVolume(asset.Preferences.MusicVolume);
            audioOptions.UpdateSoundVolume(asset.Preferences.SoundVolume);
            audioOptions.UpdateUIVolume(asset.Preferences.UIVolume);
            
            graphicsOptions.UpdateResolution(asset.Preferences.GetResolution());
            graphicsOptions.UpdateScreen(asset.Preferences.ScreenMode);
            graphicsOptions.UpdateVSync(asset.Preferences.VSync);
            
            InputToAssetConverter.ApplyInput(asset.InputBindings);
        }
        
        /// <summary>
        /// Prepares the Options menu for the user. 
        /// </summary>
        private void PrepareEditor(GameDataAsset asset)
        {
            audioPropertyBuilder.Build(asset.Preferences);
            graphicsPropertyBuilder.Build(asset.Preferences);
            inputPropertyBuilder.Build(asset.InputBindings);
        }
    }
}
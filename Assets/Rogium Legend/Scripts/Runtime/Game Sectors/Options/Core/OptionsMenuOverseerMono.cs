using RedRats.Core;
using Rogium.Options.OptionControllers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Overseers the Options Menu.
    /// </summary>
    public class OptionsMenuOverseerMono : MonoSingleton<OptionsMenuOverseerMono>
    {
        [SerializeField, FoldoutGroup("Controllers")] private AudioOptionsController audio;
        [SerializeField, FoldoutGroup("Controllers")] private GraphicsOptionsController graphics;
        
        [SerializeField, FoldoutGroup("Columns")] private Transform audioColumn;
        [SerializeField, FoldoutGroup("Columns")] private Transform graphicsColumn;
        
        private OptionsAudioPropertyBuilder audioPropertyBuilder;
        private OptionsGraphicsPropertyBuilder graphicsPropertyBuilder;
        
        private OptionsMenuOverseer editor;
        
        protected override void Awake()
        {
            base.Awake();
            editor = OptionsMenuOverseer.Instance;
            audioPropertyBuilder = new OptionsAudioPropertyBuilder(audioColumn, audio);
            graphicsPropertyBuilder = new OptionsGraphicsPropertyBuilder(graphicsColumn, graphics);
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
        /// Applies all settings from a specific <see cref="GameDataAsset"/>.
        /// </summary>
        /// <param name="asset">The data to apply to settings.</param>
        public void ApplyAllSettings(GameDataAsset asset)
        {
            audio.UpdateMasterVolume(asset.MasterVolume);
            audio.UpdateMusicVolume(asset.MusicVolume);
            audio.UpdateSoundVolume(asset.SoundVolume);
            audio.UpdateUIVolume(asset.UIVolume);
            
            graphics.UpdateResolution(asset.GetResolution());
            graphics.UpdateScreen(asset.ScreenMode);
            graphics.UpdateVSync(asset.VSync);
        }
        
        /// <summary>
        /// Prepares the Options menu for the user. 
        /// </summary>
        private void PrepareEditor(GameDataAsset asset)
        {
            audioPropertyBuilder.Build(asset);
            graphicsPropertyBuilder.Build(asset);
        }
        
    }
}
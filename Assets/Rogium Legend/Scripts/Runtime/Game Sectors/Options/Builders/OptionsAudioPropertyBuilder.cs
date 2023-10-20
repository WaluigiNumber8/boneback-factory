using Rogium.Options.OptionControllers;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Options.Core
{
    public class OptionsAudioPropertyBuilder : UIPropertyContentBuilderBaseColumn1
    {
        private readonly AudioOptionsController audio;

        public OptionsAudioPropertyBuilder(Transform contentMain, AudioOptionsController audio) : base(contentMain)
        {
            this.audio = audio;
        }

        public void Build(GameDataAsset data)
        {
            Clear();
            b.BuildSlider("Master", 0f, 1f, data.MasterVolume, contentMain, value =>
            {
                data.UpdateMasterVolume(value);
                audio.UpdateMasterVolume(value);
            });
            //TODO Enable when music is added.
            // b.BuildSlider("Music", 0f, 1f, data.MusicVolume, contentMain, value =>
            // {
            //     data.UpdateMusicVolume(value);
            //     audio.UpdateMusicVolume(value);
            // });
            b.BuildSlider("Sound", 0f, 1f, data.SoundVolume, contentMain, value =>
            {
                data.UpdateSoundVolume(value);
                audio.UpdateSoundVolume(value);
            });
            b.BuildSlider("UI", 0f, 1f, data.UIVolume, contentMain, value =>
            {
                data.UpdateUIVolume(value);
                audio.UpdateUIVolume(value);
            });
        }
    }
}
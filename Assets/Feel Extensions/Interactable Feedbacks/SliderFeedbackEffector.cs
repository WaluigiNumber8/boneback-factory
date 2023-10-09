using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables.Feedbacks
{
    /// <summary>
    /// The assigned <see cref="MMF_Player"/> is affected by the assigned <see cref="Slider"/>.
    /// </summary>
    public class SliderFeedbackEffector : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private MMF_Player feedback;
        [SerializeField] private bool playOnValueChange = true;

        /// <summary>
        /// Does the pitch of the first MMSoundManagerSound found on the feedback change according to the slider?
        /// </summary>
        [Tooltip("Does the pitch of the first MMSoundManagerSound found on the feedback change according to the slider?")]
        [SerializeField] private bool affectPitch;

        [SerializeField, HorizontalGroup, ShowIf("affectPitch"), LabelText("Pitch Min/Max")] private float pitchMin;
        [SerializeField, HorizontalGroup, ShowIf("affectPitch"), HideLabel] private float pitchMax;
        
        private MMF_MMSoundManagerSound soundFeedback;

        private void Awake()
        {
            soundFeedback = feedback.GetFeedbackOfType<MMF_MMSoundManagerSound>();
        }

        private void OnEnable() => slider.onValueChanged.AddListener(WhenValueChange);

        private void OnDisable() => slider.onValueChanged.RemoveListener(WhenValueChange);

        private void WhenValueChange(float value)
        {
            if (affectPitch) SetPitch(NormalizeSliderValueToPitch(value));
            if (playOnValueChange) feedback.PlayFeedbacks();
        }

        /// <summary>
        /// Sets the pitch of the first found <see cref="MMF_MMSoundManagerSound"/> feedback to a specific value.
        /// </summary>
        /// <param name="value">Sound's new pitch.</param>
        private void SetPitch(float value)
        {
            if (soundFeedback == null) return;
            soundFeedback.MinPitch = soundFeedback.MaxPitch = value;
        }

        /// <summary>
        /// Normalizes a slider value to a pitch value, ranging from 0.5f to 1.5f.
        /// </summary>
        /// <param name="value">The value of the slider.</param>
        /// <returns>The new value.</returns>
        private float NormalizeSliderValueToPitch(float value)
        {
            float modifier = (slider.maxValue - value) / (slider.maxValue - slider.minValue);
            return pitchMax - modifier * (pitchMax - pitchMin);
        }
    }
}
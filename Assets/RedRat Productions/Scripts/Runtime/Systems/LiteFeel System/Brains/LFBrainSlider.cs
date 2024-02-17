using System.Collections;
using RedRats.Systems.LiteFeel.Core;
using RedRats.Systems.LiteFeel.Effects;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace RedRats.Systems.LiteFeel.Brains
{
    /// <summary>
    /// Allocates LF effects to a <see cref="Slider"/>.
    /// </summary>
    public class LFBrainSlider : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private LFEffector valueChangeEffect;
        [SerializeField] private bool playOnValueChange = true;

        /// <summary>
        /// Change pitch of a specific <see cref="LFAudioEffect"/> based on the slider's value.
        /// </summary>
        [Tooltip("Change pitch of a specific LFAudioEffect based on the slider's value.")]
        [SerializeField] private bool affectPitch;
        [SerializeField, ShowIf("affectPitch")] private LFAudioEffect audioEffect;
        [SerializeField, HorizontalGroup(Width = 0.7f, DisableAutomaticLabelWidth = true), LabelText("Pitch Min/Max"), ShowIf("affectPitch")] private float pitchMin;
        [SerializeField, HorizontalGroup(Width = 0.3f, DisableAutomaticLabelWidth = true), ShowIf("affectPitch"), HideLabel] private float pitchMax;
        
        private void OnEnable()
        {
            StartCoroutine(EnableSliderListeningCoroutine());
            IEnumerator EnableSliderListeningCoroutine()
            {
                yield return null;
                slider.onValueChanged.AddListener(WhenValueChange);
            }
        }

        private void OnDisable() => slider.onValueChanged.RemoveListener(WhenValueChange);

        private void WhenValueChange(float value)
        {
            if (affectPitch) SetPitch(NormalizeSliderValueToPitch(value));
            if (playOnValueChange) valueChangeEffect.Play();
        }

        private void SetPitch(float value)
        {
            if (audioEffect == null) return;
            audioEffect.ChangePitch(value);
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
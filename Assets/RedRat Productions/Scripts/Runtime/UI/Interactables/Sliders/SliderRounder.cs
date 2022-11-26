using RedRats.Core;
using UnityEngine;
using UnityEngine.UI;

namespace RedRats.UI.Sliders
{
    /// <summary>
    /// Handles the rounding of the sliders value.
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class SliderRounder : MonoBehaviour
    {
        [SerializeField, Range(1, 5)] private int decimals = 2;
        
        private Slider slider;
        private bool wasRounded;

        // private void Awake() => slider = GetComponent<Slider>();
        // private void OnEnable() => slider.onValueChanged.AddListener(WhenValueChanged);
        // private void OnDisable() => slider.onValueChanged.RemoveListener(WhenValueChanged);
        //
        //
        // /// <summary>
        // /// Rounds the value of the slider when it is changed.
        // /// </summary>
        // /// <param name="value">The value to round.</param>
        // private void WhenValueChanged(float value)
        // {
        //     if (slider.wholeNumbers) return;
        //     if (wasRounded)
        //     {
        //         wasRounded = false;
        //         return;
        //     }
        //     
        //     slider.value = slider.value.Round(decimals);
        //     wasRounded = true;
        // }
    }
}
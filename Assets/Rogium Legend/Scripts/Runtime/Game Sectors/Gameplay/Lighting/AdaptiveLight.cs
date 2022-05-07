using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Rogium.Gameplay.Core.Lighting
{
    /// <summary>
    /// Adapts a light to it's surroundings.
    /// </summary>
    [RequireComponent(typeof(Light2D))]
    public class AdaptiveLight : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float visibilityLimit = 0.75f;
        
        private new Light2D light;

        private void Awake() => light = GetComponent<Light2D>();
        private void OnEnable() => RoomLight.GetInstance().OnChangeIntensity += AdaptVisibility;
        private void OnDisable() => RoomLight.GetInstance().OnChangeIntensity -= AdaptVisibility;

        /// <summary>
        /// Adapts the visibility of the light.
        /// </summary>
        /// <param name="globalIntensity">The intensity to adapt to.</param>
        private void AdaptVisibility(float globalIntensity)
        {
            light.enabled = (globalIntensity <= visibilityLimit);
        }
    }
}
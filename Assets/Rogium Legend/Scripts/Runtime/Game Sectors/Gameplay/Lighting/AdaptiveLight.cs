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

        private RoomLight globalLight;
        private new Light2D light;

        private void Awake()
        {
            globalLight = RoomLight.GetInstance();
            light = GetComponent<Light2D>();
            AdaptVisibility(0);
        }

        private void OnEnable()
        {
            if (globalLight != null) globalLight.OnChangeIntensity += AdaptVisibility;
        }

        private void OnDisable()
        {
            if (globalLight != null) globalLight.OnChangeIntensity -= AdaptVisibility;
        }

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
using System;
using RedRats.Core;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Rogium.Gameplay.Core.Lighting
{
    /// <summary>
    /// The central room light.
    /// </summary>
    [RequireComponent(typeof(Light2D))]
    public class RoomLight : MonoSingleton<RoomLight>
    {
        public event Action<float> OnChangeIntensity; 

        private Light2D roomLight;

        protected override void Awake()
        {
            base.Awake();
            roomLight = GetComponent<Light2D>();
        }

        /// <summary>
        /// Updates the room's intensity.
        /// </summary>
        /// <param name="newIntensity">The new intensity to use.</param>
        public void UpdateIntensity(float newIntensity)
        {
            roomLight.intensity = newIntensity;
            OnChangeIntensity?.Invoke(roomLight.intensity);
        }
    }
}
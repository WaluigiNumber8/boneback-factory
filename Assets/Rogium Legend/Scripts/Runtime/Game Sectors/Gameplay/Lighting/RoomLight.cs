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
        /// Constructs the Room Light with new parameters.
        /// </summary>
        /// <param name="intensity">The new intensity to use.</param>
        /// <param name="color">The new color of te light.</param>
        public void Construct(float intensity, Color color)
        {
            roomLight.intensity = intensity;
            roomLight.color = color;
            OnChangeIntensity?.Invoke(roomLight.intensity);
        }
    }
}
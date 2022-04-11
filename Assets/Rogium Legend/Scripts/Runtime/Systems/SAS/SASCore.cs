using System.Collections;
using BoubakProductions.Core;
using BoubakProductions.Systems.ObjectTransport;
using Rogium.UserInterface.TransitionsCanvas;
using UnityEngine;

namespace Rogium.Systems.SAS
{
    /// <summary>
    /// The core of the Sequence Action System - provides the most basic functions.
    /// </summary>
    public class SASCore : MonoSingleton<SASCore>
    {
        [SerializeField] private TransitionCanvas transitionCanvas;

        private ObjectTransporter transporter;
        private float timer;

        protected override void Awake()
        {
            base.Awake();
            transporter = new ObjectTransporter();
            transitionCanvas.gameObject.SetActive(true);
        }

        /// <summary>
        /// Transports a transform into a desired position in a specific axis order.
        /// </summary>
        /// <param name="transform">The transform to transfer.</param>
        /// <param name="position">The position to move the transform to.</param>
        /// <param name="speed">Speed of the transport.</param>
        /// <param name="order">Axis order to use.</param>
        public IEnumerator Transport(Transform transform, Vector3 position, float speed, TransportOrderType order = TransportOrderType.XYZ)
        {
            yield return transporter.Transport(transform, position, speed, order);
        }

        public IEnumerator FadeIn(float duration, bool waitForCompletion)
        {
            yield return transitionCanvas.FadeIn(duration, waitForCompletion);
        }
        
        public IEnumerator FadeOut(float duration, bool waitForCompletion)
        {
            yield return transitionCanvas.FadeOut(duration, waitForCompletion);
        }

        /// <summary>
        /// Wait for a specific time.
        /// </summary>
        /// <param name="duration">The time to wait.</param>
        public IEnumerator Wait(float duration)
        {
            yield return new WaitForSeconds(duration);
        }
        
    }
}
using System;
using System.Collections;
using RedRats.Core;
using UnityEngine;

namespace RedRats.Systems.ObjectTransport
{
    /// <summary>
    /// Allows Transforms to move to a specific position.
    /// </summary>
    public class ObjectTransporter
    {
        private Transform transform;
        private Vector3 destination;
        private float speed;
        
        /// <summary>
        /// Transports a <see cref="Transform"/> into a specific position.
        /// <param name="newDestination">The position to transport to.</param>
        /// <param name="speed">The speed of the transport.</param>
        /// <param name="order">The order of movement on different axis. Axis not mentioned will happen in the background.</param>
        /// </summary>
        public IEnumerator Transport(Transform transform, Vector3 newDestination, float speed, TransportOrderType order = TransportOrderType.XY)
        {
            this.transform = transform;
            destination = newDestination;
            this.speed = speed * Time.deltaTime;

            IEnumerator moveX = MoveOnX();
            IEnumerator moveY = MoveOnY();
            
            switch (order)
            {
                case TransportOrderType.XY:
                    yield return moveX;
                    yield return moveY;
                    break;
                case TransportOrderType.YX:
                    yield return moveY;
                    yield return moveX;
                    break;
                default: throw new ArgumentOutOfRangeException($"The order type of '{order}' is unknown or unsupported.");
            }
        }

        private IEnumerator MoveOnX() => MoveOnX(destination, speed);
        private IEnumerator MoveOnX(Vector3 targetPos, float speed)
        {
            while (!FloatUtils.AreEqual(transform.position.x, targetPos.x, 0.001f))
            {
                Vector3 position = transform.position;
                position = new Vector3(Mathf.MoveTowards(position.x, targetPos.x, speed), position.y, position.z);
                transform.position = position;
                yield return null;
            }
        }
        
        private IEnumerator MoveOnY() => MoveOnY(destination, speed);
        private IEnumerator MoveOnY(Vector3 targetPos, float speed)
        {
            while (!FloatUtils.AreEqual(transform.position.y, targetPos.y, 0.001f))
            {
                Vector3 position = transform.position;
                position = new Vector3(position.x, Mathf.MoveTowards(position.y, targetPos.y, speed), position.z);
                transform.position = position;
                yield return null;
            }
        }
    }
}

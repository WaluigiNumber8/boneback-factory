using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RedRats.Core;
using UnityEngine;

namespace RedRats.Systems.ObjectTransport
{
    /// <summary>
    /// Allows Transforms to move to a specific position.
    /// </summary>
    public class ObjectTransporterAsync
    {
        private readonly IList<Task> moves = new List<Task>();
        
        private Transform transform;
        private Vector3 destination;
        private float speed;

        /// <summary>
        /// Transports a <see cref="Transform"/> into a specific position.
        /// <param name="newDestination">The position to transport to.</param>
        /// <param name="speed">The speed of the transport.</param>
        /// <param name="order">The order of movement on different axis. Axis not mentioned will happen in the background.</param>
        /// </summary>
        public async Task Transport(Transform transform, Vector3 newDestination, float speed, TransportOrderType order = TransportOrderType.XY, Action finishMethod = null)
        {
            this.transform = transform;
            destination = newDestination;
            this.speed = speed * 0.01f;
            moves.Clear();
            
            switch (order)
            {
                case TransportOrderType.XY:
                    await MoveOnX();
                    await MoveOnY();
                    break;
                case TransportOrderType.YX:
                    await MoveOnY();
                    await MoveOnX();
                    break;
                default: throw new ArgumentOutOfRangeException($"The order type of '{order}' is unknown or unsupported.");
            }

            await Task.WhenAll(moves);
            finishMethod?.Invoke();
        }

        private async Task MoveOnX() => await MoveOnX(destination, speed);
        private async Task MoveOnX(Vector3 targetPos, float speed)
        {
            while (!FloatUtils.AreEqual(transform.position.x, targetPos.x, 0.001f))
            {
                Vector3 position = transform.position;
                position = new Vector3(Mathf.MoveTowards(position.x, targetPos.x, speed), position.y, position.z);
                transform.position = position;
                await Task.Yield();
            }
        }
        
        private async Task MoveOnY() => await MoveOnY(destination, speed);
        private async Task MoveOnY(Vector3 targetPos, float speed)
        {
            while (!FloatUtils.AreEqual(transform.position.y, targetPos.y, 0.001f))
            {
                Vector3 position = transform.position;
                position = new Vector3(position.x, Mathf.MoveTowards(position.y, targetPos.y, speed), position.z);
                transform.position = position;
                await Task.Yield();
            }
        }
    }
}

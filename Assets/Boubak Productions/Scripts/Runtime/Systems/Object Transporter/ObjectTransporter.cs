using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoubakProductions.Core;
using UnityEngine;

namespace BoubakProductions.Systems.ObjectTransport
{
    /// <summary>
    /// Allows Transforms to move to a specific position.
    /// </summary>
    public class ObjectTransporter
    {
        private readonly Transform transform;
        private readonly IList<Task> moves;
        
        private Vector3 destination;
        private float speed;
        
        public ObjectTransporter(Transform transform)
        {
            this.transform = transform;
            moves = new List<Task>();
        }

        /// <summary>
        /// Transports a transform into a specific position.
        /// <param name="newDestination">The position to transport the transform to.</param>
        /// <param name="speed">The speed of the transport.</param>
        /// <param name="order">The order of movement on different axis. Axis not mentioned will happen in the background.</param>
        /// </summary>
        public void Transport(Vector3 newDestination, float speed, Action finishMethod = null, TransportOrderType order = TransportOrderType.XYZ)
        {
            Transport(newDestination, speed, Vector3.zero, Vector3.zero, finishMethod, order);
        }
        /// <summary>
        /// Transports a transform into a specific position.
        /// <param name="newDestination">The position to transport the transform to.</param>
        /// <param name="speed">The speed of the transport.</param>
        /// <param name="beforeTransport">Where to transport before the main act. (offset).</param>
        /// <param name="afterTransport">Where to transport after the main act. (offset).</param>
        /// <param name="order">The order of movement on different axis. Axis not mentioned will happen in the background.</param>
        /// </summary>
        public async void Transport(Vector3 newDestination, float speed, Vector3 beforeTransport, Vector3 afterTransport, Action finishMethod = null, TransportOrderType order = TransportOrderType.XYZ)
        {
            destination = newDestination;
            this.speed = speed * 0.01f;
            moves.Clear();
            
            await Move(transform.position + beforeTransport, this.speed * 0.5f);

            switch (order)
            {
                case TransportOrderType.XYZ:
                    await MoveOnX();
                    await MoveOnY();
                    await MoveOnZ();
                    break;
                case TransportOrderType.XZY:
                    await MoveOnX();
                    await MoveOnZ();
                    await MoveOnY();
                    break;
                case TransportOrderType.YXZ:
                    await MoveOnY();
                    await MoveOnX();
                    await MoveOnZ();
                    break;
                case TransportOrderType.YZX:
                    await MoveOnY();
                    await MoveOnZ();
                    await MoveOnX();
                    break;
                case TransportOrderType.ZXY:
                    await MoveOnZ();
                    await MoveOnX();
                    await MoveOnY();
                    break;
                case TransportOrderType.ZYX:
                    await MoveOnZ();
                    await MoveOnY();
                    await MoveOnX();
                    break;
                case TransportOrderType.XY:
                    moves.Add(MoveOnZ());
                    await MoveOnX();
                    await MoveOnY();
                    break;
                case TransportOrderType.XZ:
                    moves.Add(MoveOnY());
                    await MoveOnX();
                    await MoveOnZ();
                    break;
                case TransportOrderType.YX:
                    moves.Add(MoveOnZ());
                    await MoveOnY();
                    await MoveOnX();
                    break;
                case TransportOrderType.YZ:
                    moves.Add(MoveOnX());
                    await MoveOnY();
                    await MoveOnZ();
                    break;
                case TransportOrderType.ZX:
                    moves.Add(MoveOnY());
                    await MoveOnZ();
                    await MoveOnX();
                    break;
                case TransportOrderType.ZY:
                    moves.Add(MoveOnX());
                    await MoveOnZ();
                    await MoveOnY();
                    break;
                case TransportOrderType.X:
                    moves.Add(MoveOnY());
                    moves.Add(MoveOnZ());
                    await MoveOnX();
                    break;
                case TransportOrderType.Y:
                    moves.Add(MoveOnX());
                    moves.Add(MoveOnZ());
                    await MoveOnY();
                    break;
                case TransportOrderType.Z:
                    moves.Add(MoveOnX());
                    moves.Add(MoveOnY());
                    await MoveOnZ();
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"The order type of '{order}' is unknown or unsupported.");
            }

            await Task.WhenAll(moves);

            await Move(transform.position + afterTransport, this.speed * 0.5f);
            
            finishMethod?.Invoke();
        }

        /// <summary>
        /// Moves an transform into a specific position simultaneously.
        /// </summary>
        /// <param name="targetPos">The position to transport to.</param>
        /// <param name="speed">The speed of the transport.</param>
        private async Task Move(Vector3 targetPos, float speed)
        {
            moves.Clear();
            moves.Add(MoveOnX(targetPos, speed));
            moves.Add(MoveOnY(targetPos, speed));
            moves.Add(MoveOnZ(targetPos, speed));
            await Task.WhenAll(moves);
            moves.Clear();
            await Task.CompletedTask;
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

        private async Task MoveOnZ() => await MoveOnZ(destination, speed);
        private async Task MoveOnZ(Vector3 targetPos, float speed)
        {
            while (!FloatUtils.AreEqual(transform.position.z, targetPos.z, 0.001f))
            {
                Vector3 position = transform.position;
                position = new Vector3(position.x, position.y, Mathf.MoveTowards(position.z, targetPos.z, speed));
                transform.position = position;
                await Task.Yield();
            }
        }
    }
}

using System;
using Cinemachine;
using UnityEngine;

namespace RedRats.Core
{
    /// <summary>
    /// Contains general helper methods.
    /// </summary>
    public static class RedRatUtils
    {
        /// <summary>
        /// Converts the <see cref="DirectionType"/> enum to a <see cref="Vector2"/> direction.
        /// </summary>
        /// <param name="direction">The direction type to convert.</param>
        /// <returns>A <see cref="Vector2"/> representation of that direction.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when the <see cref="DirectionType"/> is not supported.</exception>
        public static Vector2Int DirectionTypeToVector(DirectionType direction)
        {
            return direction switch
            {
                DirectionType.Up => Vector2Int.up,
                DirectionType.Down => Vector2Int.down,
                DirectionType.Right => Vector2Int.right,
                DirectionType.Left => Vector2Int.left,
                _ => throw new ArgumentOutOfRangeException($"The direction of type {direction} is not supported.")
            };
        }
        
        /// <summary>
        /// Returns the current active <see cref="CinemachineVirtualCamera"/>.
        /// </summary>
        public static CinemachineVirtualCamera GetActiveVCam()
        {
            CinemachineVirtualCamera vcam = (CinemachineVirtualCamera) CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera;
            return vcam;
        }

        /// <summary>
        /// Calculates the time it takes for a force to be applied to a <see cref="Rigidbody2D"/>.
        /// </summary>
        /// <param name="f">The force that is applied.</param>
        /// <param name="rb">The <see cref="Rigidbody2D"/> that the force is applied to.</param>
        /// <returns>Time of force in seconds.</returns>
        public static float GetTimeOfForce(float f, Rigidbody2D rb)
        {
            float d = (f * f) / (100 * rb.drag * rb.mass);
            return Mathf.Sqrt(2 * d / rb.mass); 
        }
        
    }
}
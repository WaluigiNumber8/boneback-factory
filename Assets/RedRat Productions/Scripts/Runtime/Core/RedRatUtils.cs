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
        public static Vector2 DirectionTypeToVector(DirectionType direction)
        {
            return direction switch
            {
                DirectionType.Up => Vector2.up,
                DirectionType.Down => Vector2.down,
                DirectionType.Right => Vector2.right,
                DirectionType.Left => Vector2.left,
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
        
    }
}
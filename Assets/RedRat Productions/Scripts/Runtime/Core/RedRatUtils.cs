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

        /// <summary>
        /// Evaluates a value based on an <see cref="AnimationCurve"/> whose x is 0-1.
        /// <br/> First remaps the value 0-1, evaluates by curve and remaps again to target range.
        /// </summary>
        /// <param name="value">The value to remap.</param>
        /// <param name="curve">The curve to remap by. (x must be 0-1).</param>
        /// <param name="firstMin">Minimum of range from which the value comes from.</param>
        /// <param name="firstMax">Maximum of range from which the value comes from.</param>
        /// <param name="secondMin">Minimum of the target range.</param>
        /// <param name="secondMax">Maximum of the target range.</param>
        /// <returns></returns>
        public static float RemapAndEvaluate(float value, AnimationCurve curve, float firstMin, float firstMax, float secondMin, float secondMax)
        {
            float firstValue = value.Remap(firstMin,firstMax, 0f, 1f);
            float curveValue = curve.Evaluate(firstValue);
            float targetValue = curveValue.Remap(0f, 1f, secondMin, secondMax);
            return targetValue;
        }
        
        /// <summary>
        /// Evaluates a value based on an <see cref="AnimationCurve"/> whose x is 0-1.
        /// <br/> First remaps the value 0-1, evaluates by curve and remaps again to target range.
        /// </summary>
        /// <param name="value">The value to remap.</param>
        /// <param name="curve">The curve to remap by. (x must be 0-1).</param>
        /// <param name="firstMin">Minimum of range from which the value comes from.</param>
        /// <param name="firstMax">Maximum of range from which the value comes from.</param>
        /// <param name="secondMin">Minimum of the target range.</param>
        /// <param name="secondMax">Maximum of the target range.</param>
        /// <returns></returns>
        public static int RemapAndEvaluate(int value, AnimationCurve curve, int firstMin, int firstMax, int secondMin, int secondMax)
        {
            float firstValue = ((float)value).Remap(firstMin,firstMax, 0f, 1f);
            float curveValue = curve.Evaluate(firstValue);
            int targetValue = (int)curveValue.Remap(0f, 1f, secondMin, secondMax);
            return targetValue;
        }
        
        /// <summary>
        /// Moves to a new position within the canvas.
        /// </summary>
        /// <param name="ttransform">The <see cref="RectTransform"/> to move.</param>
        /// <param name="targetPosition">The position to move the transform to.</param>
        /// <param name="canvasTransform">The <see cref="RectTransform"/> of the canvas.</param>
        /// <param name="cam">The camera if using screen space - camera</param>
        /// <returns>A new local position, that's within the canvas.</returns>
        public static Vector2 MoveToPositionWithinCanvas(RectTransform ttransform, Vector2 targetPosition, RectTransform canvasTransform, Camera cam = null)
        {
            (Vector2 minPos, Vector2 maxPos) = GetAllowedMinMaxPositions(ttransform, canvasTransform);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, targetPosition, cam, out Vector2 newPos);
            
            newPos.x -= ttransform.rect.width  * 0.5f;
            newPos.y -= ttransform.rect.height * 0.5f;
            
            newPos.x = Mathf.Clamp(newPos.x, minPos.x, maxPos.x);
            newPos.y = Mathf.Clamp(newPos.y, minPos.y, maxPos.y);
            return newPos;
        }
        
        /// <summary>
        /// Drags a <see cref="RectTransform"/> within the canvas.
        /// </summary>
        /// <param name="ttransform">The <see cref="RectTransform"/> to drag.</param>
        /// <param name="delta">The delta of the drag.</param>
        /// <param name="canvas">The canvas to drag in.</param>
        /// <param name="canvasTransform">The <see cref="RectTransform"/> of the canvas.</param>
        /// <returns></returns>
        public static Vector2 DragByWithinCanvas(RectTransform ttransform, Vector2 delta, Canvas canvas, RectTransform canvasTransform)
        {
            (Vector2 minPos, Vector2 maxPos) = GetAllowedMinMaxPositions(ttransform, canvasTransform);
            Vector2 newPos = ttransform.anchoredPosition + (delta / canvas.scaleFactor);
            
            newPos.x = Mathf.Clamp(newPos.x, minPos.x, maxPos.x);
            newPos.y = Mathf.Clamp(newPos.y, minPos.y, maxPos.y);
            return newPos;
        }
        
        /// <summary>
        /// Returns the allowed min and max positions for a <see cref="RectTransform"/> within a canvas.
        /// </summary>
        /// <param name="transform">The transform to find positions for.</param>
        /// <param name="canvasTransform">A bigger transform the previous one wants to stay in.</param>
        /// <returns>Min/max positions for both X & Y.</returns>
        private static (Vector2, Vector2) GetAllowedMinMaxPositions(RectTransform transform, RectTransform canvasTransform)
        {
            Vector2 minPos = canvasTransform.rect.min - transform.rect.min;
            Vector2 maxPos = canvasTransform.rect.max - transform.rect.max;
            return (minPos, maxPos);
        }
    }
}
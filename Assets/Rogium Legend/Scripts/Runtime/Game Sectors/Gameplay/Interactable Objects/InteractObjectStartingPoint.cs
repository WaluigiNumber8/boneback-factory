using System;
using Rogium.Editors.Core;
using Rogium.Editors.Objects;
using UnityEngine;

namespace Rogium.Gameplay.InteractableObjects
{
    /// <summary>
    /// Flags a position, where the player can start on room load.
    /// </summary>
    public class InteractObjectStartingPoint : MonoBehaviour, IInteractObject
    {
        public static event Action<Vector2, Vector2> OnConstruct;
        
        public void Construct(ObjectAsset data, ParameterInfo parameters)
        {
            DirectionType dir = (DirectionType) parameters.intValue1;
            Vector2 direction = dir switch
            {
                DirectionType.Up => Vector2.up,
                DirectionType.Down => Vector2.down,
                DirectionType.Right => Vector2.right,
                DirectionType.Left => Vector2.left,
                _ => throw new ArgumentOutOfRangeException($"Direction of type '{dir}' is not supported.")
            };
            
            OnConstruct?.Invoke(transform.position, direction);
        }
    }
}
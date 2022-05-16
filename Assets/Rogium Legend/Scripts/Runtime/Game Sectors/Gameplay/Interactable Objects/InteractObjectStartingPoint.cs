using System;
using RedRats.Core;
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
            Vector2 direction = RedRatUtils.DirectionTypeToVector((DirectionType) parameters.intValue1);
            OnConstruct?.Invoke(transform.position, direction);
        }
    }
}
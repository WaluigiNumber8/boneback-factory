using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// A base for all entity characteristics.
    /// </summary>
    public abstract class CharacteristicBase : MonoBehaviour
    {
        [SerializeField] protected EntityController entity;
    }
}
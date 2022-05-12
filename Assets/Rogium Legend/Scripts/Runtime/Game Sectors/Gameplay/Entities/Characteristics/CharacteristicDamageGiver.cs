using System;
using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Gives entities the ability to give out damage.
    /// </summary>
    public class CharacteristicDamageGiver : CharacteristicBase
    {
        public event Action OnGiveDamage;

        [SerializeField] private CharDamageGiverInfo defaultData;

        /// <summary>
        /// Constructs the characteristic.
        /// </summary>
        /// <param name="newInfo">New data to use.</param>
        public void Construct(CharDamageGiverInfo newInfo) => defaultData = newInfo;

        /// <summary>
        /// Rolls a new damage roll and returns it.
        /// </summary>
        /// <returns>Returns rolled damage.</returns>
        public int GetDamageTaken()
        {
            OnGiveDamage?.Invoke();
            return defaultData.baseDamage;
        }

        /// <summary>
        /// Apply knockback on receiver.
        /// </summary>
        /// <param name="other">The receiver of the knockback.</param>
        public void ReceiveKnockback(EntityController other)
        {
            entity.ForceMove(other.FaceDirection, defaultData.knockbackSelf);
            other.ForceMove(-other.FaceDirection, defaultData.knockbackOther);
        }
        
    }
}
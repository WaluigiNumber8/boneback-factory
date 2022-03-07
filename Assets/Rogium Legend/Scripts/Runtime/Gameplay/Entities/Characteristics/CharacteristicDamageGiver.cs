using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Gives entities the ability to give out damage.
    /// </summary>
    public class CharacteristicDamageGiver : MonoBehaviour
    {
        [SerializeField] private EntityController entity;
        [SerializeField] private CharDamageGiverInfo data;

        /// <summary>
        /// Constructs the characteristic.
        /// </summary>
        /// <param name="newInfo">New data to use.</param>
        public void Construct(CharDamageGiverInfo newInfo) => data = newInfo;

        /// <summary>
        /// Rolls a new damage roll and returns it.
        /// </summary>
        /// <returns>Returns rolled damage.</returns>
        public int GetDamageTaken()
        {
            return data.baseDamage;
        }

        /// <summary>
        /// Apply knockback on receiver.
        /// </summary>
        /// <param name="other">The receiver of the knockback.</param>
        public void ReceiveKnockback(EntityController other)
        {
            entity.ForceMove(other.FaceDirection, data.knockbackSelf);
            other.ForceMove(-other.FaceDirection, data.knockbackOther);
        }
        
    }
}
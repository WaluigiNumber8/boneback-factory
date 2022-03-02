using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Gives entities the ability to give out damage.
    /// </summary>
    public class CharacteristicDamageGiver : MonoBehaviour
    {
        [SerializeField] private EntityController entity;
        [SerializeField] private int baseDamage;
        [SerializeField] private ForcedMoveInfo knockbackSelf;
        [SerializeField] private ForcedMoveInfo knockbackOther;
        
        /// <summary>
        /// Rolls a new damage roll and returns it.
        /// </summary>
        /// <returns>Returns rolled damage.</returns>
        public int GetDamageTaken()
        {
            return baseDamage;
        }

        /// <summary>
        /// Apply knockback on receiver.
        /// </summary>
        /// <param name="other">The receiver of the knockback.</param>
        public void ReceiveKnockback(EntityController other)
        {
            entity.ForceMove(other.FaceDirection, knockbackSelf);
            other.ForceMove(-other.FaceDirection, knockbackOther);
        }
        
    }
}
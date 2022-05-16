using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Contains data of the <see cref="CharacteristicDamageReceiver"/> characteristic.
    /// </summary>
    [System.Serializable]
    public struct CharDamageReceiverInfo
    {
         public int maxHealth;
        [Range(0.05f, 2f)] public float invincibilityTime;

        public CharDamageReceiverInfo(int maxHealth, float invincibilityTime)
        {
            this.maxHealth = maxHealth;
            this.invincibilityTime = invincibilityTime;
        }
    }
}
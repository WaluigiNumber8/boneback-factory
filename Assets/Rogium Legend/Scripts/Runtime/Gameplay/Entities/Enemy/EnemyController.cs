using Rogium.Editors.Enemies;
using Rogium.Gameplay.Entities.Characteristics;
using UnityEngine;

namespace Rogium.Gameplay.Entities.Enemy
{
    /// <summary>
    /// Overseers and controls the enemy object.
    /// </summary>
    public class EnemyController : EntityController
    {
        [SerializeField] private CharacteristicDamageGiver damageGiver;
        
        /// <summary>
        /// Constructs the enemy.
        /// </summary>
        /// <param name="asset">The asset to take data from.</param>
        public void Construct(EnemyAsset asset)
        {
            
        }
        
    }
}
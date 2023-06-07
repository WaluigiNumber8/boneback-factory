using System.Collections.Generic;
using Rogium.Editors.Weapons;
using Rogium.Gameplay.Core;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Allows entities to shoot projectiles.
    /// </summary>
    public class CharacteristicProjectileShoot : CharacteristicBase
    {
        private ProjectileOverseerMono projectileOverseer;

        private void Awake() => projectileOverseer = ProjectileOverseerMono.GetInstance();

        /// <summary>
        /// Fires a projectile from this entity.
        /// </summary>
        /// <param name="data">The projectile that gets fired.</param>
        public void Fire(IList<ProjectileDataInfo> data) => projectileOverseer.Fire(entity.Transform, data);
    }
}
using System.Collections;
using System.Collections.Generic;
using BoubakProductions.Core;
using BoubakProductions.Safety;
using Rogium.Core;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Weapons;
using Rogium.Gameplay.Core;
using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Allows entities to shoot projectiles.
    /// </summary>
    public class CharacteristicProjectileShoot : CharacteristicBase
    {
        [SerializeField] private ProjectileController vessel;
        
        private IList<ProjectileAsset> allProjectiles;
        private ProjectileAsset lastProjectile;
        
        private void Start()
        {
            allProjectiles = GameplayOverseerMono.GetInstance().CurrentCampaign.DataPack.Projectiles;
        }

        public void Fire(IList<ProjectileDataInfo> data)
        {
            if (data == null || data.Count <= 0) return;
            foreach (ProjectileDataInfo projectileData in data)
            {
                StartCoroutine(FireProjectileCoroutine(projectileData));
            }

            IEnumerator FireProjectileCoroutine(ProjectileDataInfo projectileData)
            {
                yield return new WaitForSeconds(projectileData.SpawnDelay);

                ProjectileController copy = Instantiate(vessel, entity.transform.position, Quaternion.identity);
                TransformUtils.SetRotation2D(copy.transform, entity.FaceDirection, projectileData.AngleOffset);
                copy.gameObject.layer = entity.gameObject.layer;
                
                if (RefreshLastProjectile(projectileData.ID))
                    copy.Construct(lastProjectile);
                else copy.ConstructMissing();
            }
        }

        /// <summary>
        /// Refreshes the last projectile.
        /// </summary>
        /// <param name="id">The ID to refresh the projectile with.</param>
        /// <returns>TRUE if projectile was successfully refreshed,</returns>
        private bool RefreshLastProjectile(string id)
        {
            if (lastProjectile != null && lastProjectile.ID == id) return true;
            try
            {
                lastProjectile = allProjectiles.FindValueFirst(id);
                return true;
            }
            catch (SafetyNetCollectionException) {return false;}
        }
    }
}
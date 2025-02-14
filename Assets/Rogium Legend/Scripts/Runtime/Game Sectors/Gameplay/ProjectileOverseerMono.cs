using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Core;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Weapons;
using Rogium.Gameplay.Entities;
using UnityEngine;
using UnityEngine.Pool;

namespace Rogium.Gameplay.Core
{
    /// <summary>
    /// Overseers all spawned projectiles in the game.
    /// </summary>
    public class ProjectileOverseerMono : MonoSingleton<ProjectileOverseerMono>
    {
        [SerializeField] private ProjectileController vessel;

        private IDictionary<string, ProjectileAsset> allProjectiles;
        private ProjectileAsset lastProjectile;
        private ObjectPool<ProjectileController> pool;

        private void Start()
        {
            allProjectiles = GameplayOverseerMono.GetInstance().CurrentCampaign.DataPack.Projectiles.ToDictionary(p => p.ID);
            pool = new ObjectPool<ProjectileController>(
                () =>
                {
                    ProjectileController controller = Instantiate(vessel);
                    controller.OnDie += () => pool.Release(controller);
                    return controller;
                },
                null,
                p => p.gameObject.SetActive(false),
                Destroy,
                true, 200
            );
        }

        /// <summary>
        /// Fire a projectile.
        /// </summary>
        /// <param name="entity">The entity that fired the projectile.</param>
        /// <param name="data">The projectile to fire.</param>
        public void Fire(Transform entity, IList<ProjectileDataInfo> data)
        {
            if (data == null || data.Count <= 0) return;
            
            foreach (ProjectileDataInfo projectileData in data)
            {
                StartCoroutine(FireProjectileCoroutine(projectileData));
            }

            IEnumerator FireProjectileCoroutine(ProjectileDataInfo projectileData)
            {
                yield return new WaitForSeconds(projectileData.SpawnDelay);

                ProjectileController p = pool.Get();
                p.transform.SetPositionAndRotation(entity.position, entity.rotation);
                p.transform.eulerAngles += Vector3.forward * projectileData.AngleOffset;
                p.gameObject.layer = entity.gameObject.layer;

                if (UseExistingProjectile(projectileData.ID))
                    p.Construct(lastProjectile);
                else p.ConstructMissing();
                 
                p.gameObject.SetActive(true);
            }
        }

        public void ClearAllProjectiles() => pool.Clear();

        /// <summary>
        /// Refreshes the last projectile.
        /// </summary>
        /// <param name="id">The ID to refresh the projectile with.</param>
        /// <returns>TRUE if projectile was successfully refreshed,</returns>
        private bool UseExistingProjectile(string id)
        {
            if (lastProjectile != null && lastProjectile.ID == id) return true;
            try
            {
                lastProjectile = allProjectiles[id];
                return true;
            }
            catch (PreconditionCollectionException)
            {
                return false;
            }
        }
    }
}
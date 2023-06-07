using System.Collections;
using System.Collections.Generic;
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

        private IList<ProjectileAsset> allProjectiles;
        private ProjectileAsset lastProjectile;
        private ObjectPool<ProjectileController> pool;
        private Transform currentEntity;

        private void Start()
        {
            allProjectiles = GameplayOverseerMono.GetInstance().CurrentCampaign.DataPack.Projectiles;
            pool = new ObjectPool<ProjectileController>(
                () =>
                {
                    ProjectileController controller = Instantiate(vessel, currentEntity.position, currentEntity.rotation);
                    controller.OnDie += () => pool.Release(controller);
                    return controller;
                },
                p =>
                {
                    p.Transform.SetPositionAndRotation(currentEntity.position, currentEntity.rotation);
                    p.gameObject.SetActive(true);
                },
                p => p.gameObject.SetActive(false),
                Destroy,
                true, 30
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
            
            currentEntity = entity;
            foreach (ProjectileDataInfo projectileData in data)
            {
                StartCoroutine(FireProjectileCoroutine(projectileData));
            }

            IEnumerator FireProjectileCoroutine(ProjectileDataInfo projectileData)
            {
                yield return new WaitForSeconds(projectileData.SpawnDelay);

                ProjectileController p = pool.Get();
                p.transform.eulerAngles += Vector3.forward * projectileData.AngleOffset;
                p.gameObject.layer = entity.gameObject.layer;

                if (UseExistingProjectile(projectileData.ID))
                    p.Construct(lastProjectile);
                else p.ConstructMissing();
            }
        }

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
                lastProjectile = allProjectiles.FindValueFirst(id);
                return true;
            }
            catch (SafetyNetCollectionException)
            {
                return false;
            }
        }
    }
}
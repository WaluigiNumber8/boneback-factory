using System.Collections;
using RedRats.Systems.LiteFeel.Core;
using Rogium.Editors.Weapons;
using Rogium.Gameplay.Entities.Enemy;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Systems.LiteFeel.Brains
{
    /// <summary>
    /// Allocates LF effects to an enemy.
    /// </summary>
    public class LFBrainEnemy : MonoBehaviour
    {
        [SerializeField, Required, GUIColor(0.85f, 0.8f, 0f)] private EnemyController enemy;
        [Space] 
        [SerializeField, GUIColor(1f, 0.25f, 0f)] private LFEffector onHitEffector;
        [SerializeField, GUIColor(1f, 0.25f, 0f)] private LFEffector onDeathEffector;
        [SerializeField, ChildGameObjectsOnly, GUIColor(1f, 0.5f, 0f)] private LFEffector onGetItemEffector;
        [Space] 
        [SerializeField] private DamageParticleSettingsInfo hitSettings;
        [SerializeField] private NewItemGetSettingsInfo newItemGetSettings;
        
        private void OnEnable()
        {
            if (onHitEffector != null) enemy.DamageReceiver.OnHit += WhenHit;
            if (onDeathEffector != null) enemy.DamageReceiver.OnDeath += onDeathEffector.Play;
            if (onGetItemEffector != null) enemy.WeaponHold.OnGetNewWeapon += WhenGetItem;
        }

        private void OnDisable()
        {
            if (onHitEffector != null) enemy.DamageReceiver.OnHit -= WhenHit;
            if (onDeathEffector != null) enemy.DamageReceiver.OnDeath -= onDeathEffector.Play;
            if (onGetItemEffector != null) enemy.WeaponHold.OnGetNewWeapon -= WhenGetItem;
        }
        
        private void WhenHit(int damage, Vector3 hitDirection)
        {
            if (damage <= 0) return;
            
            int amount = hitSettings.GetAmountOfParticles(damage);
            hitSettings.particleEffect.UpdateBurstAmount(0, amount);
            hitSettings.particleEffect.UpdateColor(enemy.RepresentativeColor);
            if (hitSettings.isDirectional) hitSettings.particleEffect.UpdateRotationOffset(hitDirection);
            onHitEffector.Play();
        }
        
        private void WhenGetItem(WeaponAsset weapon)
        {
            newItemGetSettings.collectedItem.gameObject.SetActive(true);
            newItemGetSettings.UpdateSprite(weapon.Icon);
            onGetItemEffector.Play();
            
            StartCoroutine(DelayCoroutine());
            IEnumerator DelayCoroutine()
            {
                yield return new WaitForSecondsRealtime(newItemGetSettings.hideDelay);
                newItemGetSettings.collectedItem.gameObject.SetActive(false);
            }
        }
    }
}
using System;
using System.Collections;
using RedRats.Core;
using RedRats.Systems.LiteFeel.Core;
using Rogium.Editors.Weapons;
using Rogium.Gameplay.Entities.Player;
using Rogium.Gameplay.InteractableObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Systems.LiteFeel.Brains
{
    /// <summary>
    /// Allocates LF effects to the player.
    /// </summary>
    public class LFBrainPlayer : MonoBehaviour
    {
        [SerializeField, Required, GUIColor(0.85f, 0.8f, 0f)] private PlayerController player;
        [Space] 
        [SerializeField, ChildGameObjectsOnly, GUIColor(0.15f, 0.7f, 1f)] private LFEffector onTurnEffector;
        [SerializeField, ChildGameObjectsOnly, GUIColor(0.15f, 0.7f, 1f)] private LFEffector onHitEffector;
        [SerializeField, ChildGameObjectsOnly, GUIColor(0.15f, 0.7f, 1f)] private LFEffector onDeathEffector;
        [SerializeField, ChildGameObjectsOnly, GUIColor(1f, 0.5f, 0f)] private LFEffector onGetItemEffector;
        [SerializeField, ChildGameObjectsOnly, GUIColor(1f, 0.5f, 0f)] private LFEffector onGetItemFinishEffector;
        [Space]
        [SerializeField] private DamageParticleSettingsInfo hitSettings;
        [SerializeField] private NewItemGetSettingsInfo newItemGetSettings;

        private void OnEnable()
        {
            if (onTurnEffector != null) player.OnTurn += onTurnEffector.Play;
            if (onHitEffector != null) player.DamageReceiver.OnHit += WhenHit;
            if (onDeathEffector != null) player.OnDeath += onDeathEffector.Play;
            if (onGetItemEffector != null) InteractObjectWeaponDrop.OnPlayerPickUp += WhenGetItem;
            if (onGetItemFinishEffector != null) player.WeaponHold.OnEquipWeapon += WhenGetItemFinish;
        }

        private void OnDisable()
        {
            if (onTurnEffector != null) player.OnTurn -= onTurnEffector.Play;
            if (onHitEffector != null) player.DamageReceiver.OnHit -= WhenHit;
            if (onDeathEffector != null) player.OnDeath -= onDeathEffector.Play;
            if (onGetItemEffector != null) InteractObjectWeaponDrop.OnPlayerPickUp -= WhenGetItem;
            if (onGetItemFinishEffector != null) player.WeaponHold.OnEquipWeapon -= WhenGetItemFinish;
        }
        
        private void WhenHit(int damage, Vector3 hitDirection)
        {
            if (damage <= 0) return;
                
            int amount = RedRatUtils.RemapAndEvaluate(damage, hitSettings.amountCurve, hitSettings.minDamage, hitSettings.maxDamage, hitSettings.minAmount, hitSettings.maxAmount);
            hitSettings.particleEffect.UpdateBurstAmount(0, amount);
            if (hitSettings.isDirectional) hitSettings.particleEffect.UpdateRotationOffset(-hitDirection);
            onHitEffector.Play();
        }
        
        private void WhenGetItem(WeaponAsset weapon)
        {
            newItemGetSettings.collectedItem.gameObject.SetActive(true);
            newItemGetSettings.UpdateSprite(weapon.Icon);
            onGetItemEffector.Play();
        }
        
        private void WhenGetItemFinish(WeaponAsset weapon)
        {
            onGetItemFinishEffector.Play();
            StartCoroutine(DelayCoroutine());
            IEnumerator DelayCoroutine()
            {
                yield return new WaitForSecondsRealtime(newItemGetSettings.hideDelay);
                newItemGetSettings.collectedItem.gameObject.SetActive(false);
            }
        }

    }
}
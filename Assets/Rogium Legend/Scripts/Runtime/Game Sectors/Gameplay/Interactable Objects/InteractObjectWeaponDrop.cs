using System;
using BoubakProductions.Safety;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Objects;
using Rogium.Editors.Weapons;
using Rogium.Gameplay.Core;
using Rogium.Gameplay.Entities.Characteristics;
using Rogium.Gameplay.Entities.Player;
using UnityEngine;

namespace Rogium.Gameplay.InteractableObjects
{
    /// <summary>
    /// Holds a weapon, that the player will pickup upon touch.
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class InteractObjectWeaponDrop : MonoBehaviour, IInteractObject
    {
        public static event Action<WeaponAsset> OnPlayerPickUp;

        [SerializeField] private SpriteRenderer iconRenderer;
        
        private WeaponAsset weapon;
        private bool playerOnly;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out CharacteristicWeaponHold weaponHolder)) return;
            if (other.TryGetComponent(out PlayerController _))
            {
                OnPlayerPickUp?.Invoke(weapon);
                DestroyItem();
                return;
            }

            if (playerOnly) return;
            weaponHolder.Add(weapon);
            DestroyItem();
        }

        public void Construct(ObjectAsset data, ParameterInfo parameters)
        {
            try
            {
                weapon = GameplayOverseerMono.GetInstance().CurrentCampaign.DataPack.Weapons.FindValueFirstOrReturnFirst(parameters.stringValue1);
                iconRenderer.sprite = weapon.Icon;
                playerOnly = parameters.boolValue1;
            }
            catch (Exception e)
            {
                if (e is SafetyNetException or SafetyNetCollectionException) Destroy(gameObject);
            }

        }

        /// <summary>
        /// Destroys the item.
        /// </summary>
        private void DestroyItem()
        {
            Destroy(gameObject);
        }
        
    }
}
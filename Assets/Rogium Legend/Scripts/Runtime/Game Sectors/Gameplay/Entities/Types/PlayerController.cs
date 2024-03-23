using System;
using Rogium.Gameplay.Entities.Characteristics;
using Rogium.Systems.Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Gameplay.Entities.Player
{
    /// <summary>
    /// The main controller of the player.
    /// </summary>
    public class PlayerController : EntityController
    {
        public event Action OnDeath; 

        [Title("Characteristics")]
        [SerializeField] private CharacteristicMove movement;
        [SerializeField] private CharacteristicDamageReceiver damageReceiver;
        [SerializeField] private CharacteristicVisualPlayer visual;
        [SerializeField] private CharacteristicWeaponHold weaponHold;
        [SerializeField] private CharacteristicFloorInteractor floorInteractor;

        private Vector2 moveDirection;
        private InputProfilePlayer input;

        protected override void Awake()
        {
            base.Awake();
            input = InputSystem.GetInstance().Player;
        }

        private void OnEnable()
        {
            input.Movement.OnPressed += DoMovement;
            input.Movement.OnReleased += DoMovement;
            
            input.ButtonMain.OnPress += UseWeaponMain;
            input.ButtonMainAlt.OnPress += UseWeaponMainAlt;
            input.ButtonSub.OnPress += UseWeaponSub;
            input.ButtonSubAlt.OnPress += UseWeaponSubAlt;
            input.ButtonDash.OnPress += UseWeaponDash;
            input.ButtonDashAlt.OnPress += UseWeaponDashAlt;
            
            damageReceiver.OnDeath += Die;
        }

        private void OnDisable()
        {
            input.Movement.OnPressed -= DoMovement;
            input.Movement.OnReleased -= DoMovement;
            
            input.ButtonMain.OnPress -= UseWeaponMain;
            input.ButtonMainAlt.OnPress -= UseWeaponMainAlt;
            input.ButtonSub.OnPress -= UseWeaponSub;
            input.ButtonSubAlt.OnPress -= UseWeaponSubAlt;
            input.ButtonDash.OnPress -= UseWeaponDash;
            input.ButtonDashAlt.OnPress -= UseWeaponDashAlt;
            
            damageReceiver.OnDeath -= Die;
        }

        protected void FixedUpdate()
        {
            if (movementLocked) return;
            if (actionsLocked) return;
            movement.Move(moveDirection);
        }

        public void BecomeInvincible(float time) => damageReceiver.BecomeInvincible(time);
        
        protected override void UpdateFaceDirection()
        {
            if (movementLocked) return;
            if (actionsLocked || moveDirection == Vector2.zero)
            {
                base.UpdateFaceDirection();
            }
            else faceDirection = moveDirection;
        }

        /// <summary>
        /// Allow the player to move in a specific direction.
        /// </summary>
        /// <param name="inputDir">The input of the player.</param>
        private void DoMovement(Vector2 inputDir) => moveDirection = inputDir.normalized;

        /// <summary>
        /// Is run when the player dies.
        /// </summary>
        private void Die()
        {
            ChangeCollideMode(false);
            actionsLocked = true;
            movementLocked = true;
            visual.PlayDeath();
            OnDeath?.Invoke();
        }

        private void UseWeaponMain() => weaponHold.Use(0);
        private void UseWeaponMainAlt() => weaponHold.Use(1);
        private void UseWeaponSub() => weaponHold.Use(2);
        private void UseWeaponSubAlt() => weaponHold.Use(3);
        private void UseWeaponDash() => weaponHold.Use(4);
        private void UseWeaponDashAlt() => weaponHold.Use(5);

    }
}
using Rogium.Gameplay.Entities.Characteristics;
using Rogium.Systems.Input;
using UnityEngine;

namespace Rogium.Gameplay.Entities.Player
{
    /// <summary>
    /// The main controller of the player.
    /// </summary>
    public class PlayerController : EntityController
    {
        [SerializeField] private CharacteristicMove movement;
        [SerializeField] private CharacteristicDamageReceiver damageReceiver;
        [SerializeField] private CharacteristicWeaponHold weaponHold;
        
        private Vector2 moveDirection;
        private bool disableMovement;
        private InputProfilePlayer input;

        protected override void Awake()
        {
            base.Awake();
            input = InputSystem.Instance.Player;
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

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            
            if (actionsLocked) return;
            movement.Move(moveDirection);
            
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
            print("Player has died!");
        }

        private void UseWeaponMain() => weaponHold.Use(0);
        private void UseWeaponMainAlt() => weaponHold.Use(1);
        private void UseWeaponSub() => weaponHold.Use(2);
        private void UseWeaponSubAlt() => weaponHold.Use(3);
        private void UseWeaponDash() => weaponHold.Use(4);
        private void UseWeaponDashAlt() => weaponHold.Use(5);

    }
}
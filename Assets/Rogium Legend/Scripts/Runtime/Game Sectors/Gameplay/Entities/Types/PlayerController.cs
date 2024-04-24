using System;
using RedRats.Core;
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
        public event Action OnTurn;

        [Title("Characteristics")]
        [SerializeField] private CharacteristicMove movement;
        [SerializeField] private CharacteristicDamageReceiver damageReceiver;
        [SerializeField] private CharacteristicVisualPlayer visual;
        [SerializeField] private CharacteristicWeaponHold weaponHold;
        [SerializeField] private CharacteristicFloorInteractor floorInteractor;
        [Title("Special")] 
        [SerializeField] private ParticleSystem deathSkullEffect;
        
        private InputProfilePlayer input;
        
        private Vector2 moveDirection;
        private Vector2Int lastFaceDirection;

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

        protected override void Update()
        {
            base.Update();
            
            // if (Time.frameCount % 3 != 0) return;
            DetectTurning();
        }

        protected void FixedUpdate()
        {
            if (movementLocked) return;
            if (actionsLocked) return;
            movement.Move(moveDirection);
        }

        public void BecomeInvincible(float time) => damageReceiver.BecomeInvincible(time);
        
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
            if (deathSkullEffect != null) Instantiate(deathSkullEffect, transform.position, Quaternion.identity);
        }

        private void UseWeaponMain() => weaponHold.Use(0);
        private void UseWeaponMainAlt() => weaponHold.Use(1);
        private void UseWeaponSub() => weaponHold.Use(2);
        private void UseWeaponSubAlt() => weaponHold.Use(3);
        private void UseWeaponDash() => weaponHold.Use(4);
        private void UseWeaponDashAlt() => weaponHold.Use(5);

        /// <summary>
        /// Checks if the player turned and if so, fires the <see cref="OnTurn"/> event.
        /// </summary>
        private void DetectTurning()
        {
            int faceDirX = faceDirection.x.Round().Sign0();
            int faceDirY = faceDirection.y.Round().Sign0();
            int oldDirX = lastFaceDirection.x.Sign0();
            int oldDirY = lastFaceDirection.y.Sign0();
            
            //Check if player turned to a different direction
            if ((faceDirX == -oldDirX || (oldDirX == 0 && faceDirX != oldDirX)) && faceDirX != 0 ||
                (faceDirY == -oldDirY || (oldDirY == 0 && faceDirY != oldDirY)) && faceDirY != 0)
            {
                OnTurn?.Invoke();
            }
            
            lastFaceDirection = new Vector2Int(faceDirX, faceDirY);
        }
        
        public CharacteristicDamageReceiver DamageReceiver { get => damageReceiver; }
    }
}
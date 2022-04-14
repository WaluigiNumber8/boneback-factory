using System;
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
        [SerializeField] private ForcedMoveInfo dash;
        [SerializeField] private CharacteristicMove movement;
        [SerializeField] private CharacteristicDamageReceiver damageReceiver;
        
        private Vector2 moveDirection;
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
            input.ButtonDash.OnPress += InitiateDash;
            damageReceiver.OnDeath += Die;
        }

        private void OnDisable()
        {
            input.Movement.OnPressed -= DoMovement;
            input.Movement.OnReleased -= DoMovement;
            input.ButtonDash.OnPress -= InitiateDash;
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
        private void DoMovement(Vector2 inputDir)
        {
            moveDirection = inputDir.normalized;
            // faceDirection = (inputDir != Vector2.zero) ? inputDir : faceDirection;
        }

        /// <summary>
        /// Begins the player dash.
        /// </summary>
        private void InitiateDash()
        {
            if (actionsLocked) return;
            ForceMove(faceDirection, dash);
        }
        
        /// <summary>
        /// Is run when the player dies.
        /// </summary>
        private void Die()
        {
            print("Player has died!");
        }
        
    }
}
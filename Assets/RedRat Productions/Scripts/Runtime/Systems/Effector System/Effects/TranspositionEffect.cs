using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.Effectors.Effects
{
    /// <summary>
    /// Allows moving a specific target.
    /// </summary>
    public class TranspositionEffect : EffectBase
    {
        [Header("Target")]
        [SerializeField] private Transform target;
        [SerializeField] private float duration = 0.2f;
        [SerializeField] private bool restartOnReplay = true;
        [SerializeField] private bool smoothReset;
        
        [Header("Transposition")]
        [SerializeField] private Vector3 endPosition;
        [SerializeField] private Ease easing = Ease.InOutSine;
        [SerializeField] private MovementType movement = MovementType.Relative;
        [SerializeField] private WorldType worldType = WorldType.World;
        
        [Header("Looping")] 
        [SerializeField] private bool infiniteLoop;
        [SerializeField] private int loops;
        [SerializeField] private LoopType loopType = LoopType.Restart;

        private Vector3 startPosition;

        private void Awake() => startPosition = target.position;

        protected override void PlaySelf()
        {
            if (restartOnReplay) target.DOKill();
            int loopAmount = (infiniteLoop) ? -1 : loops;
            TweenMove(endPosition, duration, loopAmount, movement, worldType);
        }

        protected override void StopSelf()
        {
            target.DOKill();
            if (smoothReset)
            {
                TweenMove(startPosition, duration * 0.5f);
                return;
            }

            target.position = startPosition;
        }

        /// <summary>
        /// Tween movement for the target.
        /// </summary>
        /// <param name="targetPosition">The position the target has to reach.</param>
        /// <param name="duration">How long will the tween last (in seconds).</param>
        /// <param name="loopAmount">How many times to loop the tween (-1 for infinite).</param>
        /// <param name="movementType">Is the movement done in absolute or relative coordinates.</param>
        /// <param name="worldType">Does the movement happen in world space or relative to parent.</param>
        private void TweenMove(Vector3 targetPosition, float duration, int loopAmount = 0,
            MovementType movementType = MovementType.Absolute, WorldType worldType = WorldType.World)
        {
            Vector3 targetPos = movementType switch
            {
                MovementType.Relative => startPosition + target.position,
                MovementType.Absolute => targetPosition,
                _ => throw new ArgumentOutOfRangeException(nameof(movementType), movementType, null)
            };

            switch (worldType)
            {
                case WorldType.World:
                    target.DOMove(targetPos, duration).SetEase(easing).SetLoops(loopAmount, loopType)
                        .OnComplete(StopSelf);
                    break;
                case WorldType.Local:
                    target.DOLocalMove(targetPos, duration).SetEase(easing).SetLoops(loopAmount, loopType)
                        .OnComplete(StopSelf);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(worldType), worldType, null);
            }
        }
    }
}
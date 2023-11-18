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
        /// <summary>
        /// The target to move.
        /// </summary>
        [Tooltip("The target to move.")] 
        [Header("Target")]
        [SerializeField] private Transform target;
        /// <summary>
        /// How long will the movement last.
        /// </summary>
        [Tooltip("How long will the movement last.")] 
        [SerializeField] private float duration = 0.2f;
        /// <summary>
        /// Restart the movement from beginning when replaying.
        /// </summary>
        [Tooltip("Restart the movement from beginning when replaying.")] 
        [SerializeField] private bool restartOnReplay = true;
        /// <summary>
        /// Will the target move back to it's original position smoothly when stopped?
        /// </summary>
        [Tooltip("Will the target move back to it's original position smoothly when stopped?")] 
        [SerializeField] private bool smoothReset;

        /// <summary>
        /// The position the target has to reach.
        /// </summary>
        [Tooltip("The position the target has to reach.")] 
        [Header("Transposition")]
        [SerializeField] private Vector3 endPosition;
        /// <summary>
        /// The easing to use for the movement.
        /// </summary>
        [Tooltip("The easing to use for the movement.")] 
        [SerializeField] private Ease easing = Ease.InOutSine;
        /// <summary>
        /// Is the movement going to happen in absolute or relative coordinates?
        /// </summary>
        [Tooltip("Is the movement going to happen in absolute or relative coordinates?")] 
        [SerializeField] private MovementType movement = MovementType.Relative;
        /// <summary>
        /// is the movement happening in world space or space relative to parent?
        /// </summary>
        [Tooltip("is the movement happening in world space or space relative to parent?")] 
        [SerializeField] private WorldType worldType = WorldType.World;

        /// <summary>
        /// Loop the movement infinitely.
        /// </summary>
        [Tooltip("Loop the movement infinitely.")] 
        [Header("Looping")] 
        [SerializeField] private bool infiniteLoop;
        /// <summary>
        /// How many times to loop the movement.
        /// </summary>
        [Tooltip("How many times to loop the movement.")] [SerializeField, HideIf("infiniteLoop")]
        private int loops;
        /// <summary>
        /// The type of looping to use. RESTART moves target back after each sequence. YOYO moves target back and forth. INCREMENTAL starts the next sequence where the previous one ended.
        /// </summary>
        [Tooltip("The type of looping to use. RESTART moves target back after each sequence. YOYO moves target back and forth. INCREMENTAL starts the next sequence where the previous one ended.")]
        [SerializeField]
        private LoopType loopType = LoopType.Restart;

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
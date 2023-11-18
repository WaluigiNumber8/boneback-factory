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
        [SerializeField] private SmoothingType smoothing = SmoothingType.Tween;
        [SerializeField, HideIf("smoothing", SmoothingType.AnimationCurve)] private Ease easing = Ease.InOutSine;
        [SerializeField, HideIf("smoothing", SmoothingType.Tween)] private AnimationCurve movementCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        [SerializeField, EnumToggleButtons] private MovementType movement = MovementType.Relative;
        [SerializeField, EnumToggleButtons] private WorldType worldType = WorldType.World;
        
        [Header("Looping")] 
        [SerializeField] private bool infiniteLoop;
        [SerializeField, HideIf("infiniteLoop")] private int loops;
        [SerializeField] private LoopType loopType = LoopType.Restart;

        private Vector3 startPosition;
        private Vector3 startLocalPosition;

        private void Awake()
        {
            startPosition = target.position;
            startLocalPosition = target.localPosition;
        }

        protected override void PlaySelf()
        {
            target.DOKill();
            if (restartOnReplay) ResetTargetPosition();
            int loopAmount = (infiniteLoop) ? -1 : loops;
            TweenMove(endPosition, duration, loopAmount, smoothing, movement, worldType);
        }

        protected override void StopSelf()
        {
            target.DOKill();
            if (smoothReset)
            {
                TweenMove(GetStartingPosition(), duration * 0.5f);
                return;
            }
            ResetTargetPosition();
        }

        /// <summary>
        /// Tween movement for the target.
        /// </summary>
        /// <param name="targetPosition">The position the target has to reach.</param>
        /// <param name="duration">How long will the tween last (in seconds).</param>
        /// <param name="loopAmount">How many times to loop the tween (-1 for infinite).</param>
        /// <param name="movementType">Is the movement done in absolute or relative coordinates.</param>
        /// <param name="worldType">Does the movement happen in world space or relative to parent.</param>
        private void TweenMove(Vector3 targetPosition, float duration, int loopAmount = 0, SmoothingType smoothingType = SmoothingType.Tween,
            MovementType movementType = MovementType.Absolute, WorldType worldType = WorldType.World)
        {
            Vector3 targetPos = (movementType == MovementType.Relative) ? GetPosition() + targetPosition : targetPosition;
            var tween = (worldType == WorldType.World) ? target.DOMove(targetPos, duration) : target.DOLocalMove(targetPos, duration);
            var easedTween = (smoothingType == SmoothingType.Tween) ? tween.SetEase(easing) : tween.SetEase(movementCurve);
            easedTween.SetLoops(loopAmount, loopType);
            easedTween.OnComplete(StopSelf);
        }
        
        /// <summary>
        /// Resets the position of the target.
        /// </summary>
        private void ResetTargetPosition()
        {
            target.position = startPosition;
            target.localPosition = startLocalPosition;
        }
        
        /// <summary>
        /// Returns the starting position of the target, depending on the world type.
        /// </summary>
        private Vector3 GetStartingPosition() => (worldType == WorldType.World) ? startPosition : startLocalPosition;
        /// <summary>
        /// Returns the current position of the target, depending on the world type.
        /// </summary>
        private Vector3 GetPosition() => (worldType == WorldType.World) ? target.position : target.localPosition;
    }
}
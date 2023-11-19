using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// Allows moving a specific target.
    /// </summary>
    public class LFTranspositionEffect : LFEffectBase
    {
        [Header("Target")]
        [SerializeField] private Transform target;
        [SerializeField] private float duration = 0.2f;
        [SerializeField] private bool additivePlay;
        [SerializeField] private bool resetOnEnd;
        [SerializeField] private bool smoothReset;

        [Header("Transposition")] 
        [SerializeField] private TransitionType mode = TransitionType.AtoB;
        [SerializeField, HideIf("mode", TransitionType.ToDestination)] private Vector3 startPosition;
        [SerializeField] private Vector3 endPosition;
        [SerializeField] private SmoothingType smoothing = SmoothingType.Tween;
        [SerializeField, HideIf("smoothing", SmoothingType.AnimationCurve)] private Ease easing = Ease.InOutSine;
        [SerializeField, HideIf("smoothing", SmoothingType.Tween)] private AnimationCurve movementCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        [SerializeField, EnumToggleButtons] private MovementType movement = MovementType.Relative;
        [SerializeField, EnumToggleButtons] private WorldType worldType = WorldType.World;
        
        [Header("Looping")] 
        [SerializeField] private bool infiniteLoop;
        [SerializeField, Min(1), HideIf("infiniteLoop")] private int loops;
        [SerializeField] private LoopType loopType = LoopType.Restart;

        private Vector3 startPos;
        private Vector3 startLocalPos;
        private Tween tween;

        private void Awake()
        {
            startPos = (mode == TransitionType.AtoB) ? ((movement == MovementType.Relative) ? target.position + startPosition : startPosition) : target.position;
            startLocalPos = (mode == TransitionType.AtoB) ? ((movement == MovementType.Relative) ? target.localPosition + startPosition : startPosition) : target.localPosition;
        }

        protected override void PlaySelf()
        {
            tween.Kill();
            if (!additivePlay) ResetTargetPosition();
            int loopAmount = (infiniteLoop) ? -1 : loops;
            TweenMove(endPosition, duration, loopAmount, smoothing, movement, worldType);
        }

        protected override void StopSelf()
        {
            tween.Kill();
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
            tween = (worldType == WorldType.World) ? target.DOMove(targetPos, duration) : target.DOLocalMove(targetPos, duration);
            tween = (smoothingType == SmoothingType.Tween) ? tween.SetEase(easing) : tween.SetEase(movementCurve);
            tween.SetLoops(loopAmount, loopType);
            if (resetOnEnd) tween.OnComplete(StopSelf);
        }
        
        /// <summary>
        /// Resets the position of the target.
        /// </summary>
        private void ResetTargetPosition()
        {
            target.position = startPos;
            target.localPosition = startLocalPos;
        }
        
        /// <summary>
        /// Returns the starting position of the target, depending on the world type.
        /// </summary>
        private Vector3 GetStartingPosition() => (worldType == WorldType.World) ? startPos : startLocalPos;
        /// <summary>
        /// Returns the current position of the target, depending on the world type.
        /// </summary>
        private Vector3 GetPosition() => (worldType == WorldType.World) ? target.position : target.localPosition;
    }
}
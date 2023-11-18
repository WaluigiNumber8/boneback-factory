using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.Effectors.Effects
{
    /// <summary>
    /// Allows to rotate a specific target.
    /// </summary>
    public class RotationEffect : EffectBase
    {
        [Header("Target")]
        [SerializeField] private Transform target;
        [SerializeField] private float duration = 0.2f;
        [SerializeField] private bool restartOnReplay = true;
        [SerializeField] private bool smoothReset;

        [Header("Rotation")] 
        [SerializeField] private TransitionType mode = TransitionType.AtoB;
        [SerializeField, HideIf("mode", TransitionType.ToDestination)] private Vector3 startRotation;
        [SerializeField] private Vector3 endRotation;
        [SerializeField] private SmoothingType smoothing = SmoothingType.Tween;
        [SerializeField, HideIf("smoothing", SmoothingType.AnimationCurve)] private Ease easing = Ease.InOutSine;
        [SerializeField, HideIf("smoothing", SmoothingType.Tween)] private AnimationCurve movementCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        [SerializeField, EnumToggleButtons] private MovementType movement = MovementType.Relative;
        [SerializeField, EnumToggleButtons] private WorldType worldType = WorldType.World;
        
        [Header("Looping")] 
        [SerializeField] private bool infiniteLoop;
        [SerializeField, Min(1), HideIf("infiniteLoop")] private int loops;
        [SerializeField] private LoopType loopType = LoopType.Restart;

        private Vector3 startRot;
        private Vector3 startLocalRot;
        private Tween tween;

        private void Awake()
        {
            startRot = (mode == TransitionType.AtoB) ? ((movement == MovementType.Relative) ? target.eulerAngles + startRotation : startRotation) : target.eulerAngles;
            startLocalRot = (mode == TransitionType.AtoB) ? ((movement == MovementType.Relative) ? target.localEulerAngles + startRotation : startRotation) : target.localEulerAngles;
        }
        
        protected override void PlaySelf()
        {
            tween.Kill();
            if (restartOnReplay) ResetTargetRotation();
            int loopAmount = (infiniteLoop) ? -1 : loops;
            TweenRotate(endRotation, duration, loopAmount, smoothing, movement, worldType);
        }

        protected override void StopSelf()
        {
            tween.Kill();
            if (smoothReset)
            {
                TweenRotate(GetStartingRotation(), duration * 0.5f);
                return;
            }
            ResetTargetRotation();
        }

        /// <summary>
        /// Tween movement for the target.
        /// </summary>
        /// <param name="targetRotation">The position the target has to reach.</param>
        /// <param name="duration">How long will the tween last (in seconds).</param>
        /// <param name="loopAmount">How many times to loop the tween (-1 for infinite).</param>
        /// <param name="movementType">Is the movement done in absolute or relative coordinates.</param>
        /// <param name="worldType">Does the movement happen in world space or relative to parent.</param>
        private void TweenRotate(Vector3 targetRotation, float duration, int loopAmount = 0, SmoothingType smoothingType = SmoothingType.Tween,
            MovementType movementType = MovementType.Absolute, WorldType worldType = WorldType.World)
        {
            Vector3 targetRot = (movementType == MovementType.Relative) ? GetRotation() + targetRotation : targetRotation;
            tween = (worldType == WorldType.World) ? target.DORotate(targetRot, duration, RotateMode.FastBeyond360) : target.DOLocalRotate(targetRot, duration, RotateMode.FastBeyond360);
            var easedTween = (smoothingType == SmoothingType.Tween) ? tween.SetEase(easing) : tween.SetEase(movementCurve);
            easedTween.SetLoops(loopAmount, loopType);
            easedTween.OnComplete(StopSelf);
        }
        
        /// <summary>
        /// Resets the position of the target.
        /// </summary>
        private void ResetTargetRotation()
        {
            target.eulerAngles = startRot;
            target.localEulerAngles = startLocalRot;
        }
        
        /// <summary>
        /// Returns the starting position of the target, depending on the world type.
        /// </summary>
        private Vector3 GetStartingRotation() => (worldType == WorldType.World) ? startRot : startLocalRot;
        /// <summary>
        /// Returns the current position of the target, depending on the world type.
        /// </summary>
        private Vector3 GetRotation() => (worldType == WorldType.World) ? target.eulerAngles : target.localEulerAngles;
    }
}
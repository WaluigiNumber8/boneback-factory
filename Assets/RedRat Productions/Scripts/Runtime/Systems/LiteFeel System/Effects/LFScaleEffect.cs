using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// Allows to scale a specific target.
    /// </summary>
    public class LFScaleEffect : LFEffectBase
    {
        [Header("Target")]
        [SerializeField] private Transform target;
        [SerializeField] private float duration = 0.2f;
        [SerializeField] private bool restartOnReplay = true;
        [SerializeField] private bool smoothReset;

        [Header("Scale")] 
        [SerializeField] private TransitionType mode = TransitionType.ToDestination;
        [SerializeField, HideIf("mode", TransitionType.ToDestination)] private Vector3 startScale;
        [SerializeField] private Vector3 endScale;
        [SerializeField] private SmoothingType smoothing = SmoothingType.Tween;
        [SerializeField, HideIf("smoothing", SmoothingType.AnimationCurve)] private Ease easing = Ease.InOutSine;
        [SerializeField, HideIf("smoothing", SmoothingType.Tween)] private AnimationCurve movementCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        [SerializeField, EnumToggleButtons] private MovementType movement = MovementType.Absolute;
        
        [Header("Looping")] 
        [SerializeField] private bool infiniteLoop;
        [SerializeField, Min(1), HideIf("infiniteLoop")] private int loops;
        [SerializeField] private LoopType loopType = LoopType.Restart;

        private Vector3 startScl;
        private Tween tween;

        private void Awake()
        {
            startScl = (mode == TransitionType.AtoB) ? ((movement == MovementType.Relative) ? target.localScale + startScale : startScale) : target.localScale;
        }
        
        protected override void PlaySelf()
        {
            tween.Kill();
            if (restartOnReplay) ResetTargetScale();
            int loopAmount = (infiniteLoop) ? -1 : loops;
            TweenScale(endScale, duration, loopAmount, smoothing, movement);
        }

        protected override void StopSelf()
        {
            tween.Kill();
            if (smoothReset)
            {
                TweenScale(GetStartingScale(), duration * 0.5f);
                return;
            }
            ResetTargetScale();
        }

        /// <summary>
        /// Tween scale for the target.
        /// </summary>
        /// <param name="targetScale">The scale the target has to reach.</param>
        /// <param name="duration">How long will the tween last (in seconds).</param>
        /// <param name="loopAmount">How many times to loop the tween (-1 for infinite).</param>
        /// <param name="movementType">Is the movement done in absolute or relative coordinates.</param>
        private void TweenScale(Vector3 targetScale, float duration, int loopAmount = 0, SmoothingType smoothingType = SmoothingType.Tween,
            MovementType movementType = MovementType.Absolute)
        {
            Vector3 targetScl = (movementType == MovementType.Relative) ? GetScale() + targetScale : targetScale;
            tween = target.DOScale(targetScl, duration);
            var easedTween = (smoothingType == SmoothingType.Tween) ? tween.SetEase(easing) : tween.SetEase(movementCurve);
            easedTween.SetLoops(loopAmount, loopType);
            easedTween.OnComplete(StopSelf);
        }
        
        /// <summary>
        /// Resets the scale of the target.
        /// </summary>
        private void ResetTargetScale() => target.localScale = startScl;

        /// <summary>
        /// Returns the starting scale of the target, depending on the world type.
        /// </summary>
        private Vector3 GetStartingScale() => startScl;

        /// <summary>
        /// Returns the current position of the target, depending on the world type.
        /// </summary>
        private Vector3 GetScale() => target.localScale;
    }
}
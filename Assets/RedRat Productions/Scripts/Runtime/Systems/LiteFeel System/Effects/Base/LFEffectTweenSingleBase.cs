using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// A base for all LF effects that use a single <see cref="DOTween"/> tween for their functionality.
    /// </summary>
    /// <typeparam name="T">Any type that can be tweened.</typeparam>
    public abstract class LFEffectTweenSingleBase<T> : LFEffectTweenBase
    {
        [Header("Smoothing")]
        [SerializeField] protected SmoothingType smoothing = SmoothingType.AnimationCurve;
        [SerializeField, HideIf("smoothing", SmoothingType.AnimationCurve)] protected Ease easing = Ease.InOutSine;
        [SerializeField, HideIf("smoothing", SmoothingType.Tween)] protected AnimationCurve movementCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        protected Tween tween;

        protected override void PlayTween()
        {
            tween.Kill();
            DoTween(GetTargetValue(), duration);
        }

        protected override void StopTween()
        {
            tween.Kill();
            if (!smoothReset) return;
            DoTween(GetStartingValue(), duration * 0.5f, true);
        }

        private void DoTween(T valueToReach, float duration, bool forceAbsolute = false)
        {
            UpdateStartingValues();
            Tween(valueToReach, duration, forceAbsolute);
            tween.SetLoops(loopAmount, loopType);
            tween = (smoothing == SmoothingType.Tween) ? tween.SetEase(easing) : tween.SetEase(movementCurve);
            if (resetOnEnd) tween.OnComplete(StopTween);
        }
        
        /// <summary>
        /// Tween the target.
        /// </summary>
        /// <param name="valueToReach">The value to tween tha target to.</param>
        /// <param name="duration">How long will the tween last.</param>
        /// <param name="forceAbsolute">If TRUE the tween will be of type absolute.</param>
        protected abstract void Tween(T valueToReach, float duration, bool forceAbsolute = false);
        /// <summary>
        /// Get the value the target had originally before the tween.
        /// </summary>
        protected abstract T GetStartingValue();
        /// <summary>
        /// Returns the value to tween to.
        /// </summary>
        protected abstract T GetTargetValue();
    }
}
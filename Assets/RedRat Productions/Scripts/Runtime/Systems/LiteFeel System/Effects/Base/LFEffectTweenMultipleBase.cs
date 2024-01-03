using DG.Tweening;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// A base for all LF effects that use multiple <see cref="DOTween"/> tweens for their functionality.
    /// </summary>
    public abstract class LFEffectTweenMultipleBase : LFEffectTweenBase
    {
        private Sequence sequence;

        protected virtual void Awake() => sequence.SetAutoKill(false);
        
        protected override void PlayTween()
        {
            sequence.Kill();
            Tween();
        }

        protected override void StopTween()
        {
            sequence.Kill();
            if (!smoothReset) return;
            sequence.Rewind();
        }

        protected void AddTweenToSequence(Tween tween, bool useEasingType, Ease easing, AnimationCurve curve)
        {
            tween = (useEasingType) ? tween.SetEase(easing) : tween.SetEase(curve);
            sequence.Join(tween);
        }

        private void Tween()
        {
            sequence = DOTween.Sequence();
            UpdateStartingValues();
            SetupTweens();
            sequence.SetLoops(loopAmount, loopType);
            if (resetOnEnd) sequence.OnComplete(StopTween);
        }
        
        /// <summary>
        /// Tweens used for the effect are added into a sequence.
        /// </summary>
        protected abstract void SetupTweens();
    }
}
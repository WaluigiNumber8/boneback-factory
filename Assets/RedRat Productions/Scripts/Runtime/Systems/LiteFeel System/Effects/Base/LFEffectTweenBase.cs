using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// A base for all LF effects that use <see cref="DOTween"/> tweens for their functionality.
    /// </summary>
    /// <typeparam name="T">Any type that can be tweened.</typeparam>
    public abstract class LFEffectTweenBase<T> : LFEffectBase
    {
        [Header("General")]
        [SerializeField] protected float duration = 0.2f;
        [SerializeField] protected bool additivePlay;
        [SerializeField] protected bool resetOnEnd = true;
        [SerializeField] protected bool smoothReset;
        
        [Header("Looping")] 
        [SerializeField] private bool infiniteLoop;
        [SerializeField, Min(1), HideIf("infiniteLoop")] private int loops;
        [SerializeField] protected LoopType loopType = LoopType.Restart;
        
        protected Tween tween;
        protected int loopAmount;

        protected override void PlaySelf()
        {
            tween.Kill();
            if (!additivePlay) ResetTargetState();
            loopAmount = (infiniteLoop) ? -1 : loops;
            DoTween(GetTargetValue(), duration);
        }

        protected override void StopSelf()
        {
            tween.Kill();
            if (smoothReset)
            {
                DoTween(GetStartingValue(), duration * 0.5f, true);
                return;
            }
            ResetTargetState();
        }

        private void DoTween(T valueToReach, float duration, bool forceAbsolute = false)
        {
            Tween(valueToReach, duration, forceAbsolute);
            tween.SetLoops(loopAmount, loopType);
            if (resetOnEnd) tween.OnComplete(StopSelf);
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
        /// <summary>
        /// Reset the target to its state before the tween happened.
        /// </summary>
        protected abstract void ResetTargetState();
    }
}
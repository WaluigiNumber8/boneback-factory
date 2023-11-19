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
        [Header("Target")]
        [SerializeField] protected Transform target;
        [SerializeField] protected float duration = 0.2f;
        [SerializeField] protected bool additivePlay;
        [SerializeField] protected bool resetOnEnd;
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
            Tween(GetTweenEndValue(), duration);
        }

        protected override void StopSelf()
        {
            tween.Kill();
            if (smoothReset)
            {
                Tween(GetTargetStartingValue(), duration * 0.5f);
                return;
            }
            ResetTargetState();
        }

        /// <summary>
        /// Tween the target.
        /// </summary>
        /// <param name="valueToReach">The value to tween tha target to.</param>
        /// <param name="duration">How long will the tween last.</param>
        protected abstract void Tween(T valueToReach, float duration);
        /// <summary>
        /// Get the value the target had originally before the tween.
        /// </summary>
        protected abstract T GetTargetStartingValue();
        /// <summary>
        /// Returns the value to tween to.
        /// </summary>
        protected abstract T GetTweenEndValue();
        /// <summary>
        /// Reset the target to its state before the tween happened.
        /// </summary>
        protected abstract void ResetTargetState();
    }
}
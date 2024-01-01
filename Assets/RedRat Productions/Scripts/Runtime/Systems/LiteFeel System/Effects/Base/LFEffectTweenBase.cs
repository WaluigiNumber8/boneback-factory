using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public abstract class LFEffectTweenBase : LFEffectBase
    {
        [Header("General")]
        [SerializeField] protected float duration = 0.2f;
        [SerializeField] protected bool additivePlay;
        [SerializeField] protected bool resetOnEnd = true;
        [SerializeField] protected bool smoothReset;
        
        [Header("Looping")] 
        [SerializeField] private bool infiniteLoop;
        [SerializeField, Min(1), HideIf("infiniteLoop")] private int loops = 1;
        [SerializeField] protected LoopType loopType = LoopType.Restart;

        protected int loopAmount;

        private void Start()
        {
            Initialize();
            UpdateStartingValues();
        }

        protected override void PlaySelf()
        {
            if (!additivePlay) ResetTargetState();
            loopAmount = (infiniteLoop) ? -1 : loops;
            PlayTween();
        }

        protected override void StopSelf()
        {
            StopTween();
            ResetTargetState();
        }
        
        /// <summary>
        /// Plays the effect.
        /// </summary>
        protected abstract void PlayTween();
        /// <summary>
        /// Stops the effect.
        /// </summary>
        protected abstract void StopTween();
        /// <summary>
        /// Reset the target to its state before the tween happened.
        /// </summary>
        protected abstract void ResetTargetState();
        /// <summary>
        /// Updates starting values to current values of the current target.
        /// </summary>
        protected abstract void UpdateStartingValues();
    }
}
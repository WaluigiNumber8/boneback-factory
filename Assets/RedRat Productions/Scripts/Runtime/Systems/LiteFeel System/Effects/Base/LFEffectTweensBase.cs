using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// A base for all LF effects that use multiple <see cref="DOTween"/> tweens for their functionality.
    /// </summary>
    public abstract class LFEffectTweensBase : LFEffectBase
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

        private Sequence sequence;
        protected int loopAmount;

        protected virtual void Awake() => sequence.SetAutoKill(false);
        
        protected virtual void Start() => UpdateStartingValues();

        protected override void PlaySelf()
        {
            sequence.Kill();
            if (!additivePlay) ResetTargetState();
            loopAmount = (infiniteLoop) ? -1 : loops;
            Tween(duration);
        }

        protected override void StopSelf()
        {
            sequence.Kill();
            if (smoothReset)
            {
                sequence.Rewind();
                return;
            }
            ResetTargetState();
        }

        private void Tween(float duration)
        {
            sequence = DOTween.Sequence();
            UpdateStartingValues();
            SetupTweens(sequence, duration);
            sequence.SetLoops(loopAmount, loopType);
            if (resetOnEnd) sequence.OnComplete(StopSelf);
        }
        
        /// <summary>
        /// Tweens used for the effect are added into a sequence.
        /// </summary>
        /// <param name="usedTweens">The sequence, containing tween that will be played.</param>
        protected abstract void SetupTweens(Sequence usedTweens, float duration);
        
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
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public abstract class LFEffectTweenBase : LFEffectWithDurationBase
    {
        [FoldoutGroup("$GroupLoop"), SerializeField] protected LoopType loopType = LoopType.Restart;
        
        private Sequence sequence;

        private void Awake() => sequence.SetAutoKill(false);
        private void OnDisable() => sequence.Kill();

        protected override void Start()
        {
            Initialize();
            UpdateStartingValues();
        }

        protected override void PlaySetup()
        {
            if (!additivePlay) ResetTargetState();
            sequence.Kill();
            Tween();
        }

        protected override void StopSetup()
        {
            sequence.Kill();
            ResetTargetState();
        }
        
        protected void AddTweenToSequence(Tween tween, AnimationCurve curve)
        {
            sequence.Join(tween.SetEase(curve));
        }
        
        private void Tween()
        {
            sequence = DOTween.Sequence();
            SetBeginState();
            SetupTweens();
            sequence.SetLoops(loopAmount, loopType);
            if (resetOnEnd) sequence.OnComplete(StopSelf);
        }

        /// <summary>
        /// Set parameters to beginning states right before tweening.
        /// </summary>
        protected abstract void SetBeginState();
        /// <summary>
        /// Tweens used for the effect are added into a sequence.
        /// </summary>
        protected abstract void SetupTweens();
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
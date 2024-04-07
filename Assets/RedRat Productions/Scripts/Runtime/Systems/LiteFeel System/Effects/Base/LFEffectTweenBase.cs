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

        private void OnDisable()
        {
            sequence.Kill();
            if (!gameObject.activeInHierarchy) return;
            if (PlayImmediately) SetBeginState();
        }

        protected override void Initialize() => UpdateStartingValues();

        protected override void PlaySelf()
        {
            sequence.Kill();
            Tween();
        }

        protected override void StopSelf()
        {
            sequence.Kill();
        }

        protected override void ResetState()
        {
            if (!gameObject.activeInHierarchy) return;
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
            int loops = (TotalLoops == int.MaxValue) ? -1 : TotalLoops;
            sequence.SetLoops(loops, loopType);
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
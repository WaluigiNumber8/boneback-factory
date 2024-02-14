using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public abstract class LFEffectTweenBase : LFEffectBase
    {
        public string GroupGeneral => $"General ({duration} s)";
        [FoldoutGroup("$GroupGeneral"), SerializeField] protected float duration = 0.2f;
        [FoldoutGroup("$GroupGeneral"), SerializeField] protected bool resetOnEnd = true;
        [FoldoutGroup("$GroupGeneral"), SerializeField] protected bool additivePlay;
        
        public string GroupLoop => $"Looping ({TotalLoops} L)";
        [FoldoutGroup("$GroupLoop"), SerializeField] private bool infiniteLoop;
        [FoldoutGroup("$GroupLoop"), SerializeField, Min(1), HideIf("infiniteLoop")] private int loops = 1;
        [FoldoutGroup("$GroupLoop"), SerializeField] protected LoopType loopType = LoopType.Restart;

        private Sequence sequence;
        private int loopAmount;

        private void Awake() => sequence.SetAutoKill(false);

        protected override void Start()
        {
            Initialize();
            UpdateStartingValues();
        }

        protected override void PlaySelf()
        {
            if (!additivePlay) ResetTargetState();
            loopAmount = (infiniteLoop) ? -1 : loops;
            sequence.Kill();
            Tween();
        }

        protected override void StopSelf()
        {
            sequence.Kill();
            ResetTargetState();
        }
        
        protected void AddFloatTweenToSequence(Tween tween, SmoothingType smoothing, Ease easing, AnimationCurve curve)
        {
            tween = (smoothing == SmoothingType.Tween) ? tween.SetEase(easing) : tween.SetEase(curve);
            sequence.Join(tween);
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
        
        private string TotalLoops => (infiniteLoop) ? "∞" : loops.ToString();
    }
}
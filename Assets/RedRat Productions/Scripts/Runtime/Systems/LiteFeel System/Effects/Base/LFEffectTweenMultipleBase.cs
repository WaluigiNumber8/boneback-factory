using DG.Tweening;

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
            Tween(duration);
        }

        protected override void StopTween()
        {
            sequence.Kill();
            if (!smoothReset) return;
            sequence.Rewind();
        }

        private void Tween(float duration)
        {
            sequence = DOTween.Sequence();
            UpdateStartingValues();
            SetupTweens(sequence, duration);
            sequence.SetLoops(loopAmount, loopType);
            if (resetOnEnd) sequence.OnComplete(StopTween);
        }
        
        /// <summary>
        /// Tweens used for the effect are added into a sequence.
        /// </summary>
        /// <param name="usedTweens">The sequence, containing tween that will be played.</param>
        protected abstract void SetupTweens(Sequence usedTweens, float duration);
    }
}
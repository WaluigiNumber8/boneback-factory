using System.Collections;
using RedRats.Systems.Clocks;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// An effect that controls the timescale.
    /// </summary>
    public class LFTimeFreezeEffect : LFEffectWithDurationBase
    {
        private GameClock clock;
        private float startTimeScale;
        private Coroutine playCoroutine;

        protected override void Initialize() => clock = GameClock.Instance;

        protected override void PlaySelf()
        {
            playCoroutine = StartCoroutine(PlayCoroutine());
        }

        protected override void StopSelf()
        {
            StopCoroutine(playCoroutine);
        }

        protected override void ResetState() => clock.Reset();

        private IEnumerator PlayCoroutine()
        {
            clock.Pause();
            yield return new WaitForSecondsRealtime(Duration * TotalLoops);
            clock.Reset();
        }
        
        protected override float TotalDuration { get => Duration * TotalLoops; }
        protected override string FeedbackColor { get => "#FFFFFF"; }
    }
}
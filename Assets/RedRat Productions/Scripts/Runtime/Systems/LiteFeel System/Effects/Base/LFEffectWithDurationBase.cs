using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// A base for all LF effects which have some kind of duration.
    /// </summary>
    public abstract class LFEffectWithDurationBase : LFEffectBase
    {
        public string GroupGeneral => $"General ({duration} s)";
        [FoldoutGroup("$GroupGeneral"), SerializeField] protected float duration = 0.2f;
        [FoldoutGroup("$GroupGeneral"), SerializeField] protected bool resetOnEnd = true;
        [FoldoutGroup("$GroupGeneral"), SerializeField] protected bool additivePlay;
        
        public string GroupLoop => $"Looping ({TotalLoops} L)";
        [FoldoutGroup("$GroupLoop"), SerializeField] private bool infiniteLoop;
        [FoldoutGroup("$GroupLoop"), SerializeField, Min(1), HideIf("infiniteLoop")] private int loops = 1;
        
        protected int loopAmount;

        protected override void PlaySelf()
        {
            loopAmount = (infiniteLoop) ? -1 : loops;
            PlaySetup();
        }

        protected override void StopSelf()
        {
            StopSetup();
        }

        protected abstract void PlaySetup();
        protected abstract void StopSetup();
        
        private string TotalLoops => (infiniteLoop) ? "∞" : loops.ToString();
    }
}
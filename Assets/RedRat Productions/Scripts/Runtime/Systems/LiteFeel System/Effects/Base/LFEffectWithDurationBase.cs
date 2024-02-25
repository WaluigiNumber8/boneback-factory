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
        [FoldoutGroup("$GroupGeneral"), SerializeField] private float duration = 0.2f;
        public string GroupLoop => $"Looping ({TotalLoopsAsString} L)";
        [FoldoutGroup("$GroupLoop"), SerializeField] private bool infiniteLoop;
        [FoldoutGroup("$GroupLoop"), SerializeField, Min(1), HideIf("infiniteLoop")] private int loops = 1;
        
        protected override float TotalDuration { get => duration * TotalLoops; }
        protected float Duration { get => duration; }
        
        protected int TotalLoops => (infiniteLoop) ? int.MaxValue : loops;
        private string TotalLoopsAsString => (infiniteLoop) ? "∞" : loops.ToString();
    }
}
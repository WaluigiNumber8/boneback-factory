using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFScaleShakeEffect : LFEffectTweenBase
    {
        [Header("Target")]
        
        [Header("Shake Scale")] 
        [SerializeField, InfoBox("Missing target", InfoMessageType.Error, "@target == null")] private Transform target;
        [SerializeField] private bool uniform = true;
        [SerializeField, ShowIf("uniform")] private float strength = 0.1f;
        [SerializeField, HideIf("uniform")] private Vector3 strengthVector = Vector3.one * 0.1f;
        [SerializeField] private int vibration = 20;
        [SerializeField, Range(0f, 90f)] private float randomness = 90;
        [SerializeField] private bool fadeout;
        [SerializeField] protected AnimationCurve shakeCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        private Vector3 startScale;

        protected override void SetBeginState()
        {
            // Nothing to do here.
        }

        protected override void SetupTweens()
        {
            Tween tween = target.DOShakeScale(Duration, GetTargetValue(), vibration, randomness, fadeout);
            AddTweenToSequence(tween, shakeCurve);
        }

        protected override void ResetTargetState() => target.localScale = startScale;
        protected override void UpdateStartingValues() => startScale = target.localScale;
        protected override string FeedbackColor { get => "#00FF9B"; }
        private Vector3 GetTargetValue() => (uniform) ? Vector3.one * strength : strengthVector;
    }
}

using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFPositionShakeEffect : LFEffectTweenBase
    {
        [Header("Shake Position")] 
        [SerializeField, InfoBox("Missing target", InfoMessageType.Error, "@target == null")] private Transform target;
        [SerializeField] private bool uniform = true;
        [SerializeField, ShowIf("uniform")] private float strength = 0.1f;
        [SerializeField, HideIf("uniform")] private Vector3 strengthVector = Vector3.one * 0.1f;
        [SerializeField] private int vibration = 20;
        [SerializeField, Range(0f, 90f)] private float randomness = 90;
        [SerializeField] private bool fadeout;
        [SerializeField] protected AnimationCurve shakeCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        private Vector3 startLocalPosition;

        protected override void Initialize()
        {
            // Nothing to do here.
        }

        protected override void SetBeginState()
        {
            // Nothing to do here.
        }

        protected override void SetupTweens()
        {
            Tween tween = target.DOShakePosition(Duration, GetTargetValue(), vibration, randomness, false, fadeout);
            AddTweenToSequence(tween, shakeCurve);
        }

        protected override void ResetTargetState() => target.localPosition = startLocalPosition;
        protected override void UpdateStartingValues()
        {
            startLocalPosition = target.localPosition;
        }
        
        protected override string FeedbackColor { get => "#00FF9B"; }
        
        private Vector3 GetTargetValue() => (uniform) ? Vector3.one * strength : strengthVector;
    }
}
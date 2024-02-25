using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// Allows to scale a specific target.
    /// </summary>
    public class LFScaleEffect : LFEffectTweenBase
    {
        [Header("Scale")] 
        [SerializeField, InfoBox("Missing target", InfoMessageType.Error, "@target == null")] private Transform target;
        [SerializeField, EnumToggleButtons] private MovementType movement = MovementType.Absolute;
        [SerializeField, EnumToggleButtons] private TransitionType mode = TransitionType.ToDestination;
        [SerializeField] private bool uniform = true;
        [SerializeField, HideIf("@uniform == true || mode == TransitionType.ToDestination")] private Vector3 beginScale = Vector3.one;
        [SerializeField, HideIf("@uniform == false || mode == TransitionType.ToDestination")] private float beginScaleU = 1;
        [SerializeField, HideIf("uniform")] private Vector3 targetScale = Vector3.one;
        [SerializeField, ShowIf("uniform")] private float targetScaleU = 2;
        [SerializeField] private AnimationCurve scaleCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        private Vector3 startScale;
        
        protected override void Initialize()
        {
            // Do nothing.
        }
        
        protected override void SetBeginState()
        {
            if (mode == TransitionType.AToB) target.localScale = GetBeginScale();
        }

        protected override void SetupTweens()
        {
            Vector3 targetValue = (movement == MovementType.Relative) ? target.localScale + GetTargetScale() : GetTargetScale();
            Tween tween = target.DOScale(targetValue, Duration);
            AddTweenToSequence(tween, scaleCurve);
        }

        
        protected override void ResetTargetState() => target.localScale = startScale;
        protected override void UpdateStartingValues() => startScale = target.localScale;
        protected override string FeedbackColor { get => "#00FF9B"; }
        private Vector3 GetBeginScale() => (uniform) ? Vector3.one * beginScaleU : beginScale;
        private Vector3 GetTargetScale() => (uniform) ? Vector3.one * targetScaleU : targetScale;
    }
}
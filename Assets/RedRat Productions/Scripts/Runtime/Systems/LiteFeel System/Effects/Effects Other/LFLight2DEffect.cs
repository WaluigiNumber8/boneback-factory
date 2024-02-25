using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// An effect that controls a 2D light.
    /// </summary>
    public class LFLight2DEffect : LFEffectTweenBase
    {
        [SerializeField, InfoBox("Missing target", InfoMessageType.Error, "@target == null")] private Light2D target;
        [SerializeField, LabelText(" Intensity", SdfIconType.SquareFill)] private bool animateIntensity = true;
        [SerializeField, ShowIf("animateIntensity"), LabelText("Movement"), EnumToggleButtons] protected MovementType movementIntensity = MovementType.Absolute;
        [SerializeField, ShowIf("animateIntensity"), LabelText("Mode"), EnumToggleButtons] protected TransitionType modeIntensity = TransitionType.ToDestination;
        [SerializeField, ShowIf("animateIntensity"), HideIf("modeIntensity", TransitionType.ToDestination)] protected float beginIntensity = 1f;
        [SerializeField, ShowIf("animateIntensity")] protected float targetIntensity = 1.2f;
        [SerializeField, ShowIf("animateIntensity")] protected AnimationCurve intensityCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        [SerializeField, LabelText(" Radius", SdfIconType.CircleFill)] private bool animateRadius = true;
        [SerializeField, ShowIf("animateRadius"), LabelText("Movement"), EnumToggleButtons] protected MovementType movementRadius = MovementType.Absolute;
        [SerializeField, ShowIf("animateRadius"), LabelText("Mode"), EnumToggleButtons] protected TransitionType modeRadius = TransitionType.ToDestination;
        [SerializeField, ShowIf("animateRadius"), HideIf("modeRadius", TransitionType.ToDestination)] protected float beginRadius = 8.25f;
        [SerializeField, ShowIf("animateRadius")] protected float targetRadius = 12f;
        [SerializeField, ShowIf("animateRadius")] protected AnimationCurve RadiusCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        [SerializeField, LabelText(" Color", SdfIconType.TriangleFill)] private bool animateColor;
        [SerializeField, ShowIf("animateColor"), LabelText("Mode"), EnumToggleButtons] protected TransitionType modeColor = TransitionType.ToDestination;
        [SerializeField, ShowIf("animateColor"), HideIf("modeColor", TransitionType.ToDestination)] protected Color beginColor = Color.white;
        [SerializeField, ShowIf("animateColor")] protected Color targetColor = Color.yellow;
        [SerializeField, ShowIf("animateColor")] protected AnimationCurve colorCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        private float startIntensity;
        private float startRange;
        private Color startColor;
        
        protected override void Initialize()
        {
            // Nothing to do here.
        }

        protected override void SetBeginState()
        {
            if (animateIntensity && modeIntensity == TransitionType.AToB) target.intensity = beginIntensity;
            if (animateRadius && modeRadius == TransitionType.AToB) target.pointLightOuterRadius = beginRadius;
            if (animateColor && modeColor == TransitionType.AToB) target.color = beginColor;
        }

        protected override void SetupTweens()
        {
            if (animateIntensity)
            {
                float targetValue = (movementIntensity == MovementType.Relative) ? target.intensity + targetIntensity : targetIntensity;
                Tween tween = DOTween.To(() => target.intensity, x => target.intensity = x, targetValue, Duration);
                AddTweenToSequence(tween, intensityCurve);
            }
            if (animateRadius)
            {
                float targetValue = (movementRadius == MovementType.Relative) ? target.pointLightOuterRadius + targetRadius : targetRadius;
                Tween tween = DOTween.To(() => target.pointLightOuterRadius, x => target.pointLightOuterRadius = x, targetValue, Duration);
                AddTweenToSequence(tween, RadiusCurve);
            }
            if (animateColor)
            {
                Tween tween = DOTween.To(() => target.color, x => target.color = x, targetColor, Duration);
                AddTweenToSequence(tween, colorCurve);
            }
        }

        protected override void ResetTargetState()
        {
            if (animateIntensity) target.intensity = startIntensity;
            if (animateRadius) target.pointLightOuterRadius = startRange;
            if (animateColor) target.color = startColor;
        }

        protected override void UpdateStartingValues()
        {
            startIntensity = target.intensity;
            startRange = target.pointLightOuterRadius;
            startColor = target.color;
        }
        
        protected override string FeedbackColor { get => "#F0E890"; }
    }
}
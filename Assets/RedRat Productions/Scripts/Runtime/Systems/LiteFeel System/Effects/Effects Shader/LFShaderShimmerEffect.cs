using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// An effect that animates a simmer on the target.
    /// </summary>
    public class LFShaderShimmerEffect : LFShaderBase
    {
        [SerializeField, LabelText(" Position", SdfIconType.SquareFill)] private bool animatePosition = true;
        [SerializeField, ShowIf("animatePosition"), Range(0f, 1f)] private float beginPosition = 0f;
        [SerializeField, ShowIf("animatePosition"), Range(0f, 1f)] private float targetPosition = 1f;
        [SerializeField, ShowIf("animatePosition")] protected SmoothingType positionSmoothing = SmoothingType.AnimationCurve;
        [SerializeField, ShowIf("@animatePosition && positionSmoothing == SmoothingType.Tween")] protected Ease posEasing = Ease.InOutSine;
        [SerializeField, ShowIf("@animatePosition && positionSmoothing == SmoothingType.AnimationCurve")] protected AnimationCurve positionCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        [SerializeField, LabelText(" Glow", SdfIconType.CircleFill)] private bool animateGlow;
        [SerializeField, ShowIf("animateGlow"), Range(0f, 8f)] private float beginGlow = 0.1f;
        [SerializeField, ShowIf("animateGlow"), Range(0f, 8f)] private float targetGlow = 1f;
        [SerializeField, ShowIf("animateGlow")] protected SmoothingType glowSmoothing = SmoothingType.AnimationCurve;
        [SerializeField, ShowIf("@animateGlow && glowSmoothing == SmoothingType.Tween")] protected Ease glowEasing = Ease.InOutSine;
        [SerializeField, ShowIf("@animateGlow && glowSmoothing == SmoothingType.AnimationCurve")] protected AnimationCurve glowCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        [SerializeField, LabelText(" Rotation", SdfIconType.TriangleFill)] private bool animateRotation;
        [SerializeField, ShowIf("animateRotation"), Range(0f, 360f)] private float beginRotation = 135f;
        [SerializeField, ShowIf("animateRotation"), Range(0f, 360f)] private float targetRotation = 360f;
        [SerializeField, ShowIf("animateRotation")] protected SmoothingType rotationSmoothing = SmoothingType.AnimationCurve;
        [SerializeField, ShowIf("@animateRotation && rotationSmoothing == SmoothingType.Tween")] protected Ease rotationEasing = Ease.InOutSine;
        [SerializeField, ShowIf("@animateRotation && rotationSmoothing == SmoothingType.AnimationCurve")] protected AnimationCurve rotationCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        private static readonly int PositionProperty = Shader.PropertyToID("_ShimmerPosition");
        private static readonly int GlowProperty = Shader.PropertyToID("_ShimmerGlow");
        private static readonly int RotationProperty = Shader.PropertyToID("_ShimmerRotation");
        
        private float startPosition;
        private float startGlow;
        private float startRotation;

        protected override void ResetTargetState()
        {
            material.SetFloat(PositionProperty, startPosition);
            material.SetFloat(GlowProperty, startGlow);
            material.SetFloat(RotationProperty, startRotation);
        }

        protected override void UpdateStartingValues()
        {
            startPosition = material.GetFloat(PositionProperty);
            startGlow = material.GetFloat(GlowProperty);
            startRotation = material.GetFloat(RotationProperty);
        }

        protected override void SetBeginState()
        {
            if (animatePosition) material.SetFloat(PositionProperty, beginPosition);
            if (animateGlow) material.SetFloat(GlowProperty, beginGlow);
            if (animateRotation) material.SetFloat(RotationProperty, beginRotation);
        }

        protected override void SetupTweens()
        {
            if (animatePosition) AddFloatTween(PositionProperty, targetPosition, positionSmoothing, posEasing, positionCurve);
            if (animateGlow) AddFloatTween(GlowProperty, targetGlow, glowSmoothing, glowEasing, glowCurve);
            if (animateRotation) AddFloatTween(RotationProperty, targetRotation, rotationSmoothing, rotationEasing, rotationCurve);
        }
    }
}
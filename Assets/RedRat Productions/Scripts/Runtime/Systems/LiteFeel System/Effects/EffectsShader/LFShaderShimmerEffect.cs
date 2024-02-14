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
        [SerializeField] private bool animatePosition = true;
        [SerializeField, ShowIf("animatePosition"), Range(0f, 1f)] private float beginPosition = 0f;
        [SerializeField, ShowIf("animatePosition"), Range(0f, 1f)] private float targetPosition = 1f;
        [SerializeField, ShowIf("animatePosition")] protected SmoothingType positionSmoothing = SmoothingType.AnimationCurve;
        [SerializeField, HideIf("positionSmoothing", SmoothingType.AnimationCurve), ShowIf("animatePosition")] protected Ease posEasing = Ease.InOutSine;
        [SerializeField, HideIf("positionSmoothing", SmoothingType.Tween), ShowIf("animatePosition")] protected AnimationCurve positionCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        [SerializeField] private bool animateGlow;
        [SerializeField, ShowIf("animateGlow"), Range(0f, 1f)] private float beginGlow = 0.1f;
        [SerializeField, ShowIf("animateGlow"), Range(0f, 1f)] private float targetGlow = 1f;
        [SerializeField, ShowIf("animateGlow")] protected SmoothingType glowSmoothing = SmoothingType.AnimationCurve;
        [SerializeField, HideIf("glowSmoothing", SmoothingType.AnimationCurve), ShowIf("animateGlow")] protected Ease glowEasing = Ease.InOutSine;
        [SerializeField, HideIf("glowSmoothing", SmoothingType.Tween), ShowIf("animateGlow")] protected AnimationCurve glowCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        private static readonly int PositionProperty = Shader.PropertyToID("_ShimmerPosition");
        private static readonly int GlowProperty = Shader.PropertyToID("_ShimmerGlow");
        
        private float startPosition;
        private float startGlow;

        protected override void ResetTargetState()
        {
            material.SetFloat(PositionProperty, startPosition);
            material.SetFloat(GlowProperty, startGlow);
        }

        protected override void UpdateStartingValues()
        {
            startPosition = material.GetFloat(PositionProperty);
            startGlow = material.GetFloat(GlowProperty);
        }

        protected override void SetBeginState()
        {
            if (animatePosition) material.SetFloat(PositionProperty, beginPosition);
            if (animateGlow) material.SetFloat(GlowProperty, beginGlow);
        }

        protected override void SetupTweens()
        {
            if (animatePosition) AddFloatTween(PositionProperty, targetPosition, positionSmoothing, posEasing, positionCurve);
            if (animateGlow) AddFloatTween(GlowProperty, targetGlow, glowSmoothing, glowEasing, glowCurve);
        }
    }
}
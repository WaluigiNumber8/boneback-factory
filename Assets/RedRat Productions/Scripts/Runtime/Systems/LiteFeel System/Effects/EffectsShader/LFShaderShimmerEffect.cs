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
        [Header("Shimmer position")] 
        [SerializeField] private bool animatePosition = true;
        [SerializeField, EnableIf("animatePosition"), Range(0f, 1f)] private float beginPosition = 0f;
        [SerializeField, EnableIf("animatePosition"), Range(0f, 1f)] private float targetPosition = 1f;
        [SerializeField, EnableIf("animatePosition")] protected SmoothingType positionSmoothing = SmoothingType.Tween;
        [SerializeField, EnableIf("animatePosition"), HideIf("positionSmoothing", SmoothingType.AnimationCurve)] protected Ease posEasing = Ease.InOutSine;
        [SerializeField, EnableIf("animatePosition"), HideIf("positionSmoothing", SmoothingType.Tween)] protected AnimationCurve positionCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        [Header("Glow")]
        [SerializeField] private bool animateGlow;
        [SerializeField, EnableIf("animateGlow"), Range(0f, 1f)] private float beginGlow = 0.1f;
        [SerializeField, EnableIf("animateGlow"), Range(0f, 1f)] private float targetGlow = 1f;
        [SerializeField, EnableIf("animateGlow")] protected SmoothingType glowSmoothing = SmoothingType.Tween;
        [SerializeField, EnableIf("animateGlow"), HideIf("glowSmoothing", SmoothingType.AnimationCurve)] protected Ease glowEasing = Ease.InOutSine;
        [SerializeField, EnableIf("animateGlow"), HideIf("glowSmoothing", SmoothingType.Tween)] protected AnimationCurve glowCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
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

        protected override void SetupTweens()
        {
            material.SetFloat(PositionProperty, beginPosition);
            material.SetFloat(GlowProperty, beginGlow);
            AddFloatTween(PositionProperty, targetPosition, (positionSmoothing == SmoothingType.Tween), posEasing, positionCurve);
            AddFloatTween(GlowProperty, targetGlow, (glowSmoothing == SmoothingType.Tween), glowEasing, glowCurve);
        }
    }
}
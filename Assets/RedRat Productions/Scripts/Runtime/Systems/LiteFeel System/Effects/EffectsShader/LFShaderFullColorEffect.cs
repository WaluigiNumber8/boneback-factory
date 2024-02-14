using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFShaderFullColorEffect : LFShaderBase    
    {
        [SerializeField, LabelText(" Blend", SdfIconType.SquareFill)] private bool animateBlend = true;
        [SerializeField, ShowIf("animateBlend"), Range(0f, 1f)] private float beginBlend = 0f;
        [SerializeField, ShowIf("animateBlend"), Range(0f, 1f)] private float targetBlend = 1f;
        [SerializeField, ShowIf("animateBlend")] protected SmoothingType blendSmoothing = SmoothingType.AnimationCurve;
        [SerializeField, ShowIf("@animateBlend && blendSmoothing == SmoothingType.Tween")] protected Ease blendEasing = Ease.InOutSine;
        [SerializeField, ShowIf("@animateBlend && blendSmoothing == SmoothingType.AnimationCurve")] protected AnimationCurve blendCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));

        [SerializeField, LabelText(" Glow", SdfIconType.CircleFill)] private bool animateGlow;
        [SerializeField, ShowIf("animateGlow"), Range(1f, 12f)] private float beginGlow = 1f;
        [SerializeField, ShowIf("animateGlow"), Range(1f, 12f)] private float targetGlow = 3f;
        [SerializeField, ShowIf("animateGlow")] protected SmoothingType glowSmoothing = SmoothingType.AnimationCurve;
        [SerializeField, ShowIf("@animateGlow && glowSmoothing == SmoothingType.Tween")] protected Ease glowEasing = Ease.InOutSine;
        [SerializeField, ShowIf("@animateGlow && glowSmoothing == SmoothingType.AnimationCurve")] protected AnimationCurve glowCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));

        [SerializeField, LabelText(" Color", SdfIconType.TriangleFill)] private bool animateColor;
        [SerializeField, ShowIf("animateColor"), ColorUsage(false)] private Color targetColor = Color.red;
        [SerializeField, ShowIf("animateColor")] protected SmoothingType colorSmoothing = SmoothingType.AnimationCurve;
        [SerializeField, ShowIf("@animateColor && colorSmoothing == SmoothingType.Tween")] protected Ease colorEasing = Ease.InOutSine;
        [SerializeField, ShowIf("@animateColor && colorSmoothing == SmoothingType.AnimationCurve")] protected AnimationCurve colorCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        private static readonly int BlendProperty = Shader.PropertyToID("_FullColorBlend");
        private static readonly int GlowProperty = Shader.PropertyToID("_FullColorGlow");
        private static readonly int ColorProperty = Shader.PropertyToID("_FullBlendColor");
        
        private float startBlend;
        private float startGlow;
        private Color startColor;
        
        protected override void ResetTargetState()
        {
            if (animateBlend) material.SetFloat(BlendProperty, startBlend);
            if (animateGlow) material.SetFloat(GlowProperty, startGlow);
            if (animateColor) material.SetColor(ColorProperty, startColor);
        }

        protected override void UpdateStartingValues()
        {
            startBlend = material.GetFloat(BlendProperty);
            startGlow = material.GetFloat(GlowProperty);
            startColor = material.GetColor(ColorProperty);
        }

        protected override void SetBeginState()
        {
            if (animateBlend) material.SetFloat(BlendProperty, beginBlend);
            if (animateGlow) material.SetFloat(GlowProperty, beginGlow);
        }

        protected override void SetupTweens()
        {
            AddFloatTween(BlendProperty, targetBlend, blendSmoothing, blendEasing, blendCurve);
            AddFloatTween(GlowProperty, targetGlow, glowSmoothing, glowEasing, glowCurve);
            AddColorTween(ColorProperty, targetColor, colorSmoothing, colorEasing, colorCurve);
        }
    }
}
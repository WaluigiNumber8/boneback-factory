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
        [SerializeField, ShowIf("animateBlend")] protected AnimationCurve blendCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));

        [SerializeField, LabelText(" Glow", SdfIconType.CircleFill)] private bool animateGlow;
        [SerializeField, ShowIf("animateGlow"), Range(1f, 12f)] private float beginGlow = 1f;
        [SerializeField, ShowIf("animateGlow"), Range(1f, 12f)] private float targetGlow = 3f;
        [SerializeField, ShowIf("animateGlow")] protected AnimationCurve glowCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));

        [SerializeField, LabelText(" Color", SdfIconType.TriangleFill)] private bool animateColor;
        [SerializeField, ShowIf("animateColor"), ColorUsage(false)] private Color targetColor = Color.red;
        [SerializeField, ShowIf("animateColor")] protected AnimationCurve colorCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
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
            AddFloatTween(BlendProperty, targetBlend, blendCurve);
            AddFloatTween(GlowProperty, targetGlow, glowCurve);
            AddColorTween(ColorProperty, targetColor, colorCurve);
        }
    }
}
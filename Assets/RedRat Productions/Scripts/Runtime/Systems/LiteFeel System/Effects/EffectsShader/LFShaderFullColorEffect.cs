using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFShaderFullColorEffect : LFShaderBase    
    {
        [Header("Color Blend")] 
        [SerializeField] private bool animateBlend = true;
        [SerializeField, EnableIf("animateBlend"), Range(0f, 1f)] private float beginBlend = 0f;
        [SerializeField, EnableIf("animateBlend"), Range(0f, 1f)] private float targetBlend = 1f;
        [SerializeField, EnableIf("animateBlend")] protected SmoothingType blendSmoothing = SmoothingType.AnimationCurve;
        [SerializeField, EnableIf("animateBlend"), HideIf("blendSmoothing", SmoothingType.AnimationCurve)] protected Ease blendEasing = Ease.InOutSine;
        [SerializeField, EnableIf("animateBlend"), HideIf("blendSmoothing", SmoothingType.Tween)] protected AnimationCurve blendCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        [Header("Glow")]
        [SerializeField] private bool animateGlow;
        [SerializeField, EnableIf("animateGlow"), Range(1f, 12f)] private float beginGlow = 1f;
        [SerializeField, EnableIf("animateGlow"), Range(1f, 12f)] private float targetGlow = 3f;
        [SerializeField, EnableIf("animateGlow")] protected SmoothingType glowSmoothing = SmoothingType.AnimationCurve;
        [SerializeField, EnableIf("animateGlow"), HideIf("glowSmoothing", SmoothingType.AnimationCurve)] protected Ease glowEasing = Ease.InOutSine;
        [SerializeField, EnableIf("animateGlow"), HideIf("glowSmoothing", SmoothingType.Tween)] protected AnimationCurve glowCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        [Header("Color")] 
        [SerializeField] private bool animateColor;
        [SerializeField, EnableIf("animateColor"), ColorUsage(false)] private Color targetColor = Color.red;
        [SerializeField, EnableIf("animateColor")] protected SmoothingType colorSmoothing = SmoothingType.AnimationCurve;
        [SerializeField, EnableIf("animateColor"), HideIf("colorSmoothing", SmoothingType.AnimationCurve)] protected Ease colorEasing = Ease.InOutSine;
        [SerializeField, EnableIf("animateColor"), HideIf("colorSmoothing", SmoothingType.Tween)] protected AnimationCurve colorCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
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
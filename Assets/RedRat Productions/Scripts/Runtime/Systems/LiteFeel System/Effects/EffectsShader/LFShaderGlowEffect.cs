using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFShaderGlowEffect : LFShaderBase
    {
        [Header("Glow")]
        [SerializeField, ColorUsage(false, true)] private Color beginGlow;
        [SerializeField, ColorUsage(false, true)] private Color targetGlow;
        [SerializeField] protected SmoothingType smoothing = SmoothingType.Tween;
        [SerializeField, HideIf("smoothing", SmoothingType.AnimationCurve)] protected Ease easing = Ease.InOutSine;
        [SerializeField, HideIf("smoothing", SmoothingType.Tween)] protected AnimationCurve blendCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        private static readonly int GlowProperty = Shader.PropertyToID("_GlowColor");
        private Color startGlow;
        
        protected override void ResetTargetState()
        {
            material.SetColor(GlowProperty, startGlow);
        }

        protected override void UpdateStartingValues()
        {
            startGlow = material.GetColor(GlowProperty);
        }

        protected override void SetBeginState()
        {
            material.SetColor(GlowProperty, beginGlow);
        }

        protected override void SetupTweens()
        {
            AddColorTween(GlowProperty, targetGlow, smoothing, easing, blendCurve);
        }
    }
}
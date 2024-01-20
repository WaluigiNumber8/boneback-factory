using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFShaderTintEffect : LFShaderBase
    {
        [Header("Tint")]
        [SerializeField] private Color beginTint = Color.white;
        [SerializeField] private Color targetTint = Color.white;
        [SerializeField] protected SmoothingType smoothing = SmoothingType.Tween;
        [SerializeField, HideIf("smoothing", SmoothingType.AnimationCurve)] protected Ease easing = Ease.InOutSine;
        [SerializeField, HideIf("smoothing", SmoothingType.Tween)] protected AnimationCurve blendCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        private static readonly int TintProperty = Shader.PropertyToID("_Tint");
        private Color startTint;

        protected override void ResetTargetState()
        {
            material.SetColor(TintProperty, startTint);
        }

        protected override void UpdateStartingValues()
        {
            startTint = material.GetColor(TintProperty);
        }

        protected override void SetBeginState()
        {
            material.SetColor(TintProperty, beginTint);
        }

        protected override void SetupTweens()
        {
            AddColorTween(TintProperty, targetTint, (smoothing == SmoothingType.Tween), easing, blendCurve);
        }
    }
}
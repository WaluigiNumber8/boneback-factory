using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFShaderGlowEffect : LFShaderBase
    {
        [SerializeField, LabelText(" Intensity", SdfIconType.SquareFill)] private bool animateIntensity;
        [SerializeField, ShowIf("animateIntensity"), Range(1f, 16f)] private float beginIntensity = 1;
        [SerializeField, ShowIf("animateIntensity"), Range(1f, 16f)] private float targetIntensity = 2;
        [SerializeField, ShowIf("animateIntensity")] protected SmoothingType intensitySmoothing = SmoothingType.AnimationCurve;
        [SerializeField, ShowIf("@animateIntensity && intensitySmoothing == SmoothingType.Tween")] protected Ease intensityEasing = Ease.InOutSine;
        [SerializeField, ShowIf("@animateIntensity && intensitySmoothing == SmoothingType.AnimationCurve")] protected AnimationCurve intensityCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));

        [SerializeField, LabelText(" Saturation", SdfIconType.CircleFill)] private bool animateSaturation;
        [SerializeField, ShowIf("animateSaturation"), Range(0f, 12f)] private float beginSaturation = 1;
        [SerializeField, ShowIf("animateSaturation"), Range(0f, 12f)] private float targetSaturation = 2;
        [SerializeField, ShowIf("animateSaturation")] protected SmoothingType saturationSmoothing = SmoothingType.AnimationCurve;
        [SerializeField, ShowIf("@animateSaturation && saturationSmoothing == SmoothingType.Tween")] protected Ease saturationEasing = Ease.InOutSine;
        [SerializeField, ShowIf("@animateSaturation && saturationSmoothing == SmoothingType.AnimationCurve")] protected AnimationCurve saturationCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        private static readonly int IntensityProperty = Shader.PropertyToID("_GlowIntensity");
        private static readonly int SaturationProperty = Shader.PropertyToID("_GlowSaturation");
        
        private float startIntensity;
        private float startSaturation;
        
        protected override void ResetTargetState()
        {
            material.SetFloat(IntensityProperty, startIntensity);
            material.SetFloat(SaturationProperty, startSaturation);
        }

        protected override void UpdateStartingValues()
        {
            startIntensity = material.GetFloat(IntensityProperty);
            startSaturation = material.GetFloat(SaturationProperty);
        }

        protected override void SetBeginState()
        {
            if (animateIntensity) material.SetFloat(IntensityProperty, beginIntensity);
            if (animateSaturation) material.SetFloat(SaturationProperty, beginSaturation);
        }

        protected override void SetupTweens()
        {
            if (animateIntensity) AddFloatTween(IntensityProperty, targetIntensity, intensitySmoothing, intensityEasing, intensityCurve);
            if (animateSaturation) AddFloatTween(SaturationProperty, targetSaturation, saturationSmoothing, saturationEasing, saturationCurve);
        }
    }
}
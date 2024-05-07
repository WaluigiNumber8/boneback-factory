using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFShaderGlowEffect : LFEffectShaderBase
    {
        [SerializeField, LabelText(" Intensity", SdfIconType.SquareFill)] private bool animateIntensity;
        [SerializeField, ShowIf("animateIntensity"), EnumToggleButtons] private TransitionType intensityMode = TransitionType.AToB;
        [SerializeField, ShowIf("@animateIntensity && intensityMode == TransitionType.AToB"), Range(1f, 16f)] private float beginIntensity = 1;
        [SerializeField, ShowIf("animateIntensity"), Range(1f, 16f)] private float targetIntensity = 2;
        [SerializeField, ShowIf("animateIntensity")] protected AnimationCurve intensityCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));

        [SerializeField, LabelText(" Saturation", SdfIconType.CircleFill)] private bool animateSaturation;
        [SerializeField, ShowIf("animateSaturation"), EnumToggleButtons] private TransitionType saturationMode = TransitionType.AToB;
        [SerializeField, ShowIf("@animateSaturation && saturationMode == TransitionType.AToB"), Range(0f, 12f)] private float beginSaturation = 1;
        [SerializeField, ShowIf("animateSaturation"), Range(0f, 12f)] private float targetSaturation = 2;
        [SerializeField, ShowIf("animateSaturation")] protected AnimationCurve saturationCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
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
            if (animateIntensity && intensityMode == TransitionType.AToB) material.SetFloat(IntensityProperty, beginIntensity);
            if (animateSaturation && saturationMode == TransitionType.AToB) material.SetFloat(SaturationProperty, beginSaturation);
        }

        protected override void SetupTweens()
        {
            if (animateIntensity) AddFloatTween(IntensityProperty, targetIntensity, intensityCurve);
            if (animateSaturation) AddFloatTween(SaturationProperty, targetSaturation, saturationCurve);
        }
    }
}
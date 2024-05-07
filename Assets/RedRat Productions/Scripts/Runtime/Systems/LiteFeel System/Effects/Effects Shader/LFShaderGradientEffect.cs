using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// An effect that changes the target's gradient.
    /// </summary>
    public class LFShaderGradientEffect : LFEffectShaderBase
    {
        [SerializeField, LabelText(" Blend", SdfIconType.SquareFill)] private bool animateBlend = true;
        [SerializeField, ShowIf("animateBlend"), LabelText("Mode"), EnumToggleButtons] protected TransitionType modeBlend = TransitionType.AToB;
        [SerializeField, ShowIf("@animateBlend && modeBlend == TransitionType.AToB"), Range(0f, 1f)] private float beginBlend = 0f;
        [SerializeField, ShowIf("animateBlend"), Range(0f, 1f)] private float targetBlend = 1f;
        [SerializeField, ShowIf("animateBlend")] protected AnimationCurve blendCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));

        [SerializeField, LabelText(" Influence", SdfIconType.CircleFill)] private bool animateInfluence;
        [SerializeField, ShowIf("animateInfluence"), LabelText("Mode"), EnumToggleButtons] protected TransitionType modeInfluence = TransitionType.AToB;
        [SerializeField, ShowIf("@animateInfluence && modeInfluence == TransitionType.AToB"), Range(0f, 1f)] private float beginColor1Influence = 0.5f;
        [SerializeField, ShowIf("animateInfluence"), Range(0f, 1f)] private float targetColor1Influence = 0.75f;
        [SerializeField, ShowIf("@animateInfluence && modeInfluence == TransitionType.AToB"), Range(0f, 1f)] private float beginColor2Influence = 0.5f;
        [SerializeField, ShowIf("animateInfluence"), Range(0f, 1f)] private float targetColor2Influence = 0.75f;
        [SerializeField, ShowIf("animateInfluence")] protected AnimationCurve influenceCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));

        [SerializeField, LabelText(" Color", SdfIconType.TriangleFill)] private bool animateColor;
        [SerializeField, ShowIf("animateColor"), LabelText("Mode"), EnumToggleButtons] protected TransitionType modeColor = TransitionType.ToDestination;
        [SerializeField, ShowIf("@animateColor && modeColor == TransitionType.AToB"), ColorUsage(false, true)] private Color beginColor01 = Color.yellow;
        [SerializeField, ShowIf("animateColor"), ColorUsage(false, true)] private Color targetColor01 = Color.red;
        [SerializeField, ShowIf("@animateColor && modeColor == TransitionType.AToB"), ColorUsage(false, true)] private Color beginColor02 = Color.yellow;
        [SerializeField, ShowIf("animateColor"), ColorUsage(false, true)] private Color targetColor02 = Color.red;
        [SerializeField, ShowIf("@animateColor && modeColor == TransitionType.AToB"), ColorUsage(false, true)] private Color beginColor03 = Color.black;
        [SerializeField, ShowIf("animateColor"), ColorUsage(false, true)] private Color targetColor03 = Color.black;
        [SerializeField, ShowIf("animateColor")] protected AnimationCurve colorCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        private static readonly int BlendProperty = Shader.PropertyToID("_GradientBlend");
        private static readonly int Color1InfluenceProperty = Shader.PropertyToID("_GradientColor1Influence");
        private static readonly int Color2InfluenceProperty = Shader.PropertyToID("_GradientColor2Influence");
        private static readonly int Color1Property = Shader.PropertyToID("_GradientColor1");
        private static readonly int Color2Property = Shader.PropertyToID("_GradientColor2");
        private static readonly int Color3Property = Shader.PropertyToID("_GradientColor3");
        
        private float startBlend;
        private float startColor1Influence;
        private float startColor2Influence;
        private Color startColor1;
        private Color startColor2;
        private Color startColor3;
        
        protected override void ResetTargetState()
        {
            if (animateBlend) material.SetFloat(BlendProperty, startBlend);
            if (animateInfluence)
            {
                material.SetFloat(Color1InfluenceProperty, startColor1Influence);
                material.SetFloat(Color2InfluenceProperty, startColor2Influence);
            }
            if (animateColor)
            {
                material.SetColor(Color1Property, startColor1);
                material.SetColor(Color2Property, startColor2);
                material.SetColor(Color3Property, startColor3);
            }
        }

        protected override void UpdateStartingValues()
        {
            startBlend = material.GetFloat(BlendProperty);
            startColor1Influence = material.GetFloat(Color1InfluenceProperty);
            startColor2Influence = material.GetFloat(Color2InfluenceProperty);
            startColor1 = material.GetColor(Color1Property);
            startColor2 = material.GetColor(Color2Property);
            startColor3 = material.GetColor(Color3Property);
        }

        protected override void SetBeginState()
        {
            if (animateBlend && modeBlend == TransitionType.AToB) material.SetFloat(BlendProperty, beginBlend);
            if (animateInfluence && modeInfluence == TransitionType.AToB)
            {
                material.SetFloat(Color1InfluenceProperty, beginColor1Influence);
                material.SetFloat(Color2InfluenceProperty, beginColor2Influence);
            }
            if (animateColor && modeColor == TransitionType.AToB)
            {
                material.SetColor(Color1Property, beginColor01);
                material.SetColor(Color2Property, beginColor02);
                material.SetColor(Color3Property, beginColor03);
            }
        }

        protected override void SetupTweens()
        {
            if (animateBlend) AddFloatTween(BlendProperty, targetBlend, blendCurve);
            if (animateInfluence)
            {
                AddFloatTween(Color1InfluenceProperty, targetColor1Influence, influenceCurve);
                AddFloatTween(Color2InfluenceProperty, targetColor2Influence, influenceCurve);
            }
            if (animateColor)
            {
                AddColorTween(Color1Property, targetColor01, colorCurve);
                AddColorTween(Color2Property, targetColor02, colorCurve);
                AddColorTween(Color3Property, targetColor03, colorCurve);
            }
        }
    }
}
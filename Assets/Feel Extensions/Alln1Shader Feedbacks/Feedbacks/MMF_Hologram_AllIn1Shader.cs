using MoreMountains.Feedbacks;
using UnityEngine;

namespace RedRats.FeelExtension.AllIn1ShaderFeedbacks
{
    [FeedbackHelp("Allows for animating the hologram effect of the renderer.")]
    [FeedbackPath("AllIn1Shader/Hologram")]
    public class MMF_Hologram_AllIn1Shader : MMF_Base_AllIn1Shader
    {
        private static readonly int AttributeHologramBlend = Shader.PropertyToID("_HologramBlend");
        private static readonly int AttributeHologramColor = Shader.PropertyToID("_HologramStripeColor");

        /// <summary>
        /// Curve of the hologram blend effect.
        /// </summary>
        [Tooltip("Curve of the hologram blend effect.")]
        [MMFInspectorGroup("Hologram Blend", true, 32)]
        public AnimationCurve hologramBlendCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        /// <summary>
        /// Starting position of the hologram blend effect.
        /// </summary>
        [Tooltip("Starting position of the hologram blend effect.")]
        public float hologramBlendZero = 0f;
        /// <summary>
        /// Ending position of the hologram blend effect.
        /// </summary>
        [Tooltip("Ending position of the hologram blend effect.")]
        public float hologramBlendOne = 0.256f;

        /// <summary>
        /// The color of the hologram over time.
        /// </summary>
        [Tooltip("The color of the hologram over time.")]
        [MMFInspectorGroup("Color", true, 2)]
        public Gradient colorOverTime;
        
        private float _initialHologramBlend;
        private Color32 _initialColor;

        protected override void InitializeValues()
        {
            _initialHologramBlend = _material.GetFloat(AttributeHologramBlend);
            _initialColor = _material.GetColor(AttributeHologramColor);
        }
        
        protected override void SetMaterialValues(float time, float intensityMultiplier)
        {
            SetMaterialFloat(AttributeHologramBlend, hologramBlendCurve, hologramBlendZero, hologramBlendOne, time, intensityMultiplier);
            SetMaterialColor(AttributeHologramColor, colorOverTime, time);
        }

        protected override void ResetValues()
        {
            _material.SetFloat(AttributeHologramBlend, _initialHologramBlend);
            _material.SetColor(AttributeHologramColor, _initialColor);
        }
        
    }
}
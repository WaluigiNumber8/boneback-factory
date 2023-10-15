using MoreMountains.Feedbacks;
using UnityEngine;

namespace RedRats.FeelExtension.AllIn1ShaderFeedbacks
{
    /// <summary>
    /// Allows for animating the glow effect of the renderer.
    /// </summary>
    [FeedbackHelp("Allows for animating the glow effect of renderer.")]
    [FeedbackPath("AllIn1Shader/Glow")]
    public class MMF_Glow_AllIn1Shader : MMF_Base_AllIn1Shader
    {
        private static readonly int AttributeGlowGlobal = Shader.PropertyToID("_GlowGlobal");
        private static readonly int AttributeGlowColor = Shader.PropertyToID("_Glow");

        /// <summary>
        /// If TRUE the glow effect will be animated.
        /// </summary>
        [Tooltip("If TRUE the glow effect will be animated.")]
        [MMFInspectorGroup("Glow Intensity", true, 5)]
        public bool animateGlow = true;
        /// <summary>
        /// Curve of the glow effect.
        /// </summary>
        [Tooltip("Curve of the glow effect.")]
        public AnimationCurve glowCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        /// <summary>
        /// Starting position of the glow.
        /// </summary>
        [Tooltip("Starting position of the glow")]
        [Range(1f, 100f)] public float glowZero = 1f;
        /// <summary>
        /// Ending position of the glow.
        /// </summary>
        [Tooltip("Ending position of the glow.")]
        [Range(1f, 100f)] public float glowOne = 2.5f;

        /// <summary>
        /// If TRUE the color glow will be affected by the animation.
        /// </summary>
        [Tooltip("If TRUE the color glow will be affected by the animation.")]
        [MMFInspectorGroup("Glow Color", true, 17)]
        public bool animateGlowColor = false;
        /// <summary>
        /// Curve of the glow color.
        /// </summary>
        [Tooltip("Curve of the glow color.")]
        public AnimationCurve glowColorCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        /// <summary>
        /// Starting position of the color glow.
        /// </summary>
        [Tooltip("Starting position of the color glow.")]
        public float glowColorZero = 0f;
        /// <summary>
        /// Ending position of the color glow.
        /// </summary>
        [Tooltip("Ending position of the color glow.")]
        public float glowColorOne = 2f;
        private float _initialGlowGlobal;
        private float _initialGlowColor;
        
        protected override void InitializeValues()
        {
            _initialGlowGlobal = _material.GetFloat(AttributeGlowGlobal);
            _initialGlowColor = _material.GetFloat(AttributeGlowColor);
        }

        protected override void SetMaterialValues(float time, float intensityMultiplier)
        {
            if (animateGlow) SetMaterialFloat(AttributeGlowGlobal, glowCurve, glowZero, glowOne, time, intensityMultiplier);
            if (animateGlowColor) SetMaterialFloat(AttributeGlowColor, glowColorCurve, glowColorZero, glowColorOne, time, intensityMultiplier);
        }

        protected override void ResetValues()
        {
            _material.SetFloat(AttributeGlowGlobal, _initialGlowGlobal);
            _material.SetFloat(AttributeGlowColor, _initialGlowColor);
        }
    }
}
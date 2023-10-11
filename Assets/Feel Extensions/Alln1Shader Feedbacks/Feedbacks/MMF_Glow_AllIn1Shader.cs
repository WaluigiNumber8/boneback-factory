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

        /// <summary>
        /// Curve of the glow effect.
        /// </summary>
        [Tooltip("Curve of the glow effect.")]
        [MMFInspectorGroup("Glow Intensity", true, 5)]
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
        
        private float _initialGlowGlobal;
        
        protected override void InitializeValues()
        {
            _initialGlowGlobal = _material.GetFloat(AttributeGlowGlobal);
        }

        protected override void SetMaterialValues(float time, float intensityMultiplier)
        {
            SetMaterialFloat(AttributeGlowGlobal, glowCurve, glowZero, glowOne, time, intensityMultiplier);
        }

        protected override void ResetValues()
        {
            _material.SetFloat(AttributeGlowGlobal, _initialGlowGlobal);
        }
    }
}
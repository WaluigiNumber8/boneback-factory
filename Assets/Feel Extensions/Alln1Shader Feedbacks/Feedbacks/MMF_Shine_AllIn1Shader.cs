using MoreMountains.Feedbacks;
using UnityEngine;

namespace RedRats.Plugins.FeelAllIn1Shader
{
    [AddComponentMenu("")]
    [FeedbackHelp("Allows for animating the shine effect of renderer.")]
    [FeedbackPath("AllIn1Shader/Shine")]
    public class MMF_Shine_AllIn1Shader : MMF_Base_AllIn1Shader
    {
        private static readonly int AttributeShineLocation = Shader.PropertyToID("_ShineLocation");
        
        /// <summary>
        /// Curve of the shine position.
        /// </summary>
        [Tooltip("Curve of the shine position.")]
        [MMFInspectorGroup("Shine position", true, 42)] 
        public AnimationCurve shinePositionCurve = new(new Keyframe(0, 0, 1, 1), new Keyframe(1, 1, 1, 1));
        /// <summary>
        /// Starting position of the shine.
        /// </summary>
        [Tooltip("Starting position of the shine.")]
        [Range(0f, 1f)] public float shinePositionZero = 0;
        /// <summary>
        /// Ending position of the shine.
        /// </summary>
        [Tooltip("Ending position of the shine.")]
        [Range(0f, 1f)] public float shinePositionOne = 1;

        private float _initialShinePosition;

        public override void Initialization(MMF_Player owner, int index)
        {
            base.Initialization(owner, index);
            _initialShinePosition = _material.GetFloat(AttributeShineLocation);
        }

        /// <summary>
        /// Sets the value of the material parameter.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="intensityMultiplier"></param>
        protected override void SetMaterialValues(float time, float intensityMultiplier)
        {
            SetMaterialValue(AttributeShineLocation, shinePositionCurve, shinePositionZero, shinePositionOne, time, intensityMultiplier);
        }

        protected override void ResetValues()
        {
            _material.SetFloat(AttributeShineLocation, _initialShinePosition);
        }
    }
}
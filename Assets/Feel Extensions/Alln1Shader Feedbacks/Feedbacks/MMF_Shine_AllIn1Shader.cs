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
        private static readonly int AttributeShineRotation = Shader.PropertyToID("_ShineRotate");

        /// <summary>
        /// If TRUE the shine position will be affected by the animation.
        /// </summary>
        [Tooltip("If TRUE the shine position will be affected by the animation.")]
        [MMFInspectorGroup("Shine position", true, 42)]
        public bool animateShinePosition = true;
        /// <summary>
        /// Curve of the shine position.
        /// </summary>
        [Tooltip("Curve of the shine position.")] 
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

        /// <summary>
        /// If TRUE the rotation will be affected by the animation.
        /// </summary>
        [Tooltip("If TRUE the rotation will be affected by the animation.")]
        [MMFInspectorGroup("Rotation", true, 3)]
        public bool animateRotation = false;
        /// <summary>
        /// Curve of the shine rotation.
        /// </summary>
        [Tooltip("Curve of the shine rotation.")]
        public AnimationCurve shineRotationCurve = new(new Keyframe(0, 0, 1, 1), new Keyframe(1, 1, 1, 1));
        /// <summary>
        /// Starting position of the shine.
        /// </summary>
        [Tooltip("Starting rotation of the shine.")]
        [Range(0f, 6.28f)] public float shineRotationZero = 0;
        /// <summary>
        /// Ending position of the shine.
        /// </summary>
        [Tooltip("Ending rotation of the shine.")]
        [Range(0f, 6.28f)] public float shineRotationOne = 0.56f;

        private float _initialShinePosition;
        private float _initialShineRotation;

        protected override void InitializeValues()
        {
            _initialShinePosition = _material.GetFloat(AttributeShineLocation);
            _initialShineRotation = _material.GetFloat(AttributeShineRotation);
        }
        
        /// <summary>
        /// Sets the value of the material parameter.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="intensityMultiplier"></param>
        protected override void SetMaterialValues(float time, float intensityMultiplier)
        {
            if (animateShinePosition) SetMaterialFloat(AttributeShineLocation, shinePositionCurve, shinePositionZero, shinePositionOne, time, intensityMultiplier);
            if (animateRotation) SetMaterialFloat(AttributeShineRotation, shineRotationCurve, shineRotationZero, shineRotationOne, time, intensityMultiplier);
        }

        protected override void ResetValues()
        {
            _material.SetFloat(AttributeShineLocation, _initialShinePosition);
            _material.SetFloat(AttributeShineRotation, _initialShineRotation);
        }
    }
}
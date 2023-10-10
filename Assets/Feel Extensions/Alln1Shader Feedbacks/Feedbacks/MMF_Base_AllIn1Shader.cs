using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace RedRats.FeelExtension.AllIn1ShaderFeedbacks
{
    public abstract class MMF_Base_AllIn1Shader : MMF_Feedback
    {
        public static bool FeedbackTypeAuthorized = true;
        public override float FeedbackDuration { get { return ApplyTimeMultiplier(duration); } }

        public override bool EvaluateRequiresSetup() => targetMaterial == null;
        public override string RequiredTargetText => (targetMaterial != null) ? targetMaterial.name : "";
        public override string RequiresSetupText => "This feedback requires a single target material, grabbed by the MaterialExtractor.";
        public override bool HasAutomatedTargetAcquisition => true;
        protected override void AutomateTargetAcquisition() => targetMaterial = FindAutomatedTarget<MaterialExtractor>();
#if UNITY_EDITOR
        public override Color FeedbackColor => new Color32(254, 11, 104, 255);
#endif
        /// <summary>
        /// The target material that will be affected.
        /// </summary>
        [Tooltip("The target material that will be affected.")]
        [MMFInspectorGroup("Material", true)]
        public MaterialExtractor targetMaterial;
        /// <summary>
        /// Duration of the effect.
        /// </summary>
        [Tooltip("Duration of the effect.")]
        public float duration = 0.2f;
        /// <summary>
        /// Whenever to reset affected shader attributes to their original form when the feedback is stopped.
        /// </summary>
        [Tooltip("Whenever to reset affected shader attributes to their original form when the feedback is stopped.")]
        public bool resetAfterStop = true;
        
        protected Material _material;
        protected Coroutine _coroutine;
        
        public override void Initialization(MMF_Player owner, int index)
        {
            base.Initialization(owner, index);
            _material = targetMaterial.Get();
            InitializeValues();
        }
        
        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            if (!Active || !FeedbackTypeAuthorized) return;
            
            float intensity = ComputeIntensity(feedbacksIntensity, position);
            _coroutine = Owner.StartCoroutine(TransitionCoroutine(intensity));
        }
        
        protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            if (!Active || !FeedbackTypeAuthorized) return;

            base.CustomStopFeedback(position, feedbacksIntensity);
            IsPlaying = false;

            if (resetAfterStop) ResetValues();

            if (!Active || (_coroutine == null)) return;
            Owner.StopCoroutine(_coroutine);
            _coroutine = null;

        }
        
        public override void RestoreInitialValues()
        {
            if (!Active || !FeedbackTypeAuthorized) return;
            base.CustomRestoreInitialValues();
            
            ResetValues();
        }
        
        /// <summary>
        /// Transitions material parameters.
        /// </summary>
        /// <param name="intensityMultiplier"></param>
        /// <returns></returns>
        private IEnumerator TransitionCoroutine(float intensityMultiplier)
        {
            IsPlaying = true;
            float journey = NormalPlayDirection ? 0f : FeedbackDuration;
            while ((journey >= 0) && (journey <= FeedbackDuration) && (FeedbackDuration > 0))
            {
                float remappedTime = MMFeedbacksHelpers.Remap(journey, 0f, FeedbackDuration, 0f, 1f);

                SetMaterialValues(remappedTime, intensityMultiplier);
                
                journey += NormalPlayDirection ? FeedbackDeltaTime : -FeedbackDeltaTime;
                yield return null;
            }
            SetMaterialValues(FinalNormalizedTime, intensityMultiplier);
            IsPlaying = false;
            _coroutine = null;
            yield return null;
        }

        /// <summary>
        /// Runs at Start. Initializes all values.
        /// </summary>
        protected abstract void InitializeValues();
        
        /// <summary>
        /// Runs every frame. Use it together with <see cref="SetMaterialFloat"/> to move a material's shader value.
        /// </summary>
        /// <param name="time">The current time of the animation.</param>
        /// <param name="intensityMultiplier">Multiplier for randomness settings.</param>
        protected abstract void SetMaterialValues(float time, float intensityMultiplier);

        /// <summary>
        /// Reset all shader values to their original form.
        /// </summary>
        protected abstract void ResetValues();

        /// <summary>
        /// Sets a shader float value to a material according to it's position on an <see cref="AnimationCurve"/> in the current time.
        /// </summary>
        /// <param name="shaderAttribute">The attribute of the shader to modify.</param>
        /// <param name="curve">The attributes <see cref="AnimationCurve"/>.</param>
        /// <param name="position0">Starting position of the attribute.</param>
        /// <param name="position1">Ending position of the attribute.</param>
        /// <param name="currentTime">Current time of the transition.</param>
        /// <param name="intensityMultiplier">intensityMultiplier for randomness.</param>
        protected void SetMaterialFloat(int shaderAttribute, AnimationCurve curve, float position0, float position1, float currentTime, float intensityMultiplier = 1)
        {
            float value = MMFeedbacksHelpers.Remap(curve.Evaluate(currentTime), 0f, 1f, position0, position1);
            _material.SetFloat(shaderAttribute, value * intensityMultiplier);
        }

        /// <summary>
        /// Sets a shader color value to a material according to current time.
        /// </summary>
        /// <param name="shaderAttribute">The attribute of the shader to modify.</param>
        /// <param name="colorOverTime">The gradient showing color changes over time.</param>
        /// <param name="currentTime">Current time of the transition.</param>
        protected void SetMaterialColor(int shaderAttribute, Gradient colorOverTime, float currentTime)
        {
            _material.SetColor(shaderAttribute, colorOverTime.Evaluate(currentTime));
        }
    }
}
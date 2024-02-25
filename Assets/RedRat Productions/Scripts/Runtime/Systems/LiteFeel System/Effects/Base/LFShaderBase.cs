using DG.Tweening;
using RedRats.Core.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public abstract class LFShaderBase : LFEffectTweenBase
    {
        [Header("Target")] 
        [SerializeField, InfoBox("Missing target", InfoMessageType.Error, nameof(HasNoTarget))] private MaterialExtractor target;

        protected Material material;

        protected override void Initialize()
        {
            base.Initialize();
            material = target.Get();
        }

        /// <summary>
        /// Adds a shader property tween to the sequence.
        /// </summary>
        /// <param name="property">The shader property that is animated.</param>
        /// <param name="targetValue">The end value of the tween.</param>
        /// <param name="curve">Smoothing curve of the tween.</param>
        protected void AddFloatTween(int property, float targetValue, AnimationCurve curve)
        {
            Tween tween = DOTween.To(() => material.GetFloat(property), x => material.SetFloat(property, x), targetValue, Duration);
            AddTweenToSequence(tween, curve);
        }
        
        /// <summary>
        /// Adds a shader property tween to the sequence.
        /// </summary>
        /// <param name="property">The shader property that is animated.</param>
        /// <param name="targetValue">The end value of the tween.</param>
        /// <param name="curve">Smoothing curve of the tween.</param>
        protected void AddColorTween(int property, Color targetValue, AnimationCurve curve)
        {
            Tween tween = DOTween.To(() => material.GetColor(property), x => material.SetColor(property, x), targetValue, Duration);
            AddTweenToSequence(tween, curve);
        }
        
        protected override string FeedbackColor { get => "#FD6180"; }
        
        private bool HasNoTarget() => target == null; 
    }
}
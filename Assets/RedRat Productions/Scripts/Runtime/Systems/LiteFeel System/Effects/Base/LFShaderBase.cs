using DG.Tweening;
using RedRats.Core.Utils;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public abstract class LFShaderBase : LFEffectTweenMultipleBase
    {
        [Header("Target")] 
        [SerializeField] private MaterialExtractor target;

        protected Material material;

        protected override void Initialize()
        {
            material = target.Get();
        }

        /// <summary>
        /// Adds a shader property tween to the sequence.
        /// </summary>
        /// <param name="property">The shader property that is animated.</param>
        /// <param name="targetValue">The end value of the tween.</param>
        /// <param name="useEasingType">Will the tween use easing or animation curve.</param>
        /// <param name="easing">Easing type for the tween.</param>
        /// <param name="curve">Smoothing curve of the tween.</param>
        protected void AddPropertyTween(int property, float targetValue, bool useEasingType, Ease easing, AnimationCurve curve)
        {
            Tween tween = CreateTween(property, targetValue);
            AddTweenToSequence(tween, useEasingType, easing, curve);
        }
        
        private Tween CreateTween(int property, float targetValue) => DOTween.To(() => material.GetFloat(property), x => material.SetFloat(property, x), targetValue, duration);
    }
}
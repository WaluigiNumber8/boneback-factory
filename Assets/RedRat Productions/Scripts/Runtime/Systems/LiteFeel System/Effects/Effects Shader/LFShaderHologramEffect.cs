using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// An effect that makes the target look like a hologram.
    /// </summary>
    public class LFShaderHologramEffect : LFEffectShaderBase
    {
        [Header("Hologram Blend")]
        [SerializeField] private float beginBlend = 0f;
        [SerializeField] private float targetBlend = 1f;
        [SerializeField] protected AnimationCurve blendCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));

        private float startBlend;
        private static readonly int BlendProperty = Shader.PropertyToID("_HologramBlend");

        protected override void SetBeginState()
        {
            material.SetFloat(BlendProperty, beginBlend);
        }

        protected override void SetupTweens()
        {
            AddFloatTween(BlendProperty, targetBlend, blendCurve);
        }
        
        protected override void ResetTargetState()
        {
            material.SetFloat(BlendProperty, startBlend);
        }

        protected override void UpdateStartingValues()
        {
            startBlend = material.GetFloat(BlendProperty);
        }
        
    }
}
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFShaderFullColorEffect : LFShaderBase    
    {
        [Header("Color Blend")] 
        [SerializeField] private bool animateBlend = true;
        [SerializeField, EnableIf("animateBlend"), Range(0f, 1f)] private float beginBlend = 0f;
        [SerializeField, EnableIf("animateBlend"), Range(0f, 1f)] private float targetBlend = 1f;
        [SerializeField, EnableIf("animateBlend")] protected SmoothingType blendSmoothing = SmoothingType.Tween;
        [SerializeField, EnableIf("animateBlend"), HideIf("positionSmoothing", SmoothingType.AnimationCurve)] protected Ease blendEasing = Ease.InOutSine;
        [SerializeField, EnableIf("animateBlend"), HideIf("positionSmoothing", SmoothingType.Tween)] protected AnimationCurve blendCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        [Header("Glow")]
        [SerializeField] private bool animateGlow;
        [SerializeField, EnableIf("animateGlow"), Range(0f, 1f)] private float beginGlow = 0.1f;
        [SerializeField, EnableIf("animateGlow"), Range(0f, 1f)] private float targetGlow = 1f;
        [SerializeField, EnableIf("animateGlow")] protected SmoothingType glowSmoothing = SmoothingType.Tween;
        [SerializeField, EnableIf("animateGlow"), HideIf("glowSmoothing", SmoothingType.AnimationCurve)] protected Ease glowEasing = Ease.InOutSine;
        [SerializeField, EnableIf("animateGlow"), HideIf("glowSmoothing", SmoothingType.Tween)] protected AnimationCurve glowCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        protected override void ResetTargetState()
        {
            
        }

        protected override void UpdateStartingValues()
        {
            
        }

        protected override void SetupTweens()
        {
            
        }
    }
}
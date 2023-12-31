using DG.Tweening;
using RedRats.Core.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// An effect that makes the target look like a hologram.
    /// </summary>
    public class LFShaderHologramEffect : LFEffectTweenMultipleBase
    {
        [Header("Target")] 
        [SerializeField] private MaterialExtractor target;
        [SerializeField] protected SmoothingType smoothing = SmoothingType.Tween;
        [SerializeField, HideIf("smoothing", SmoothingType.AnimationCurve)] protected Ease easing = Ease.InOutSine;
        [SerializeField, HideIf("smoothing", SmoothingType.Tween)] protected AnimationCurve movementCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        protected override void SetupTweens(Sequence usedTweens, float duration)
        {
            
        }
        
        protected override void ResetTargetState()
        {
            
        }

        protected override void UpdateStartingValues()
        {
            
        }

        
    }
}
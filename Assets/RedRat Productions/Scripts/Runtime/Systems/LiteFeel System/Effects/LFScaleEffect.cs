using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// Allows to scale a specific target.
    /// </summary>
    public class LFScaleEffect : LFEffectTweenBase
    {
        [Header("Scale")] 
        [SerializeField] protected Transform target;
        [SerializeField, EnumToggleButtons] protected MovementType movement = MovementType.Absolute;
        [SerializeField, EnumToggleButtons] protected TransitionType mode = TransitionType.ToDestination;
        [SerializeField, HideIf("mode", TransitionType.ToDestination)] protected Vector3 beginScale;
        [SerializeField] protected Vector3 targetScale;
        [SerializeField] protected SmoothingType smoothing = SmoothingType.AnimationCurve;
        [SerializeField, HideIf("smoothing", SmoothingType.AnimationCurve)] protected Ease scaleEasing = Ease.InOutSine;
        [SerializeField, HideIf("smoothing", SmoothingType.Tween)] protected AnimationCurve scaleCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        private Vector3 startScale;
        
        protected override void Initialize()
        {
            // Do nothing.
        }
        
        protected override void SetBeginState()
        {
            if (mode == TransitionType.AtoB) target.localScale = beginScale;
        }

        protected override void SetupTweens()
        {
            Vector3 targetValue = (movement == MovementType.Relative) ? target.localScale + targetScale : targetScale;
            Tween tween = target.DOScale(targetValue, duration);
            AddFloatTweenToSequence(tween, smoothing, scaleEasing, scaleCurve);
        }

        protected override void ResetTargetState() => target.localScale = startScale;
        protected override void UpdateStartingValues() => startScale = target.localScale;
    }
}
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// Allows moving a specific target.
    /// </summary>
    public class LFPositionEffect : LFEffectTweenBase
    {
        [Header("Position")] 
        [SerializeField] protected Transform target;
        [SerializeField, EnumToggleButtons] protected MovementType movement = MovementType.Absolute;
        [SerializeField, EnumToggleButtons] protected WorldType worldType = WorldType.World;
        [SerializeField, EnumToggleButtons] protected TransitionType mode = TransitionType.AtoB;
        [SerializeField, HideIf("mode", TransitionType.ToDestination)] protected Vector3 beginPosition;
        [SerializeField] protected Vector3 targetPosition;
        [SerializeField] protected SmoothingType smoothing = SmoothingType.AnimationCurve;
        [SerializeField, HideIf("smoothing", SmoothingType.AnimationCurve)] protected Ease positionEasing = Ease.InOutSine;
        [SerializeField, HideIf("smoothing", SmoothingType.Tween)] protected AnimationCurve positionCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        protected Vector3 startPosition;
        protected Vector3 startLocalPosition;
        
        protected override void Initialize()
        {
            // Do nothing.
        }
        
        protected override void SetBeginState()
        {
            if (mode != TransitionType.AtoB) return;
            if (worldType == WorldType.World) target.position = beginPosition;
            else target.localPosition = beginPosition;
        }

        protected override void SetupTweens()
        {
            Vector3 targetValue = (movement == MovementType.Relative) ? GetPositionBasedOnSpace() + targetPosition : targetPosition;
            Tween tween = (worldType == WorldType.World) ? target.DOMove(targetValue, duration) : target.DOLocalMove(targetValue, duration);
            AddFloatTweenToSequence(tween, smoothing, positionEasing, positionCurve);
        }

        protected override void ResetTargetState()
        {
            if (worldType == WorldType.World) target.position = startPosition;
            else target.localPosition = startLocalPosition;
        }

        protected override void UpdateStartingValues()
        {
            startPosition = target.position;
            startLocalPosition = target.localPosition;
        }

        private Vector3 GetPositionBasedOnSpace() => (worldType == WorldType.World) ? target.position : target.localPosition;
    }
}
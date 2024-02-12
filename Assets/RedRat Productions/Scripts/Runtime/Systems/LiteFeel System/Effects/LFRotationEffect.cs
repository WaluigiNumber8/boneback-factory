using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    /// <summary>
    /// Allows to rotate a specific target.
    /// </summary>
    public class LFRotationEffect : LFEffectTweenBase
    {
        [Header("Rotation")] 
        [SerializeField] protected Transform target;
        [SerializeField, EnumToggleButtons] protected MovementType movement = MovementType.Absolute;
        [SerializeField, EnumToggleButtons] protected WorldType worldType = WorldType.World;
        [SerializeField, EnumToggleButtons] protected TransitionType mode = TransitionType.ToDestination;
        [SerializeField, HideIf("mode", TransitionType.ToDestination)] protected Vector3 beginRotation;
        [SerializeField] protected Vector3 targetRotation;
        [SerializeField] protected SmoothingType smoothing = SmoothingType.AnimationCurve;
        [SerializeField, HideIf("smoothing", SmoothingType.AnimationCurve)] protected Ease rotationEasing = Ease.InOutSine;
        [SerializeField, HideIf("smoothing", SmoothingType.Tween)] protected AnimationCurve rotationCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        private Vector3 startRotation;
        private Vector3 startLocalRotation;
        
        protected override void Initialize()
        {
            // Do nothing.
        }

        protected override void SetBeginState()
        {
            if (mode != TransitionType.AtoB) return;
            if (worldType == WorldType.World) target.eulerAngles = startRotation;
            else target.localEulerAngles = startLocalRotation;
        }

        protected override void SetupTweens()
        {
            Vector3 targetValue = (movement == MovementType.Relative) ? GetRotationBasedOnSpace() + targetRotation : targetRotation;
            Tween tween = (worldType == WorldType.World) ? target.DORotate(targetValue, duration, RotateMode.FastBeyond360) : target.DOLocalRotate(targetValue, duration, RotateMode.FastBeyond360);
            AddFloatTweenToSequence(tween, smoothing, rotationEasing, rotationCurve);
        }

        protected override void ResetTargetState()
        {
            if (worldType == WorldType.World) target.eulerAngles = beginRotation;
            else target.localEulerAngles = startLocalRotation;
        }

        protected override void UpdateStartingValues()
        {
            startRotation = target.eulerAngles;
            startLocalRotation = target.localEulerAngles;
        }

        private Vector3 GetRotationBasedOnSpace() => (worldType == WorldType.World) ? target.eulerAngles : target.localEulerAngles;
    }
}
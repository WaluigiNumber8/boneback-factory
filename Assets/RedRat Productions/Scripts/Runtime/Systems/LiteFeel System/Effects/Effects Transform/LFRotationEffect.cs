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
        [SerializeField, InfoBox("Missing target", InfoMessageType.Error, "@target == null")] private Transform target;
        [SerializeField, EnumToggleButtons] private MovementType movement = MovementType.Absolute;
        [SerializeField, EnumToggleButtons] private WorldType worldType = WorldType.Local;
        [SerializeField, EnumToggleButtons] private TransitionType mode = TransitionType.ToDestination;
        [SerializeField] private bool uniform;
        [SerializeField, HideIf("@uniform == true || mode == TransitionType.ToDestination")] private Vector3 beginRotation;
        [SerializeField, HideIf("@uniform == false || mode == TransitionType.ToDestination")] private float beginRotationU;
        [SerializeField, HideIf("uniform")] private Vector3 targetRotation;
        [SerializeField, ShowIf("uniform")] private float targetRotationU = 90;
        [SerializeField] private AnimationCurve rotationCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        private Vector3 startRotation;
        private Vector3 startLocalRotation;
        
        protected override void Initialize()
        {
            // Do nothing.
        }

        protected override void SetBeginState()
        {
            if (mode != TransitionType.AToB) return;
            if (worldType == WorldType.World) target.eulerAngles = startRotation;
            else target.localEulerAngles = startLocalRotation;
        }

        protected override void SetupTweens()
        {
            Vector3 targetValue = (movement == MovementType.Relative) ? GetRotationBasedOnSpace() + GetTargetRotation() : GetTargetRotation();
            Tween tween = (worldType == WorldType.World) ? target.DORotate(targetValue, duration, RotateMode.FastBeyond360) : target.DOLocalRotate(targetValue, duration, RotateMode.FastBeyond360);
            AddFloatTweenToSequence(tween, rotationCurve);
        }

        protected override void ResetTargetState()
        {
            if (worldType == WorldType.World) target.eulerAngles = GetBeginRotation();
            else target.localEulerAngles = startLocalRotation;
        }

        protected override void UpdateStartingValues()
        {
            startRotation = target.eulerAngles;
            startLocalRotation = target.localEulerAngles;
        }

        protected override string FeedbackColor { get => "#00FF9B"; }
        private Vector3 GetRotationBasedOnSpace() => (worldType == WorldType.World) ? target.eulerAngles : target.localEulerAngles;
        private Vector3 GetBeginRotation() => (uniform) ? Vector3.one * beginRotationU : beginRotation;
        private Vector3 GetTargetRotation() => (uniform) ? Vector3.one * targetRotationU : targetRotation;
    }
}
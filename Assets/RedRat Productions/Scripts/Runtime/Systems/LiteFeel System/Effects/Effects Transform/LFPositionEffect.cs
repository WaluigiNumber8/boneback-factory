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
        [SerializeField, InfoBox("Missing target", InfoMessageType.Error, "@target == null")] private Transform target;
        [SerializeField, EnumToggleButtons] private MovementType movement = MovementType.Relative;
        [SerializeField, EnumToggleButtons] private WorldType worldType = WorldType.Local;
        [SerializeField, EnumToggleButtons] private TransitionType mode = TransitionType.ToDestination;
        [SerializeField] private bool uniform;
        [SerializeField, HideIf("@uniform == true || mode == TransitionType.ToDestination")] private Vector3 beginPosition;
        [SerializeField, HideIf("@uniform == false || mode == TransitionType.ToDestination")] private float beginPositionU;
        [SerializeField, HideIf("uniform")] private Vector3 targetPosition;
        [SerializeField, ShowIf("uniform")] private float targetPositionU = 1;
        [SerializeField] private AnimationCurve positionCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        private Vector3 startPosition;
        private Vector3 startLocalPosition;
        
        protected override void Initialize()
        {
            // Do nothing.
        }

        protected override void SetBeginState()
        {
            if (mode != TransitionType.AToB) return;
            if (worldType == WorldType.World) target.position = GetBeginPosition();
            else target.localPosition = GetBeginPosition();
        }

        protected override void SetupTweens()
        {
            Vector3 targetValue = (movement == MovementType.Relative) ? GetPositionBasedOnSpace() + GetTargetPosition() : GetTargetPosition();
            Tween tween = (worldType == WorldType.World) ? target.DOMove(targetValue, Duration) : target.DOLocalMove(targetValue, Duration);
            AddTweenToSequence(tween, positionCurve);
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

        protected override string FeedbackColor { get => "#00FF9B"; }
        
        private Vector3 GetPositionBasedOnSpace() => (worldType == WorldType.World) ? target.position : target.localPosition;
        private Vector3 GetBeginPosition() => (uniform) ? Vector3.one * beginPositionU : beginPosition;
        private Vector3 GetTargetPosition() => (uniform) ? Vector3.one * targetPositionU : targetPosition;
    }
}
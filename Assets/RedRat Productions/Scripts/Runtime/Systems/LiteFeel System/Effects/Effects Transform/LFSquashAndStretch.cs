using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFSquashAndStretch : LFEffectTweenBase
    {
        [Header("Squash and Stretch")]
        [SerializeField, InfoBox("Missing target", InfoMessageType.Error, "@target == null")] private Transform target;
        [SerializeField, EnumToggleButtons] private TransitionType mode = TransitionType.ToDestination;
        [SerializeField, HideIf("mode", TransitionType.ToDestination)] private float beginScale = 1;
        [SerializeField] private float targetScale = 2;
        [SerializeField, EnumToggleButtons] private AxisType affectedAxis = AxisType.Y;
        [SerializeField] private SmoothingType smoothing = SmoothingType.AnimationCurve;
        [SerializeField, HideIf("smoothing", SmoothingType.AnimationCurve)] private Ease deformEasing = Ease.InOutSine;
        [SerializeField, HideIf("smoothing", SmoothingType.Tween)] private AnimationCurve deformCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        private Vector3 startScale;
        
        protected override void Initialize()
        {
            // Do nothing.
        }

        protected override void SetBeginState()
        {
            if (mode == TransitionType.ToDestination) return;
            Vector3 endScale = GetSquashedVector(target.localScale, beginScale);
            target.localScale = endScale;
        }

        protected override void SetupTweens()
        {
            Vector3 endScale = GetSquashedVector(target.localScale, targetScale);
            Tween tween = target.DOScale(endScale, duration);
            AddFloatTweenToSequence(tween, smoothing, deformEasing, deformCurve);
        }

        protected override void ResetTargetState()
        {
           target.localScale = startScale;
        }

        protected override void UpdateStartingValues()
        {
            startScale = target.localScale;
        }

        private Vector3 GetSquashedVector(Vector3 original, float squashValue)
        {
            return new()
            {
                x = (AffectX) ? squashValue : original.x / squashValue,
                y = (AffectY) ? squashValue : original.y / squashValue,
                z = (AffectZ) ? squashValue : original.z / squashValue
            };
        }
        private bool AffectX => (affectedAxis & AxisType.X) != 0;
        private bool AffectY => (affectedAxis & AxisType.Y) != 0;
        private bool AffectZ => (affectedAxis & AxisType.Z) != 0;
    }
}
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFCameraOrthographicSize : LFEffectTweenSingleBase<float>
    {
        [Header("Orthographic Size")]
        [SerializeField] protected float targetSize = 6f;
        [SerializeField, EnumToggleButtons] protected MovementType mode = MovementType.Absolute;

        private float startOrthographicSize;
        private Camera cam;

        private void Awake() => cam = Camera.main;

        protected override void Tween(float valueToReach, float duration, bool forceAbsolute = false)
        {
            float targetValue = (!forceAbsolute && mode == MovementType.Relative) ? cam.orthographicSize + valueToReach : valueToReach;
            tween = cam.DOOrthoSize(targetValue, duration);
        }

        protected override float GetStartingValue() => startOrthographicSize;
        protected override float GetTargetValue() => targetSize;
        protected override void ResetTargetState() => cam.orthographicSize = startOrthographicSize;
        protected override void UpdateStartingValues()
        {
            startOrthographicSize = cam.orthographicSize;
        }
    }
}
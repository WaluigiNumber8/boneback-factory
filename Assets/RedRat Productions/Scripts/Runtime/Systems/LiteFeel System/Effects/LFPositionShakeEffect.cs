using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFPositionShakeEffect : LFEffectTweenSingleBase<Vector3>
    {
        [Header("Target")]
        [SerializeField] private Transform target;
        
        [Header("Shake")] 
        [SerializeField] private bool uniform = true;
        [SerializeField, ShowIf("uniform")] private float strength = 0.1f;
        [SerializeField, HideIf("uniform")] private Vector3 strengthVector = Vector3.one * 0.1f;
        [SerializeField, Min(0)] private int vibration = 20;
        [SerializeField, Range(0f, 90f)] private float randomness = 90;
        [SerializeField] private bool fadeout;
        
        private Vector3 startLocalPosition;

        protected override void Tween(Vector3 valueToReach, float duration, bool forceAbsolute = false)
        {
            tween = target.DOShakePosition(duration, valueToReach, vibration, randomness, false, fadeout);
        }

        protected override Vector3 GetStartingValue() => startLocalPosition;
        protected override Vector3 GetTargetValue() => (uniform) ? Vector3.one * strength : strengthVector;
        protected override void Initialize()
        {
            // Nothing to do here.
        }

        protected override void ResetTargetState() => target.localPosition = startLocalPosition;
        protected override void UpdateStartingValues()
        {
            startLocalPosition = target.localPosition;
        }
    }
}
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFScaleShakeEffect : LFEffectTweenSingleBase<Vector3>
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
        
        private Vector3 startScale;

        protected override void Tween(Vector3 valueToReach, float duration, bool forceAbsolute = false)
        {
            tween = target.DOShakeScale(duration, valueToReach, vibration, randomness, fadeout);
        }

        protected override Vector3 GetStartingValue() => startScale;
        protected override Vector3 GetTargetValue() => (uniform) ? Vector3.one * strength : strengthVector;
        protected override void ResetTargetState() => target.localScale = startScale;
        protected override void UpdateStartingValues()
        {
            startScale = target.localScale;
        }
    }
}
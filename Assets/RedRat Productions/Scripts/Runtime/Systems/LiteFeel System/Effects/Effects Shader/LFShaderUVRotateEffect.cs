using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.Systems.LiteFeel.Effects
{
    public class LFShaderUVRotateEffect : LFEffectShaderBase
    {
        [Header("Rotation")]
        [SerializeField, LabelText("Mode"), EnumToggleButtons] protected TransitionType modeRotation = TransitionType.AToB;
        [SerializeField, HideIf("modeRotation", TransitionType.ToDestination), Range(0f, 360f)] private float beginRotation = 0f;
        [SerializeField, Range(0f, 360f)] private float targetRotation = 90f;
        [SerializeField] protected AnimationCurve rotationCurve = new(new Keyframe(0, 0), new Keyframe(1, 1));
        
        private static readonly int RotationProperty = Shader.PropertyToID("_UVRotation");
        
        private float startRotation;
        
        protected override void SetBeginState()
        {
            if (modeRotation == TransitionType.AToB) material.SetFloat(RotationProperty, beginRotation);
        }

        protected override void SetupTweens()
        {
            AddFloatTween(RotationProperty, targetRotation, rotationCurve);
        }

        protected override void ResetTargetState()
        {
            material.SetFloat(RotationProperty, startRotation);
        }

        protected override void UpdateStartingValues()
        {
            startRotation = material.GetFloat(RotationProperty);
        }
    }
}
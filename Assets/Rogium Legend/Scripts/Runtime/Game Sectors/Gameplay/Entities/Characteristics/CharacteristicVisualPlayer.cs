using System;
using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// A special visual characteristic allowing the player to have richer animations.
    /// </summary>
    public class CharacteristicVisualPlayer : CharacteristicBase
    {
        [SerializeField] private Animator animator;
        [SerializeField] private AnimatorPropertyData propertyNames;
        [SerializeField] private WeaponController weapon;

        // TODO Enable once animation for diagonal movement is solved
        // private void OnEnable()
        // {
        //     if (weapon == null) return;
        //     weapon.OnUse += PlayWeaponUse;
        //     weapon.OnUseStop += PlayWeaponUseStop;
        // }
        //
        // private void OnDisable()
        // {
        //     if (weapon == null) return;
        //     weapon.OnUse -= PlayWeaponUse;
        //     weapon.OnUseStop -= PlayWeaponUseStop;
        // }
        
        private void Update() => UpdateAnimator();

        public void PlayDeath() => animator.SetTrigger(propertyNames.onDeath);
        private void PlayWeaponUse() => animator.SetTrigger(propertyNames.onWeaponUse);
        private void PlayWeaponUseStop() => animator.SetTrigger(propertyNames.onWeaponUseStop);
        
        private void UpdateAnimator()
        {
            animator.SetFloat(propertyNames.FaceDirectionX, entity.FaceDirection.x);
            animator.SetFloat(propertyNames.FaceDirectionY, entity.FaceDirection.y);
            animator.SetFloat(propertyNames.MoveSpeed, entity.CurrentSpeed);
        }

        [Serializable]
        public struct AnimatorPropertyData
        {
            public string FaceDirectionX;
            public string FaceDirectionY;
            public string MoveSpeed;
            public string onDeath;
            public string onWeaponUse;
            public string onWeaponUseStop;
        }
        
    }
}
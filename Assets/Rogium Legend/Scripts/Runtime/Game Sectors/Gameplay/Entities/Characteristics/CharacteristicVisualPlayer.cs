using System;
using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// A special visual characteristic allowing the player to have richer animations.
    /// </summary>
    public class CharacteristicVisualPlayer : CharacteristicBase
    {
        public event Action OnFrameChange;
        
        [SerializeField] private Animator animator;
        [SerializeField] private AnimatorPropertyData propertyNames;
        
        private void Update() => UpdateAnimator();

        /// <summary>
        /// Play the Death animation.
        /// </summary>
        public void PlayDeath() => animator.SetTrigger(propertyNames.onDeath);

        /// <summary>
        /// Calls when the frame changes. Intended to be used with the Animator.
        /// </summary>
        public void CallFrameChange() => OnFrameChange?.Invoke();
        
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
        }
        
    }
}
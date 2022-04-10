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
        
        private void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            animator.SetFloat(propertyNames.FaceDirectionX, entity.FaceDirection.x);
            animator.SetFloat(propertyNames.FaceDirectionY, entity.FaceDirection.y);
            animator.SetFloat(propertyNames.MoveSpeed, entity.CurrentSpeed);
        }

        [System.Serializable]
        public struct AnimatorPropertyData
        {
            public string FaceDirectionX;
            public string FaceDirectionY;
            public string MoveSpeed;
        }
        
    }
}
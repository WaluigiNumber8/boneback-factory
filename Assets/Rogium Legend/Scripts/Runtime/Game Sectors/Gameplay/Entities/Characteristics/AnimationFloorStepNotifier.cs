using UnityEngine;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Notifies a <see cref="CharacteristicFloorInteractor"/> when during an animation the entity steps on the floor. 
    /// </summary>
    public class AnimationFloorStepNotifier : MonoBehaviour
    {
        private CharacteristicFloorInteractor floorInteractor;
        
        private void Awake() => floorInteractor = GetComponentInParent<CharacteristicFloorInteractor>();

        public void NotifyFloorStep() => floorInteractor.TakeStep();
    }
}
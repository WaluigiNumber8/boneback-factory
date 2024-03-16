using Rogium.Gameplay.TilemapInteractions;
using UnityEngine.Tilemaps;

namespace Rogium.Gameplay.Entities.Characteristics
{
    /// <summary>
    /// Allows entities to interact with the floor <see cref="Tilemap"/>.
    /// </summary>
    public class CharacteristicFloorInteractor : CharacteristicBase
    {
        private FloorInteractionService interactionService;

        private void Awake() => interactionService = FindFirstObjectByType<FloorInteractionService>();

        /// <summary>
        /// Runs processes that happen when the entity takes a step on the floor.
        /// </summary>
        public void TakeStep()
        {
            if (interactionService == null) return;
            interactionService.TriggerFloorEffect(entity.Transform);
        }
    }
}
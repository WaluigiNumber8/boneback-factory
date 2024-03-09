using RedRats.Systems.LiteFeel.Core;
using Rogium.Editors.Tiles;
using Rogium.Gameplay.TilemapInteractions;
using UnityEngine;

namespace Rogium.Systems.LiteFeel.Brains
{
    /// <summary>
    /// Allocates LF effects to footsteps.
    /// </summary>
    public class LFBrainFootsteps : MonoBehaviour
    {
        [SerializeField] private FloorInteractionService interactionService;
        [Space] 
        [SerializeField] private LFEffector tileStepEffector;
        [SerializeField] private LFEffector woodStepEffector;
        [SerializeField] private LFEffector stoneStepEffector;
        [SerializeField] private LFEffector grassStepEffector;
        [SerializeField] private LFEffector dirtStepEffector;
        [SerializeField] private LFEffector sandStepEffector;
        [SerializeField] private LFEffector carpetStepEffector;
        [SerializeField] private LFEffector waterStepEffector;

        private void OnEnable() => interactionService.OnFootstep += PlayEffect;
        private void OnDisable() => interactionService.OnFootstep -= PlayEffect;

        private void PlayEffect(TerrainType terrain)
        {
            LFEffector effect = GetEffect(terrain);
            if (effect == null) return;
            effect.Play();
        }

        private LFEffector GetEffect(TerrainType terrain)
        {
            return terrain switch
            {
                TerrainType.Tile => tileStepEffector,
                TerrainType.Wood => woodStepEffector,
                TerrainType.Stone => stoneStepEffector,
                TerrainType.Grass => grassStepEffector,
                TerrainType.Dirt => dirtStepEffector,
                TerrainType.Sand => sandStepEffector,
                TerrainType.Carpet => carpetStepEffector,
                TerrainType.ShallowWater => waterStepEffector,
                _ => null
            };
        }
    }
}
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
        [SerializeField] private LFEffector metalStepEffector;
        [SerializeField] private LFEffector glassStepEffector;
        [SerializeField] private LFEffector carpetStepEffector;
        [SerializeField] private LFEffector dirtStepEffector;
        [SerializeField] private LFEffector grassStepEffector;
        [SerializeField] private LFEffector sandStepEffector;
        [SerializeField] private LFEffector waterStepEffector;
        [SerializeField] private LFEffector snowStepEffector;

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
                TerrainType.Metal => metalStepEffector,
                TerrainType.Glass => glassStepEffector,
                TerrainType.Carpet => carpetStepEffector,
                TerrainType.Dirt => dirtStepEffector,
                TerrainType.Grass => grassStepEffector,
                TerrainType.Sand => sandStepEffector,
                TerrainType.ShallowWater => waterStepEffector,
                TerrainType.Snow => snowStepEffector,
                _ => null
            };
        }
    }
}
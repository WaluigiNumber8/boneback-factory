using RedRats.Systems.LiteFeel.Core;
using Rogium.Editors.Tiles;
using Rogium.Gameplay.TilemapInteractions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Systems.LiteFeel.Brains
{
    /// <summary>
    /// Allocates LF effects to footsteps.
    /// </summary>
    public class LFBrainFootsteps : MonoBehaviour
    {
        [SerializeField, GUIColor(0.85f, 0.8f, 0f)] private FloorInteractionService interactionService;
        [Space] 
        [SerializeField, GUIColor(0.1f, 1f, 0f)] private LFEffector tileStepEffector;
        [SerializeField, GUIColor(0.1f, 1f, 0f)] private LFEffector woodStepEffector;
        [SerializeField, GUIColor(0.1f, 1f, 0f)] private LFEffector metalStepEffector;
        [SerializeField, GUIColor(0.1f, 1f, 0f)] private LFEffector glassStepEffector;
        [SerializeField, GUIColor(0.1f, 1f, 0f)] private LFEffector carpetStepEffector;
        [SerializeField, GUIColor(0.1f, 1f, 0f)] private LFEffector dirtStepEffector;
        [SerializeField, GUIColor(0.1f, 1f, 0f)] private LFEffector grassStepEffector;
        [SerializeField, GUIColor(0.1f, 1f, 0f)] private LFEffector sandStepEffector;
        [SerializeField, GUIColor(0.1f, 1f, 0f)] private LFEffector waterStepEffector;
        [SerializeField, GUIColor(0.1f, 1f, 0f)] private LFEffector snowStepEffector;

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
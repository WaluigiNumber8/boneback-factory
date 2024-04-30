using RedRats.Systems.LiteFeel.Core;
using RedRats.Systems.LiteFeel.Effects;
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
        [SerializeField, Required, GUIColor(0.85f, 0.8f, 0f)] private FloorInteractionService interactionService;
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

        private LFAudioEffect tileAudio, woodAudio, metalAudio, glassAudio, carpetAudio, dirtAudio, grassAudio, 
                              sandAudio, waterAudio, snowAudio;

        private void Awake()
        {
            tileAudio = tileStepEffector.GetComponent<LFAudioEffect>();
            woodAudio = woodStepEffector.GetComponent<LFAudioEffect>();
            metalAudio = metalStepEffector.GetComponent<LFAudioEffect>();
            glassAudio = glassStepEffector.GetComponent<LFAudioEffect>();
            carpetAudio = carpetStepEffector.GetComponent<LFAudioEffect>();
            dirtAudio = dirtStepEffector.GetComponent<LFAudioEffect>();
            grassAudio = grassStepEffector.GetComponent<LFAudioEffect>();
            sandAudio = sandStepEffector.GetComponent<LFAudioEffect>();
            waterAudio = waterStepEffector.GetComponent<LFAudioEffect>();
            snowAudio = snowStepEffector.GetComponent<LFAudioEffect>();
        }

        private void OnEnable() => interactionService.OnFootstep += PlayEffect;
        private void OnDisable() => interactionService.OnFootstep -= PlayEffect;

        private void PlayEffect( Transform entity, TerrainType terrain)
        {
            LFEffector effect = GetEffect(terrain);
            if (effect == null) return;
            UpdateTargetOfFirstAudioEffect(terrain, entity);
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

        private void UpdateTargetOfFirstAudioEffect(TerrainType terrain, Transform soundTarget)
        {
            switch (terrain)
            {
                case TerrainType.Tile:
                    tileAudio.ChangeSoundTarget(soundTarget);
                    break;
                case TerrainType.Wood:
                    woodAudio.ChangeSoundTarget(soundTarget);
                    break;
                case TerrainType.Metal:
                    metalAudio.ChangeSoundTarget(soundTarget);
                    break;
                case TerrainType.Glass:
                    glassAudio.ChangeSoundTarget(soundTarget);
                    break;
                case TerrainType.Carpet:
                    carpetAudio.ChangeSoundTarget(soundTarget);
                    break;
                case TerrainType.Dirt:
                    dirtAudio.ChangeSoundTarget(soundTarget);
                    break;
                case TerrainType.Grass:
                    grassAudio.ChangeSoundTarget(soundTarget);
                    break;
                case TerrainType.Sand:
                    sandAudio.ChangeSoundTarget(soundTarget);
                    break;
                case TerrainType.ShallowWater:
                    waterAudio.ChangeSoundTarget(soundTarget);
                    break;
                case TerrainType.Snow:
                    snowAudio.ChangeSoundTarget(soundTarget);
                    break;
            }
        }
    }
}
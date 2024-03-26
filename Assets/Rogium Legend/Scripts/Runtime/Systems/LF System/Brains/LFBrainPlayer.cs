using RedRats.Systems.LiteFeel.Core;
using Rogium.Gameplay.Entities.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Systems.LiteFeel.Brains
{
    public class LFBrainPlayer : MonoBehaviour
    {
        [SerializeField, GUIColor(0.85f, 0.8f, 0f)] private PlayerController player;
        [Space] 
        [SerializeField, GUIColor(0.1f, 0.5f, 1f)] private LFEffector onTurnEffector;

        private void OnEnable()
        {
            if (onTurnEffector != null) player.OnTurn += onTurnEffector.Play;
        }

        private void OnDisable()
        {
            if (onTurnEffector != null) player.OnTurn -= onTurnEffector.Play;
        }

    }
}
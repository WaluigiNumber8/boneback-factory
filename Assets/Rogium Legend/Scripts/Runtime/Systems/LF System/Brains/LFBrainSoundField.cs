using RedRats.Systems.LiteFeel.Core;
using Rogium.UserInterface.Interactables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Systems.LiteFeel.Brains
{
    /// <summary>
    /// Allocates LF effects to an <see cref="SoundField"/>.
    /// </summary>
    public class LFBrainSoundField : MonoBehaviour
    {
        [SerializeField, GUIColor(0.85f, 0.8f, 0f)] private SoundField soundField;
        [Space] 
        [SerializeField, GUIColor(0.1f, 0.5f, 1f)] private LFEffector onClearEffector;
        
        private void OnEnable()
        {
            if (onClearEffector != null) soundField.OnValueEmptied += onClearEffector.Play;
        }
        
        private void OnDisable()
        {
            if (onClearEffector != null) soundField.OnValueEmptied -= onClearEffector.Play;
        }
    }
}
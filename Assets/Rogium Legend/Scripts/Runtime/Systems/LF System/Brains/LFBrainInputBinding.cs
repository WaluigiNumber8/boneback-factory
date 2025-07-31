using RedRats.Systems.LiteFeel.Core;
using Rogium.UserInterface.Interactables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Systems.LiteFeel.Brains
{
    /// <summary>
    /// Allocates LF effects to <see cref="InputBindingReader"/>.
    /// </summary>
    public class LFBrainInputBinding : MonoBehaviour
    {
        [SerializeField, Required, GUIColor(0.85f, 0.8f, 0f)] private InputBindingReader inputReader;
        [Space] 
        [SerializeField, GUIColor(0.1f, 0.5f, 1f)] private LFEffector onRebindStartEffector;
        [SerializeField, GUIColor(0.1f, 0.5f, 1f)] private LFEffector onRebindEndEffector;
        [SerializeField, GUIColor(0.1f, 0.5f, 1f)] private LFEffector onClearEffector;
        
        private void OnEnable()
        {
            if (onRebindStartEffector != null) inputReader.OnRebindStart += onRebindStartEffector.Play;
            if (onRebindEndEffector != null) inputReader.OnRebindEnd += onRebindEndEffector.Play;
            if (onClearEffector != null) inputReader.OnClear += onClearEffector.Play;
        }

        private void OnDisable()
        {
            if (onRebindStartEffector != null) inputReader.OnRebindStart -= onRebindStartEffector.Play;
            if (onRebindEndEffector != null) inputReader.OnRebindEnd -= onRebindEndEffector.Play;
            if (onClearEffector != null) inputReader.OnClear -= onClearEffector.Play;
        }
    }
}
using RedRats.Systems.LiteFeel.Core;
using Rogium.UserInterface.Interactables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Systems.LiteFeel.Brains
{
    /// <summary>
    /// Allocates LF effects to an <see cref="AssetField"/>.
    /// </summary>
    public class LFBrainAssetField : MonoBehaviour
    {
        [SerializeField, GUIColor(0.85f, 0.8f, 0f)] private AssetField assetField;
        [Space] 
        [SerializeField, GUIColor(0.1f, 0.5f, 1f)] private LFEffector onClearEffector;
        
        private void OnEnable()
        {
            if (onClearEffector != null) assetField.OnValueEmptied += onClearEffector.Play;
        }
        
        private void OnDisable()
        {
            if (onClearEffector != null) assetField.OnValueEmptied -= onClearEffector.Play;
        }
    }
}
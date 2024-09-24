using UnityEngine;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Controls a single instance of a selection menu.
    /// </summary>
    public class AssetSelector : MonoBehaviour
    {
        [SerializeField] private RectTransform content;
        
        
        public RectTransform Content { get => content; }
    }
}
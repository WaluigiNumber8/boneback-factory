using System;
using RedRats.Core;
using Rogium.Core;
using UnityEngine;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Overseers the Asset Selection Menu.
    /// </summary>
    public sealed class SelectionMenuOverseerMono : MonoSingleton<SelectionMenuOverseerMono>
    {
        [SerializeField] private AssetSelectorInfo selectors;
        
        private AssetSelector currentSelector;
        
        public void Open(AssetType type)
        {
            // Open the selection menu.
            currentSelector = type switch
            {
                AssetType.Pack => selectors.packSelector,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public AssetSelector CurrentSelector { get => currentSelector; }

        [Serializable]
        public struct AssetSelectorInfo
        {
            public AssetSelector packSelector;
        }
    }
}
using System;
using System.Linq;
using RedRats.UI.Tabs;
using Rogium.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// A special version of <see cref="TabGroup"/> that links <see cref="AssetType"/> to <see cref="TabPageButton"/>s.
    /// </summary>
    public class AssetTabGroup : TabGroupBase
    {
        [SerializeField] private AssetTabPair[] buttonPairs;
        
        public void Switch(AssetType type) => Switch(buttonPairs.First(pair => pair.type == type).button);
        public new void Switch(int index) => base.Switch(index);
        
        protected override TabPageButton[] GetButtons() => buttonPairs.Select(pair => pair.button).ToArray();
        
        [Serializable]
        public struct AssetTabPair
        {
            [HorizontalGroup, HideLabel] 
            public AssetType type;
            [HorizontalGroup, HideLabel] 
            public TabPageButton button;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Core;
using Rogium.Editors.Core;
using UnityEngine;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Overseers the Asset Selection Menu.
    /// </summary>
    public sealed partial class SelectionMenuOverseerMono : MonoSingleton<SelectionMenuOverseerMono>
    {
        [SerializeField] private SelectionMenuData packSelection;

        private IDictionary<AssetType, SelectionMenuFullData> menuData;
        private AssetType currentType;

        protected override void Awake()
        {
            base.Awake();
            menuData = new Dictionary<AssetType, SelectionMenuFullData>
            {
                {AssetType.Pack, new SelectionMenuFullData(packSelection, ExternalLibraryOverseer.Instance.Packs.Cast<IAsset>().ToList)}
            };
        }

        public void Open(AssetType type)
        {
            currentType = type;
            GetData(currentType).Load();
        }
        
        private SelectionMenuFullData GetData(AssetType type)
        {
            SafetyNet.EnsureDictionaryContainsKey(menuData, type, nameof(menuData));
            return menuData[type];
        }

        public AssetSelector CurrentSelector { get => GetData(currentType).Data.assetSelector; }
    }
}
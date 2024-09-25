using System;
using System.Collections.Generic;
using Rogium.Editors.Core;

namespace Rogium.Editors.NewAssetSelection
{
    public sealed partial class SelectionMenuOverseerMono
    {
        private readonly struct SelectionMenuFullData
        {
            private readonly SelectionMenuData data;
            private readonly Func<IList<IAsset>> getAssetList;

            public SelectionMenuFullData(SelectionMenuData data, Func<IList<IAsset>> getAssetList)
            {
                this.data = data;
                this.getAssetList = getAssetList;
            }

            public void Load() => data.assetSelector.Load(getAssetList.Invoke(), data.cardData);
            
            public SelectionMenuData Data { get => data; }
        }
    }
}
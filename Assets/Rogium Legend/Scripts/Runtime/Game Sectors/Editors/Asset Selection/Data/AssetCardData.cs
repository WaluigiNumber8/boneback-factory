using System;
using Rogium.UserInterface.Interactables;
using UnityEngine;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Contains data needed to setup an <see cref="AssetCardControllerV2"/>.
    /// </summary>
    [Serializable]
    public class AssetCardData
    {
        public string title;
        public Sprite icon;
        public ButtonType whenAssetEdit;
        public ButtonType whenAssetConfig;
        public ButtonType whenAssetDelete;

        private AssetCardData() { }
        
        public class Builder
        {
            private readonly AssetCardData data = new();

            public Builder()
            {
                data.title = string.Empty;
                data.icon = null;
                data.whenAssetEdit = ButtonType.None;
                data.whenAssetConfig = ButtonType.None;
                data.whenAssetDelete = ButtonType.None;
            }
            
            public Builder WithTitle(string title)
            {
                data.title = title;
                return this;
            }
            
            public Builder WithIcon(Sprite icon)
            {
                data.icon = icon;
                return this;
            }
            
            public Builder WithEditButton(ButtonType buttonType)
            { 
                data.whenAssetEdit = buttonType;
                return this;
            }
        
            public Builder WithConfigButton(ButtonType buttonType)
            {
                data.whenAssetConfig = buttonType;
                return this;
            }
        
            public Builder WithDeleteButton(ButtonType buttonType)
            {
                data.whenAssetDelete = buttonType;
                return this;
            }
        
            public AssetCardData Build() => data;
        }
    }
}
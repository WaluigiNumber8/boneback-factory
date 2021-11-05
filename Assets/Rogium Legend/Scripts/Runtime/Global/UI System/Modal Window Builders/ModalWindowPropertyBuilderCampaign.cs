using System;
using BoubakProductions.UI;
using Rogium.Editors.Campaign;
using Rogium.Editors.Packs;
using Rogium.Global.UISystem.AssetSelection;

namespace Rogium.Global.UISystem.UI
{
    /// <summary>
    /// Constructor for the Campaign Properties Modal Window
    /// </summary>
    public class ModalWindowPropertyBuilderCampaign : ModalWindowPropertyBuilder
    {
        public override void OpenForCreate()
        {
            //TODO Remove temporary solution for getting pack.
            OpenWindow(new CampaignAsset(LibraryOverseer.Instance.GetPacksCopy[0]), CreateAsset, "Creating a new campaign");
        }

        public override void OpenForUpdate()
        {
            throw new System.NotImplementedException();
        }

        private void OpenWindow(CampaignAsset asset, Action onConfirmButton, string headerText)
        {
            propertyBuilder.BuildInputField("Name", asset.Title, window.FirstColumnContent, asset.UpdateTitle);
            propertyBuilder.BuildPlainText("Created by", asset.Author, window.FirstColumnContent);
            propertyBuilder.BuildPlainText("Created on", asset.CreationDate.ToString(), window.FirstColumnContent);

            editedAssetBase = asset;
            window.OpenAsPropertiesColumn1(headerText, ThemeType.Red, "Done", "Cancel", onConfirmButton, true);
        }
        
        protected override void CreateAsset()
        {
            lib.CreateAndAddCampaign((CampaignAsset)editedAssetBase);
            CampaignAssetSelectionOverseer.Instance.SelectCampaignLast();
        }

        protected override void UpdateAsset()
        {
            throw new System.NotImplementedException();
        }
    }
}
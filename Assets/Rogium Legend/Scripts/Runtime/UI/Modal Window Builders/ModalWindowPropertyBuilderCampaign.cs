using System;
using BoubakProductions.UI;
using Rogium.Editors.Campaign;
using Rogium.Editors.Packs;
using Rogium.UserInterface.AssetSelection;

namespace Rogium.UserInterface.UI
{
    /// <summary>
    /// Constructor for the Campaign Properties Modal Window
    /// </summary>
    public class ModalWindowPropertyBuilderCampaign : ModalWindowPropertyBuilder
    {
        private CampaignEditorOverseer campaignEditor;

        public ModalWindowPropertyBuilderCampaign()
        {
            campaignEditor = CampaignEditorOverseer.Instance;;
        }
        
        public override void OpenForCreate()
        {
            OpenWindow(new CampaignAsset(), CreateAsset, "Creating a new campaign");
        }

        public override void OpenForUpdate()
        {
            OpenWindow(new CampaignAsset(campaignEditor.CurrentCampaign), UpdateAsset, $"Updating {campaignEditor.CurrentCampaign.Title}");
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
            campaignEditor.UpdateAsset((CampaignAsset)editedAssetBase);
            campaignEditor.CompleteEditing();
            CampaignAssetSelectionOverseer.Instance.SelectAgain();
        }
    }
}
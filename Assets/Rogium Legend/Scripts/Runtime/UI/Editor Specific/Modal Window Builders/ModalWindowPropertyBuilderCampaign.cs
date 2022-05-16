using System;
using System.Collections.Generic;
using System.Linq;
using RedRats.UI;
using Rogium.Editors.Campaign;
using Rogium.Editors.Packs;
using Rogium.Systems.GASExtension;
using Rogium.UserInterface.Editors.AssetSelection;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Campaign Properties Modal Window
    /// </summary>
    public class ModalWindowPropertyBuilderCampaign : ModalWindowPropertyBuilder
    {
        private readonly CampaignEditorOverseer campaignEditor;
        private readonly string[] lengthOptions;
        private readonly IDictionary<int, int> conversionData;

        public ModalWindowPropertyBuilderCampaign()
        {
            campaignEditor = CampaignEditorOverseer.Instance;
            lengthOptions = new[]
            {
                "(10) Short",
                "(25) Normal",
                "(35) Long",
                "(50) Marathon"
            };
            conversionData = new Dictionary<int, int>();
            conversionData.Add(0, 12);
            conversionData.Add(1, 25);
            conversionData.Add(2, 35);
            conversionData.Add(3, 50);
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
            b.BuildInputField("Name", asset.Title, window.FirstColumnContent, asset.UpdateTitle);
            b.BuildDropdown("Length", lengthOptions, QuickConvertToTier(asset.AdventureLength), window.FirstColumnContent, i => asset.UpdateLength(QuickConvertToRoomCount(i)));
            b.BuildPlainText("Created by", asset.Author, window.FirstColumnContent);
            b.BuildPlainText("Created on", asset.CreationDate.ToString(), window.FirstColumnContent);

            editedAssetBase = asset;
            window.OpenAsPropertiesColumn1(headerText, ThemeType.Red, "Done", "Cancel", onConfirmButton, true);
        }
        
        protected override void CreateAsset()
        {
            lib.CreateAndAddCampaign((CampaignAsset)editedAssetBase);
            CampaignAssetSelectionOverseer.Instance.SelectCampaignLast();
            GASButtonActions.OpenEditorCampaign(lib.CampaignCount-1);
        }

        protected override void UpdateAsset()
        {
            campaignEditor.UpdateAsset((CampaignAsset)editedAssetBase);
            campaignEditor.CompleteEditing();
            CampaignAssetSelectionOverseer.Instance.SelectAgain();
        }

        /// <summary>
        /// Quickly (and dirtily) converts the chosen length tier of the campaign.
        /// </summary>
        /// <param name="lengthTier">The length tier chosen from a dropdown.</param>
        /// <returns>Returns the proper amount of rooms.</returns>
        private int QuickConvertToRoomCount(int lengthTier) => conversionData[lengthTier];
        /// <summary>
        /// Quickly (dirtily) converts the chosen room count to a difficulty tier.
        /// </summary>
        /// <param name="roomCount">The amount of rooms.</param>
        /// <returns></returns>
        private int QuickConvertToTier(int roomCount) => conversionData.First(pair => pair.Value == roomCount).Key;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Rogium.Editors.Campaign;
using Rogium.Editors.Core;
using UnityEngine;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Campaign Properties Modal Window
    /// </summary>
    public class ModalWindowPropertyBuilderCampaign : ModalWindowPropertyBuilderBase
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

        public override void OpenForCreate(Action whenConfirm = null) => OpenWindow(new CampaignAsset.Builder().Build() , () => CreateAsset(whenConfirm), "Creating a new campaign", AssetModificationType.Create);

        public override void OpenForUpdate(Action whenConfirm = null) => OpenWindow(new CampaignAsset.Builder().AsCopy(campaignEditor.CurrentAsset).Build(), () => UpdateAsset(whenConfirm), $"Updating {campaignEditor.CurrentAsset.Title}", AssetModificationType.Update);

        public override void OpenForClone(Action whenConfirm = null) => OpenWindow(new CampaignAsset.Builder().AsClone(campaignEditor.CurrentAsset).Build(), () => CloneAsset(whenConfirm), $"Creating a clone of {campaignEditor.CurrentAsset.Title}", AssetModificationType.Clone);
        
        private void OpenWindow(CampaignAsset asset, Action onConfirm, string headerText, AssetModificationType modification)
        {
            asset.UpdateTitle(GetTitleByModificationType(asset, modification));
            OpenForColumns1(headerText, onConfirm, out Transform col);
            
            b.BuildInputField("Name", asset.Title, col, asset.UpdateTitle);
            b.BuildDropdown("Length", lengthOptions, QuickConvertToTier(asset.AdventureLength), col, i => asset.UpdateLength(QuickConvertToRoomCount(i)));
            b.BuildPlainText("Created by", asset.Author, col);
            b.BuildPlainText("Created on", asset.CreationDate.ToString(), col);

            editedAssetBase = asset;
        }

        protected override void CreateAsset(Action whenConfirm)
        {
            ExternalLibraryOverseer.Instance.CreateAndAddCampaign((CampaignAsset) editedAssetBase);
            whenConfirm?.Invoke();
        }

        protected override void UpdateAsset(Action whenConfirm)
        {
            campaignEditor.UpdateAsset((CampaignAsset) editedAssetBase);
            campaignEditor.CompleteEditing();
            whenConfirm?.Invoke();
        }
        
        protected override void CloneAsset(Action whenConfirm)
        {
            ExternalLibraryOverseer.Instance.CreateAndAddCampaign((CampaignAsset) editedAssetBase);
            campaignEditor.UpdateAsset((CampaignAsset) editedAssetBase);
            campaignEditor.CompleteEditing();
            whenConfirm?.Invoke();
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
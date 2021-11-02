using System;
using Rogium.Editors.Campaign;
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
            OpenWindow(new CampaignAsset(), CreateAsset, "Creating a new campaign");
        }

        public override void OpenForUpdate()
        {
            throw new System.NotImplementedException();
        }

        private void OpenWindow(CampaignAsset asset, Action onConfirmButton, string headerText)
        {
            
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
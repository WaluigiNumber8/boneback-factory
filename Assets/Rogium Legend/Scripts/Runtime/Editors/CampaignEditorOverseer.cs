using Rogium.Editors.Campaign;
using UnityEngine;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// Overseers the work on a given campaign.
    /// </summary>
    public class CampaignEditorOverseer : IEditorOverseer
    {
        private CampaignAsset currentCampaign;
        private int myIndex;
        
        #region Singleton Pattern
        private static CampaignEditorOverseer instance;
        private static readonly object padlock = new object();

        public static CampaignEditorOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new CampaignEditorOverseer();
                    return instance;
                }
            }
        }

        #endregion

        public void AssignAsset(CampaignAsset campaign, int index)
        {
            currentCampaign = new CampaignAsset(campaign);
            myIndex = index;
        }
        
        public void CompleteEditing()
        {
            throw new System.NotImplementedException();
        }
        
        public CampaignAsset CurrentCampaign
        {
            get 
            {
                if (currentCampaign == null) throw new MissingReferenceException("Current Pack has not been set. Did you forget to activate the editor?");
                return this.CurrentCampaign;
            }
        }
        
    }
}
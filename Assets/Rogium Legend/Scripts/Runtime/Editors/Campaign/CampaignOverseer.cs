using System;
using BoubakProductions.Safety;
using UnityEngine;

namespace Rogium.Editors.Campaign
{
    /// <summary>
    /// Overseers everything happening in the Campaign Editor.
    /// </summary>
    public class CampaignOverseer : IEditorOverseer
    {
        public event Action OnCompleteEditing;
        
        private CampaignAsset currentCampaign;

        #region Singleton Pattern
        private static CampaignOverseer instance;
        private static readonly object padlock = new object();

        public static CampaignOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new CampaignOverseer();
                    return instance;
                }
            }
        }

        #endregion

        private CampaignOverseer()
        {
        }

        public void AssignAsset(CampaignAsset campaign)
        {
            SafetyNet.EnsureIsNotNull(campaign, "Assigned Campaign");
            currentCampaign = new CampaignAsset(campaign);
        }
        
        public void CompleteEditing()
        {
            OnCompleteEditing?.Invoke();
        }
    
        public CampaignAsset CurrentCampaign
        {
            get 
            {
                if (currentCampaign == null) throw new MissingReferenceException("Current Campaign has not been set. Did you forget to activate the editor?");
                return this.currentCampaign;
            }
        }
    }
}
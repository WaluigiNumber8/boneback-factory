using BoubakProductions.Core;
using BoubakProductions.Safety;
using Rogium.Editors.Campaign;
using UnityEngine;

namespace Rogium.Global.SceneTransferService
{
    public class SceneTransferOverseer : MonoSingleton<SceneTransferOverseer>
    {
        private CampaignAsset campaign;

        protected override void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// Load up things for transfer.
        /// </summary>
        /// <param name="campaign">The Campaign Asset to load up for transfer.</param>
        public void LoadUp(CampaignAsset campaign)
        {
            SafetyNet.EnsureIsNotNull(campaign, "Loaded up campaign");
            SafetyNet.EnsureIsNotNull(campaign.DataPack, "Loaded up campaign's DataPack.");
            this.campaign = new CampaignAsset(campaign);
        }

        /// <summary>
        /// Pick up the stored campaign.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MissingReferenceException">Is Thrown when campaign is null.</exception>
        public CampaignAsset PickUpCampaign()
        {
            if (campaign == null) throw new MissingReferenceException("No campaign has been register at the Transfer Service.");
            return campaign;
        }
    }
}
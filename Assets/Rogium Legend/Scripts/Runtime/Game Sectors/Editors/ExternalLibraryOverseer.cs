using System.Collections.Generic;
using System.Collections.ObjectModel;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Core;
using Rogium.Editors.Campaign;
using Rogium.Editors.Packs;
using Rogium.ExternalStorage;
using Rogium.Options.Core;
using Rogium.Systems.Input;
using Rogium.Systems.SceneTransferService;
using static Rogium.Editors.Packs.AssetAssociation;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// Overseers the main in-game saveable assets library and controls their content.
    /// This library is also synced with asset data files located on the external hard drive.
    /// </summary>
    public sealed class ExternalLibraryOverseer : Singleton<ExternalLibraryOverseer>
    {
        private readonly IExternalStorageOverseer ex = ExternalCommunicator.Instance;
        private readonly PackEditorOverseer packEditor = PackEditorOverseer.Instance;
        private readonly CampaignEditorOverseer campaignEditor = CampaignEditorOverseer.Instance;
        private readonly OptionsMenuOverseer optionsEditor = OptionsMenuOverseer.Instance;
        
        private readonly AssetList<PackAsset> packs;
        private readonly AssetList<CampaignAsset> campaigns;
        private GameDataAsset preferences;
        private string inputJSON;

        private ExternalLibraryOverseer()
        {
            packs = new AssetList<PackAsset>(ex.Packs.Save, ex.Packs.Update, ex.Packs.Delete);
            campaigns = new AssetList<CampaignAsset>(ex.Campaigns.Save, ex.Campaigns.Update, ex.Campaigns.Delete);
            
            packEditor.OnSaveChanges += UpdatePack;
            packEditor.OnRemoveSprite += RemoveSpriteAssociation;
            campaignEditor.OnSaveChanges += UpdateCampaign;
            optionsEditor.OnSaveChanges += UpdatePreferences;
            ReloadFromExternalStorage();
        }

        #region Packs
        /// <summary>
        /// Creates a new Pack, and adds it to the library.
        /// </summary>
        /// <param name="pack">Information about the pack.</param>
        public void CreateAndAddPack(PackAsset pack)
        {
            packs.Add(new PackAsset.Builder().AsCopy(pack).Build());
        }

        /// <summary>
        /// Updates the pack on a specific position in the library
        /// </summary>
        /// <param name="pack">The new data for the pack.</param>
        /// <param name="index">Position to override.</param>
        /// <param name="lastTitle">Pack's Title before updating.</param>
        /// <param name="lastAuthor">Pack's Author before updating.</param>
        /// <param name="lastAssociatedSpriteID">Pack's referenced sprite before updating.</param>
        public void UpdatePack(PackAsset pack, int index, string lastTitle, string lastAuthor, string lastAssociatedSpriteID)
        {
            pack.RefreshAssetCounts();
            ProcessSpriteAssociations(pack, pack, lastAssociatedSpriteID);
            if (!string.IsNullOrEmpty(pack.AssociatedSpriteID))
            {
                RefreshSpriteAndSaveAsset(packs, pack.ID, pack.Sprites.FindValueFirstOrDefault(pack.AssociatedSpriteID));
            }
            else packs.Update(index, pack);
        }
        
        /// <summary>
        /// Remove pack from the library.
        /// </summary>
        /// <param name="packIndex">Pack ID in the list.</param>
        public void DeletePack(int packIndex)
        {
            AssetAssociation.RemoveSpriteAssociation(packs[packIndex], packs[packIndex]);
            packs.Remove(packIndex);
        }

        /// <summary>
        /// Prepare one of the packs in the library for editing.
        /// </summary>
        public void ActivatePackEditor(int packIndex)
        {
            SafetyNet.EnsureListIsNotNullOrEmpty(packs, "Pack Library");
            SafetyNet.EnsureIntIsInRange(packIndex, 0, packs.Count, "packIndex for activating Pack Editor");
            packs[packIndex] = ex.Packs.Load(packs[packIndex]);
            packs[packIndex].RefreshAssetCounts();
            packEditor.AssignAsset(packs[packIndex], packIndex);
        }
        
        public void ClearPacks() => packs.Clear();

        private void RemoveSpriteAssociation(string id) => RemoveSpriteAssociationsAndSaveAsset(packs, id);

        #endregion

        #region Campaigns

        /// <summary>
        /// Creates a new Campaign, and adds it to the library.
        /// </summary>
        public void CreateAndAddCampaign(CampaignAsset campaign)
        {
            campaigns.Add(new CampaignAsset.Builder().AsCopy(campaign).Build());
        }

        /// <summary>
        /// Updates the campaign on a specific position in the library.
        /// </summary>
        /// <param name="campaign">The new data for the campaign.</param>
        /// <param name="index">Position to override.</param>
        /// <param name="originalTitle">Campaign's title before updating.</param>
        /// <param name="originalAuthor">Campaign's author before updating.</param>
        public void UpdateCampaign(CampaignAsset campaign, int index, string originalTitle, string originalAuthor)
        {
            campaigns.Update(index, campaign);
        }
        
        /// <summary>
        /// Remove campaign from the library.
        /// </summary>
        /// <param name="campaignIndex">Campaign ID in the list.</param>
        public void DeleteCampaign(int campaignIndex)
        {
            campaigns.Remove(campaignIndex);
        }
        /// <summary>
        /// Remove Campaign from the library.
        /// </summary>
        /// <param name="title">Campaign's Title</param>
        /// <param name="author">Campaign's Author</param>
        public void DeleteCampaign(string title, string author)
        {
            campaigns.Remove(title, author);
        }
        
        /// <summary>
        /// Prepare one of the campaigns in the library for editing.
        /// </summary>
        public void ActivateCampaignEditor(int campaignIndex, bool prepareEditor = true)
        {
            SafetyNet.EnsureListIsNotNullOrEmpty(campaigns, "Campaign Library");
            SafetyNet.EnsureIntIsInRange(campaignIndex, 0, campaigns.Count, "campaignIndex for activating Campaign Editor");
            campaignEditor.AssignAsset(campaigns[campaignIndex], campaignIndex, prepareEditor);
        }

        /// <summary>
        /// Prepares one of the campaign for transfer to gameplay.
        /// </summary>
        /// <param name="campaignIndex"></param>
        public void ActivateCampaignPlaythrough(int campaignIndex)
        {
            SafetyNet.EnsureListIsNotNullOrEmpty(campaigns, "Campaign Library");
            SafetyNet.EnsureIntIsInRange(campaignIndex, 0, campaigns.Count, "campaignIndex for activating Campaign Playthrough");
            SceneTransferOverseer.GetInstance().LoadUp(campaigns[campaignIndex]);
        }
        
        public void ClearCampaigns() => campaigns.Clear();
        #endregion

        #region Preferences

        public void UpdatePreferences(GameDataAsset gameData)
        {
            preferences = gameData;
            ex.Preferences.Save(preferences);
            inputJSON = InputSystem.GetInstance().GetInputsAsJSON();
            ex.Input.Save(inputJSON);
        }

        public void ActivateOptionsEditor()
        {
            optionsEditor.AssignAsset(preferences);
        }

        /// <summary>
        /// Refresh game settings from the currently saved data.
        /// </summary>
        public void RefreshSettings()
        {
            InputSystem.GetInstance().LoadInputsFromJSON(inputJSON);
            ActivateOptionsEditor();
            optionsEditor.ApplyAllSettings(preferences);
        }

        #endregion
        
        /// <summary>
        /// Loads all external storage data into the library.
        /// </summary>
        private void ReloadFromExternalStorage()
        {
            packs.ReplaceAll(ex.Packs.LoadAll());
            campaigns.ReplaceAll(ex.Campaigns.LoadAll());
            campaigns.RemoveAll(campaign => campaign.PackReferences.Count <= 0);
            
            IList<GameDataAsset> data = ex.Preferences.LoadAll();
            preferences = (data == null || data.Count <= 0) ? new GameDataAsset.Builder().Build() : data[0];
            inputJSON = ex.Input.Load();
            if (string.IsNullOrEmpty(inputJSON))
            {
                inputJSON = InputSystem.GetInstance().GetInputsAsJSON();
                ex.Input.Save(inputJSON);
            }
        }
        
        public ReadOnlyCollection<PackAsset> Packs { get => new(packs); }
        public ReadOnlyCollection<CampaignAsset> Campaigns {get => new(campaigns);}
        public GameDataAsset Preferences { get => preferences; }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BoubakProductions.Safety;
using Rogium.ExternalStorage;

namespace Rogium.Editors.Campaign
{
    /// <summary>
    /// Special distinct List variant for campaigns. Is synced with external storage.
    /// </summary>
    public class CampaignList : IList<CampaignAsset>
    {
        private IList<CampaignAsset> list;

        public CampaignList()
        {
            list = new List<CampaignAsset>();
        }
        public CampaignList(CampaignList original)
        {
            list = new List<CampaignAsset>(original);
        }

        /// <summary>
        /// Adds a new campaign to the library.
        /// </summary>
        /// <param name="campaign">The Campaign to add.</param>
        public void Add(CampaignAsset campaign)
        {
            if (TryFinding(campaign.Title, campaign.Author) != null)
                throw new FoundDuplicationException("You are trying to create a campaign, that already exists. Cannot have the same title and author!");
            
            ExternalStorageOverseer.Instance.Campaigns.Save(campaign);
            list.Add(campaign);
        }

        /// <summary>
        /// Change an campaign on a specific index in the list.
        /// </summary>
        /// <param name="index">Index of the campaign to change.</param>
        /// <param name="campaign">The new campaign data.</param>
        /// <exception cref="FoundDuplicationException"></exception>
        public void Update(int index, CampaignAsset campaign)
        {
            SafetyNet.EnsureIsNotNull(campaign, "Campaign to add");
            SafetyNet.EnsureIntIsInRange(index ,0, list.Count, "Campaign List");
            
            if (TryFinding(campaign.Title, campaign.Author, index) != null)
                throw new FoundDuplicationException("You are trying to update a campaign with a name and author that is already taken. Cannot have the same title and author!");
            list[index] = campaign;
            
            ExternalStorageOverseer.Instance.Campaigns.UpdateTitle(campaign);
            ExternalStorageOverseer.Instance.Campaigns.Save(campaign);
        }
        
        /// <summary>
        /// Remove campaign from the library with a specific name.
        /// </summary>
        /// <param name="name">The name of the campaign to remove.</param>
        public void Remove(string name, string author)
        {
            CampaignAsset foundCampaign = TryFinding(name, author);
            SafetyNet.EnsureIsNotNull(foundCampaign, "Pack to Remove");

            ExternalStorageOverseer.Instance.Campaigns.Delete(foundCampaign);
            list.Remove(foundCampaign);
        }

        /// <summary>
        /// Remove campaign from the library with a specific name.
        /// </summary>
        /// <param name="campaignIndex">The position of the campaign to remove on the list.</param>
        public void Remove(int campaignIndex)
        {
            SafetyNet.EnsureIntIsInRange(campaignIndex, 0, list.Count, "Campaign Index when removing");
            CampaignAsset foundCampaign = list[campaignIndex];
            SafetyNet.EnsureIsNotNull(foundCampaign, "Campaign to Remove");

            ExternalStorageOverseer.Instance.Campaigns.Delete(foundCampaign);
            list.Remove(foundCampaign);
        }

        /// <summary>
        /// Finds a Campaign in the library by a given name and a given author.
        /// If no campaign is found, returns NULL.
        /// </summary>
        /// <param name="title">The name by which we are searching</param>
        /// <param name="author">The name of the author who created the campaign.</param>
        /// <param name="excludePos">The position in the list to exclude.</param>
        /// <returns>The campaign asset with the given name</returns>
        public CampaignAsset TryFinding(string title, string author, int excludePos = -1)
        {
            if (list.Count == 0) return null;
            IList<CampaignAsset> foundCampaigns = list.Where((pack, counter) => counter != excludePos)
                                              .Where((campaign) => campaign.Title == title && campaign.Author == author)
                                              .ToList();

            SafetyNet.EnsureListIsNotLongerThan(foundCampaigns, 1, "Found Packs by name & author");
            return (foundCampaigns.Count == 0) ? null : foundCampaigns[0];
        }

        /// <summary>
        /// Replaces the current list with a new one.
        /// </summary>
        /// <param name="newList">The list to replace it with.</param>
        public void ReplaceAll(IList<CampaignAsset> newList)
        {
            SafetyNet.EnsureListDoesNotHaveDuplicities(newList, "New List");
            list = new List<CampaignAsset>(newList);
        }

        #region Untouched Overrides
        public int Count => list.Count;
        public bool IsReadOnly => false;
        public CampaignAsset this[int index] { get => list[index]; set => list[index] = value; }

        public int IndexOf(CampaignAsset item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, CampaignAsset item)
        {
            SafetyNet.EnsureListNotContains(this, item, "List of Packs");
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(CampaignAsset item)
        {
            return list.Contains(item);
        }

        public void CopyTo(CampaignAsset[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public bool Remove(CampaignAsset item)
        {
            return list.Remove(item);
        }

        public IEnumerator<CampaignAsset> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
        #endregion
    }
}
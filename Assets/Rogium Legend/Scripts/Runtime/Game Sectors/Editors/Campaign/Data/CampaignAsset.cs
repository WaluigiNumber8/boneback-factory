using RedRats.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using System;
using System.Collections.Generic;

namespace Rogium.Editors.Campaign
{
    /// <summary>
    /// Contains everything important to an individual Campaign.
    /// </summary>
    public class CampaignAsset : AssetWithDirectSpriteBase
    {
        private int adventureLength;
        private PackAsset dataPack;
        private IList<string> packReferences;

        private CampaignAsset() { }

        #region Update Values

        public void UpdateLength(int newLength)
        {
            SafetyNet.EnsureIntIsBiggerThan(newLength, 0, "New Campaign Length");
            adventureLength = newLength;
        }
        public void UpdateDataPack(PackAsset newPack)
        {
            SafetyNet.EnsureIsNotNull(newPack, "newPack");
            dataPack = new PackAsset(newPack);
        }

        public void UpdatePackReferences(IList<string> newPackReferences)
        {
            SafetyNet.EnsureIsNotNull(newPackReferences, nameof(newPackReferences));
            packReferences = new List<string>(newPackReferences);
        }
        #endregion

        public int AdventureLength { get => adventureLength; }
        public PackAsset DataPack { get => dataPack; }
        public IList<string> PackReferences { get => new List<string>(packReferences); }
        
        public class Builder : BaseBuilder<CampaignAsset, Builder>
        {
            public Builder()
            {
                Asset.title = EditorDefaults.Instance.CampaignTitle;
                Asset.icon = EditorDefaults.Instance.EmptySprite;
                Asset.author = EditorDefaults.Instance.Author;
                Asset.creationDate = DateTime.Now;
                Asset.GenerateID();
                Asset.packReferences = new List<string>();
            }

            public Builder WithAdventureLength(int adventureLength)
            {
                Asset.adventureLength = adventureLength;
                return This;
            }

            public Builder WithDataPack(PackAsset dataPack)
            {
                Asset.dataPack = new PackAsset(dataPack);
                return This;
            }

            public Builder WithPackReferences(IList<string> packReferences)
            {
                Asset.packReferences = new List<string>(packReferences);
                return This;
            }

            public override Builder AsClone(CampaignAsset asset)
            {
                AsCopy(asset);
                Asset.GenerateID();
                return This;
            }

            public override Builder AsCopy(CampaignAsset asset)
            {
                Asset.id = asset.ID;
                Asset.title = asset.Title;
                Asset.icon = asset.Icon;
                Asset.author = asset.Author;
                Asset.creationDate = asset.CreationDate;
                Asset.adventureLength = asset.AdventureLength;
                Asset.dataPack = new PackAsset(asset.DataPack);
                Asset.packReferences = new List<string>(asset.packReferences);
                return This;
            }

            protected sealed override CampaignAsset Asset { get; } = new();
        }
    }
}
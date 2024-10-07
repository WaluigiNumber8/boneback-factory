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
        private ISet<string> packReferences;

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
            dataPack = new PackAsset.Builder().AsCopy(newPack).Build();
        }

        public void UpdatePackReferences(ISet<string> newPackReferences)
        {
            SafetyNet.EnsureIsNotNull(newPackReferences, nameof(newPackReferences));
            packReferences = new HashSet<string>(newPackReferences);
        }
        #endregion

        public int AdventureLength { get => adventureLength; }
        public PackAsset DataPack { get => dataPack; }
        public ISet<string> PackReferences { get => new HashSet<string>(packReferences); }
        
        public class Builder : BaseBuilder<CampaignAsset, Builder>
        {
            public Builder()
            {
                Asset.title = EditorDefaults.Instance.CampaignTitle;
                Asset.icon = EditorDefaults.Instance.EmptySprite;
                Asset.author = EditorDefaults.Instance.Author;
                Asset.creationDate = DateTime.Now;
                Asset.GenerateID();
                Asset.packReferences = new HashSet<string>();
            }

            public Builder WithAdventureLength(int adventureLength)
            {
                Asset.adventureLength = adventureLength;
                return This;
            }

            public Builder WithDataPack(PackAsset dataPack)
            {
                Asset.dataPack = new PackAsset.Builder().AsCopy(dataPack).Build();
                return This;
            }

            public Builder WithPackReferences(IList<string> packReferences)
            {
                Asset.packReferences = new HashSet<string>(packReferences);
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
                Asset.dataPack = (asset.dataPack != null) ? new PackAsset.Builder().AsCopy(asset.DataPack).Build() : new PackAsset.Builder().Build();
                Asset.packReferences = new HashSet<string>(asset.packReferences);
                return This;
            }

            protected sealed override CampaignAsset Asset { get; } = new();
        }
    }
}
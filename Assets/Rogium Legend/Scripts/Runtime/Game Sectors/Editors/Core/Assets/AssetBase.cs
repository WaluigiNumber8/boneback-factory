using System;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Weapons;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// A base for all user created assets.
    /// </summary>
    public abstract class AssetBase : IAsset
    {
        //TODO - For all assets upon creation, read author from Player Profile.
        protected string id;
        protected string title;
        protected Sprite icon;
        protected string author;
        protected DateTime creationDate;

        /// <summary>
        /// Generates the ID for this asset.
        /// </summary>
        /// <returns>ID</returns>
        protected void GenerateID(string assetIdentifier)
        {
            string authorPart = Mathf.Abs(author.GetHashCode()).ToString().Substring(0, 3);
            string datePart = Mathf.Abs(creationDate.GetHashCode()).ToString().Substring(0, 3);
            string randomPart = Random.Range(100, 999).ToString();
            id = assetIdentifier + authorPart + datePart + randomPart;
        }

        #region Update Values

        public void UpdateTitle(string newTitle) => title = newTitle.Trim();

        #endregion

        /// <summary>
        /// Initialize the base values of the asset.
        /// </summary>
        protected void InitBase(string title, Sprite icon, string author, DateTime creationDate)
        {
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;
        }

        public override bool Equals(object obj) => obj is AssetBase asset && ID == asset.ID;
        public override int GetHashCode() => (id != null ? id.GetHashCode() : 0);
        public override string ToString() => $"{Title} ({ID})";

        public string ID { get => id; }
        public string Title { get => title; }
        public Sprite Icon { get => icon; }
        public string Author { get => author; }
        public DateTime CreationDate { get => creationDate; }

        public abstract class BaseBuilder<T, TBuilder> where T : AssetBase where TBuilder : BaseBuilder<T, TBuilder>
        {
            public TBuilder WithID(string id)
            {
                Asset.id = id;
                return This;
            }

            public TBuilder WithTitle(string title)
            {
                Asset.title = title;
                return This;
            }

            public TBuilder WithIcon(Sprite icon)
            {
                Asset.icon = icon;
                return This;
            }

            public TBuilder WithAuthor(string author)
            {
                Asset.author = author;
                return This;
            }

            public TBuilder WithCreationDate(DateTime creationDate)
            {
                Asset.creationDate = creationDate;
                return This;
            }

            /// <summary>
            /// Creates a clone of the asset with a different ID.
            /// </summary>
            public abstract TBuilder AsClone(T asset);

            /// <summary>
            /// Creates a copy of the asset with the same ID.
            /// </summary>
            /// <returns></returns>
            public abstract TBuilder AsCopy(T asset);
            
            public abstract T Build();
            protected abstract T Asset { get; }
            protected TBuilder This => (TBuilder) this;
        }
    }
}
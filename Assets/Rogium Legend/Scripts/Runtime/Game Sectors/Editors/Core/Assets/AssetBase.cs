using System;
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

        public override string ToString() => $"{Title} ({ID})";

        public string ID { get => id; }
        public string Title { get => title; }
        public Sprite Icon { get => icon; }
        public string Author { get => author; }
        public DateTime CreationDate { get => creationDate; }
    }
}

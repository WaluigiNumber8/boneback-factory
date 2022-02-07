using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// Interface for a Asset type object.
    /// </summary>
    public abstract class AssetBase
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
            string authorPart = Mathf.Abs(author.GetHashCode()).ToString().Substring(0, 4);
            string datePart = Mathf.Abs(creationDate.GetHashCode()).ToString().Substring(0, 4);
            string randomPart = Random.Range(100, 999).ToString();
            id = assetIdentifier + authorPart + datePart + randomPart;
        }

        #region Update Values
        public void UpdateTitle(string newTitle) => this.title = newTitle;
        public virtual void UpdateIcon(Sprite newIcon) => this.icon = newIcon;

        #endregion
        
        public string ID { get => id; }
        public string Title { get => title; }
        public virtual Sprite Icon { get => icon; }
        public string Author { get => author; }
        public DateTime CreationDate { get => creationDate; }
    }
}

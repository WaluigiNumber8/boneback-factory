using Rogium.Editors.Core;
using System;
using UnityEngine;

namespace Rogium.Editors.PaletteData
{
    public class PaletteAsset : IAsset
    {
        private string title;
        private Sprite icon;
        private string author;
        private DateTime creationDate;
        private Color[] colors;

        public string Title { get => title; }
        public Sprite Icon { get => icon; }
        public string Author  { get => author; }
        public DateTime CreationDate { get => creationDate; }

        #region Update Values
        public void UpdateTitle(string newTitle)
        {
            this.title = newTitle;
        }

        public void UpdateIcon(Sprite newIcon)
        {
            this.icon = newIcon;
        }

        public void UpdateAuthor(string newAuthor)
        {
            this.author = newAuthor;
        }

        public void UpdateCreationDate(DateTime newCreationDate)
        {
            this.creationDate = newCreationDate;
        }
        #endregion
    }
}

using System;
using UnityEngine;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// Interface for a Asset type object.
    /// </summary>
    public interface IAsset
    {
        //TODO - For all assets upon creation, read author from Player Profile.

        public string Title { get; }
        public Sprite Icon { get; }
        public string Author { get; }
        public DateTime CreationDate { get; }

        public void UpdateTitle(string newTitle);
        public void UpdateIcon(Sprite newIcon);
        public void UpdateAuthor(string newAuthor);
        public void UpdateCreationDate(DateTime newCreationDate);
    }
}
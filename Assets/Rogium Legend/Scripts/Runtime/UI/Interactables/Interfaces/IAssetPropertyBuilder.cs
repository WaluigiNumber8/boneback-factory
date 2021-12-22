using System;
using UnityEngine;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// A Base for all Property Asset Builders.
    /// </summary>
    public interface IAssetPropertyBuilder
    {
        public void UpdateTitle(string newTitle);
        public void UpdateIcon(Sprite newIcon);
        public void UpdateAuthor(string newAuthor);
        public void UpdateCreationDate(DateTime newCreationDate);
    }
}
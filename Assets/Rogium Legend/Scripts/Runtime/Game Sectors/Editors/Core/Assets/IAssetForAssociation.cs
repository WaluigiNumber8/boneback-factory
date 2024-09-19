using System.Collections.Generic;

namespace Rogium.Editors.Sprites
{
    /// <summary>
    /// Represents an asset that can have other assets associated with it.
    /// </summary>
    public interface IAssetForAssociation
    {
        public void AddAssociation(string id);
        public void RemoveAssociation(string id);
        public ISet<string> AssociatedAssetsIDs { get; }
    }
}
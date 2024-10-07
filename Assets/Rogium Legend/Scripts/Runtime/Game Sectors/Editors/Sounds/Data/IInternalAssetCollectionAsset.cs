using System.Collections.Generic;
using System.Collections.ObjectModel;
using Rogium.Editors.Core;

namespace Rogium.Editors.Objects
{
    /// <summary>
    /// Represents an asset that holds a collection of Internal Assets.
    /// </summary>
    /// <typeparam name="T">Any type of asset.</typeparam>
    public interface IInternalAssetCollectionAsset<T> where T : IAsset
    {
        public T GetAssetByID(string id);
        public ReadOnlyCollection<T> ReadOnlyList { get; }
    }
}
using UnityEngine;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// A base for all asset objects.
    /// </summary>
    public interface IAsset : IDataAsset
    {
        public Sprite Icon { get; }
        public string Author { get; }
    }
}
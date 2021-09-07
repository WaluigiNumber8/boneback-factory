using UnityEngine;

namespace RogiumLegend.Editors
{
    /// <summary>
    /// Interface for a Asset type object.
    /// </summary>
    public interface IAsset
    {
        string Title { get; }
        Sprite Icon { get; }
    }
}
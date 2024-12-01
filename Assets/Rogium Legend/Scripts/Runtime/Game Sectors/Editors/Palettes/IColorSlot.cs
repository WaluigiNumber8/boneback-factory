using Rogium.Editors.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.Editors.Palettes
{
    /// <summary>
    /// Represents a slot that holds a color.
    /// </summary>
    public interface IColorSlot : IIndexHolder
    {
        public Color CurrentColor { get; }
        public Image ColorImage { get; }
    }
}
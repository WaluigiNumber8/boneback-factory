using System.Collections;
using UnityEngine;

namespace RogiumLegend.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized of a Color.
    /// </summary>
    [System.Serializable]
    public class SerializedColor
    {
        public readonly float r, g, b, a;

        public SerializedColor(Color color) : this(color.r, color.g, color.b, color.a) { }
        public SerializedColor(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
    }
}
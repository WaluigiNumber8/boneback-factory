using RogiumLegend.Editors.PackData;
using System.Collections;
using UnityEngine;

namespace RogiumLegend.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the PackAsset Class, ready for saving.
    /// </summary>
    [System.Serializable]
    public class SerializedPackAsset
    {
        public readonly SerializedPackInfoAsset packInfo;

        public SerializedPackAsset(PackInfoAsset packInfo)
        {
            this.packInfo = new SerializedPackInfoAsset(packInfo);
        }
    }
}
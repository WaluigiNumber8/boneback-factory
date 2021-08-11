using RogiumLegend.Global.SafetyChecks;
using System.Collections;
using UnityEngine;

namespace RogiumLegend.Editors.PackData
{
    /// <summary>
    /// Handles everything related to crating new packs.
    /// </summary>
    public class PackBuilder
    {
        /// <summary>
        /// Creates a new Pack. Before creation fires parameters safety checks.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="author"></param>
        /// <param name="icon"></param>
        public PackAsset BuildPack(string name, string description, string author, Sprite icon)
        {
            SafetyNet.EnsureStringInRange(name, 4, 30, "name");
            SafetyNet.EnsureStringInRange(description, 0, 2000, "description");

            PackAsset newPack = new PackAsset(name, description, author, icon);
            return newPack;
        }

    }
}
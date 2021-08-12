using RogiumLegend.Global.SafetyChecks;
using System;
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
        /// <param name="packAsset">Asset to copy.</param>
        public PackAsset BuildPack(PackAsset packAsset)
        {
            return BuildPack(packAsset.PackInfo.packName,
                             packAsset.PackInfo.description,
                             packAsset.PackInfo.author,
                             packAsset.PackInfo.icon,
                             DateTime.Now);
        }
        /// <summary>
        /// Creates a new Pack. Before creation fires parameters safety checks.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="author"></param>
        /// <param name="icon"></param>
        public PackAsset BuildPack(string name, string description, string author, Sprite icon)
        {
            return BuildPack(name, description, author, icon, DateTime.Now);
        }
        /// <summary>
        /// Creates a new Pack. Before creation fires parameters safety checks.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="author"></param>
        /// <param name="icon"></param>
        /// <param name="creationDateTime"></param>
        public PackAsset BuildPack(string name, string description, string author, Sprite icon, DateTime creationDateTime)
        {
            SafetyNet.EnsureStringInRange(name, 4, 30, "name");
            SafetyNet.EnsureStringInRange(description, 0, 2000, "description");

            PackInfoAsset newInfo = new PackInfoAsset(name, description, author, icon, creationDateTime);
            PackAsset newPack = new PackAsset(newInfo);
            return newPack;
        }
    }
}
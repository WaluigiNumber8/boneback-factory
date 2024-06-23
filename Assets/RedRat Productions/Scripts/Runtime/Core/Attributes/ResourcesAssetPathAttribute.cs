using System;

namespace RedRats.Core
{
    /// <summary>
    /// Stores the path of an asset in the Resources folder.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ResourcesAssetPathAttribute : Attribute
    {
        public string AssetPath { get; }

        public ResourcesAssetPathAttribute(string assetPath)
        {
            AssetPath = assetPath;
        }
    }
}
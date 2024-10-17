namespace Rogium.Editors.Core
{
    /// <summary>
    /// Represents an asset with a description.
    /// </summary>
    public interface IAssetWithDescription : IAsset
    {
        public string Description { get; }
    }
}
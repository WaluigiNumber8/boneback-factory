namespace Rogium.Editors.Core
{
    /// <summary>
    /// Represents an asset that has a palette.
    /// </summary>
    public interface IAssetWithPalette : IAsset
    {
        public void UpdatePalette(IAsset newPalette);
    }
}
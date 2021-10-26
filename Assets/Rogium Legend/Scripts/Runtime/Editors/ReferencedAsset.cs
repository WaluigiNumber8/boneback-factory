namespace Rogium.Editors.Core
{
    /// <summary>
    /// An asset containing the reference index and the data stored under it.
    /// </summary>
    public class ReferencedAsset
    {
        private readonly int index;
        private readonly AssetBase asset;

        public ReferencedAsset(int index, AssetBase asset)
        {
            this.index = index;
            this.asset = asset;
        }
        
        public int Index {get => index;}
        public AssetBase Asset {get => asset;}
    }
}
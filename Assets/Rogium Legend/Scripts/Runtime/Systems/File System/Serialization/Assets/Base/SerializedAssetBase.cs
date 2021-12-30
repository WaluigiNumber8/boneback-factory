using BoubakProductions.Systems.Serialization;
using Rogium.Editors.Core;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// A base for all serialized assets.
    /// </summary>
    [System.Serializable]
    public abstract class SerializedAssetBase
    {
        protected string id;
        protected string title;
        protected SerializedSprite icon;
        protected string author;
        protected string creationDate;

        protected SerializedAssetBase(AssetBase asset)
        {
            this.id = asset.ID;
            this.title = asset.Title;
            this.icon = new SerializedSprite(asset.Icon);
            this.author = asset.Author;
            this.creationDate = asset.CreationDate.ToString();
        }
        
    }
}
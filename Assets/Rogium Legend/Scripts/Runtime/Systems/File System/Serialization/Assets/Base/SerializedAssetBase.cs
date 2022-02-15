using BoubakProductions.Systems.FileSystem.Serialization;
using Rogium.Editors.Core;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// A base for all serialized assets.
    /// <typeparam name="T">The readable form of the asset.</typeparam>
    /// </summary>
    [System.Serializable]
    public abstract class SerializedAssetBase<T> : ISerializedObject<T> where T : AssetBase
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

        T ISerializedObject<T>.Deserialize() => Deserialize();
        public abstract T Deserialize();

    }
}
using RedRats.Systems.FileSystem.Serialization;
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

        protected SerializedAssetBase(T asset)
        {
            id = asset.ID;
            title = asset.Title;
            icon = new SerializedSprite(asset.Icon);
            author = asset.Author;
            creationDate = asset.CreationDate.ToString();
        }

        T ISerializedObject<T>.Deserialize() => Deserialize();
        
       /// <summary>
       /// Deserializes the asset.
       /// </summary>
       /// <returns>The asset in a readable form.</returns>
        public abstract T Deserialize();

    }
}
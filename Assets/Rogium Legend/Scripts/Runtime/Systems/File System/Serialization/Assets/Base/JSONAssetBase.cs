using RedRats.Systems.FileSystem;
using RedRats.Systems.FileSystem.JSON.Serialization;
using Rogium.Editors.Core;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// A base for all serialized assets.
    /// <typeparam name="T">The readable form of the asset.</typeparam>
    /// </summary>
    [System.Serializable]
    public abstract class JSONAssetBase<T> : IEncodedObject<T> where T : AssetBase
    {
        public string id;
        public string title;
        public JSONSprite icon;
        public string author;
        public string creationDate;

        protected JSONAssetBase(T asset)
        {
            id = asset.ID;
            title = asset.Title;
            icon = new JSONSprite(asset.Icon);
            author = asset.Author;
            creationDate = asset.CreationDate.ToString();
        }

        T IEncodedObject<T>.Decode() => Decode();
        
       /// <summary>
       /// Deserializes the asset.
       /// </summary>
       /// <returns>The asset in a readable form.</returns>
        public abstract T Decode();

    }
}
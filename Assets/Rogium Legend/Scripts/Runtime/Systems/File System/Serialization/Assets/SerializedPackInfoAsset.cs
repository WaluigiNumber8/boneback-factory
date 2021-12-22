using BoubakProductions.Systems.Serialization;
using Rogium.Editors.Packs;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the PackInfoAsset, ready for saving.
    /// </summary>
    [System.Serializable]
    public class SerializedPackInfoAsset
    {
        public readonly string id;
        public readonly string title;
        public readonly SerializedSprite icon;
        public readonly string author;
        public readonly string creationTime;
        public readonly string description;

        public SerializedPackInfoAsset(PackInfoAsset packInfo)
        {
            this.id = packInfo.ID;
            this.title = packInfo.Title;
            this.icon = new SerializedSprite(packInfo.Icon);
            this.author = packInfo.Author;
            this.creationTime = packInfo.CreationDate.ToString();
            this.description = packInfo.Description;
        }

    }
}
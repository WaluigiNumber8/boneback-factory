using Rogium.Editors.Campaign;

namespace Rogium.ExternalStorage.Serialization
{
    [System.Serializable]
    public class SerializedPackImportInfo
    {
        private string ID;
        private bool weapons;
        private bool enemies;
        private bool rooms;

        public SerializedPackImportInfo(PackImportInfo importInfo) : this(importInfo.ID, importInfo.weapons, importInfo.enemies, importInfo.rooms) { }
        public SerializedPackImportInfo(string ID, bool weapons, bool enemies, bool rooms)
        {
            this.ID = ID;
            this.weapons = weapons;
            this.enemies = enemies;
            this.rooms = rooms;
        }

        /// <summary>
        /// Turns the pack info into a Unity readable format.
        /// </summary>
        /// <returns>The PackImportInfo Unity can read.</returns>
        public PackImportInfo Deserialize()
        {
            PackImportInfo importInfo;
            importInfo.ID = ID;
            importInfo.weapons = weapons;
            importInfo.enemies = enemies;
            importInfo.rooms = rooms;
            return importInfo;
        }
    }
}
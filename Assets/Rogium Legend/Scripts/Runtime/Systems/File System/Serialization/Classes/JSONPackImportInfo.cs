using Rogium.Editors.Campaign;

namespace Rogium.ExternalStorage.Serialization
{
    [System.Serializable]
    public class JSONPackImportInfo
    {
        public string ID;
        public bool weapons;
        public bool enemies;
        public bool rooms;

        public JSONPackImportInfo(PackImportInfo importInfo) : this(importInfo.ID, importInfo.weapons, importInfo.enemies, importInfo.rooms) { }
        public JSONPackImportInfo(string ID, bool weapons, bool enemies, bool rooms)
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
            return new PackImportInfo(ID, weapons, enemies, rooms);;
        }
    }
}
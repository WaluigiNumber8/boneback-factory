using Rogium.Editors.Core;

namespace Rogium.Editors.Campaign
{
    /// <summary>
    /// Contains information about what packs and what in them will be imported.
    /// </summary>
    public struct PackImportInfo : IIDHolder
    {
        private string id;
        
        public bool weapons;
        public bool enemies;
        public bool rooms;

        public PackImportInfo(string id, bool weapons, bool enemies, bool rooms)
        {
            this.id = id;
            this.weapons = weapons;
            this.enemies = enemies;
            this.rooms = rooms;
        }
        
        public string ID { get => id; }
    }
}
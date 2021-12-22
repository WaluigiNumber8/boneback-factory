namespace Rogium.Editors.Campaign
{
    /// <summary>
    /// Contains information about what packs and what in them will be imported.
    /// </summary>
    public struct PackImportInfo
    {
        public string ID;
        
        public bool weapons;
        public bool enemies;
        public bool rooms;
    }
}
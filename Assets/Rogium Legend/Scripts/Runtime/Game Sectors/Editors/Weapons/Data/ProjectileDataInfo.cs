
namespace Rogium.Editors.Weapons
{
    /// <summary>
    /// Contains info about an added projectile.
    /// </summary>
    [System.Serializable]
    public class ProjectileDataInfo
    {
        public string id;
        public float spawnDelay;
        public int angleOffset;

        public ProjectileDataInfo(string id, float spawnDelay, int angleOffset)
        {
            this.id = id;
            this.spawnDelay = spawnDelay;
            this.angleOffset = angleOffset;
        }

        public void UpdateID(string newID) => id = newID;
        public void UpdateSpawnDelay(float newSpawnDelay) => spawnDelay = newSpawnDelay;
        public void UpdateAngleOffset(int newAngleOffset) => angleOffset = newAngleOffset;
        
        public string ID { get => id; }
        public float SpawnDelay { get => spawnDelay; }
        public int AngleOffset { get => angleOffset; }
    }
}
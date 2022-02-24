using System;
using Rogium.Editors.Enemies;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="EnemyAsset"/>.
    /// </summary>
    [System.Serializable]
    public class SerializedEnemyAsset : SerializedEntityAssetBase<EnemyAsset>
    {
        public SerializedEnemyAsset(EnemyAsset asset) : base(asset)
        {
            
        }

        public override EnemyAsset Deserialize()
        {
            return new EnemyAsset(id,
                                  title,
                                  icon.Deserialize(),
                                  author,
                                  baseDamage,
                                  useDelay,
                                  knockbackSelf,
                                  knockbackOther,
                                  DateTime.Parse(creationDate));
        }
    }
}
using System.Linq;
using Rogium.Core;
using Rogium.Editors.Enemies;
using Rogium.Editors.Packs;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Editors.AssetSelection
{
    /// <summary>
    /// Builds the <see cref="SelectionInfoColumn"/> for a <see cref="EnemyAsset"/>.
    /// </summary>
    public class SelectionInfoColumnPropertyBuilderEnemy : IPContentBuilderBaseColumn1<EnemyAsset>
    {
        public SelectionInfoColumnPropertyBuilderEnemy(Transform contentMain) : base(contentMain) { }

        /// <summary>
        /// Build <see cref="SelectionInfoColumn"/> properties for an enemy.
        /// </summary>
        /// <param name="asset">The enemy to build for.</param>
        public override void BuildInternal(EnemyAsset asset)
        {
            b.BuildPlainText("Damage", asset.BaseDamage.ToString(), contentMain);
            b.BuildPlainText("Health", asset.MaxHealth.ToString(), contentMain);
            b.BuildAssetEmblemList("Weapons", asset.WeaponIDs.TryGetAssets(PackEditorOverseer.Instance.CurrentPack.Weapons).Select(d => d.Icon).ToList(), contentMain);
        }
    }
}
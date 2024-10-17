using System.Linq;
using Rogium.Core;
using Rogium.Editors.Packs;
using Rogium.Editors.Weapons;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Builds the <see cref="SelectionInfoColumn"/> for a <see cref="WeaponAsset"/>.
    /// </summary>
    public class SelectionInfoColumnPropertyBuilderWeapon : UIPropertyContentBuilderBaseColumn1<WeaponAsset>
    {
        public SelectionInfoColumnPropertyBuilderWeapon(Transform contentMain) : base(contentMain) { }

        /// <summary>
        /// Build <see cref="SelectionInfoColumn"/> properties for a weapon.
        /// </summary>
        /// <param name="asset">The weapon to build for.</param>
        public override void Build(WeaponAsset asset)
        {
            Clear();
            b.BuildPlainText("Damage", asset.BaseDamage.ToString(), contentMain);
            b.BuildPlainText("Type", (asset.IsEvasive) ? "Evasive" : "Active", contentMain);
            b.BuildAssetEmblemList("Fires", asset.ProjectileIDs.Select(d => d.id).ToList().TryGetAssets(PackEditorOverseer.Instance.CurrentPack.Projectiles).Select(d => d.Icon).ToList(), contentMain);
        }
    }
}
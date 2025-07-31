using Rogium.Editors.Projectiles;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Editors.AssetSelection
{
    /// <summary>
    /// Builds the <see cref="SelectionInfoColumn"/> for a <see cref="ProjectileAsset"/>.
    /// </summary>
    public class SelectionInfoColumnPropertyBuilderProjectile : IPContentBuilderBaseColumn1<ProjectileAsset>
    {
        public SelectionInfoColumnPropertyBuilderProjectile(Transform contentMain) : base(contentMain) { }

        /// <summary>
        /// Build <see cref="SelectionInfoColumn"/> properties for a projectile.
        /// </summary>
        /// <param name="asset">The projectile to build for.</param>
        public override void BuildInternal(ProjectileAsset asset)
        {
            b.BuildPlainText("Damage", asset.BaseDamage.ToString(), contentMain);
            b.BuildPlainText("Speed", asset.FlightSpeed.ToString(), contentMain);
            b.BuildPlainText("Type", asset.PierceType.ToString(), contentMain);
        }
    }
}
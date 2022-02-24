using System;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using UnityEngine;

namespace Rogium.Editors.Projectiles
{
    /// <summary>
    /// Contains all data needed for a projectile.
    /// </summary>
    public class ProjectileAsset : EntityAssetBase
    {

        #region Constructors
        public ProjectileAsset()
        {
            title = EditorDefaults.ProjectileTitle;
            icon = EditorDefaults.ProjectileIcon;
            author = EditorDefaults.Author;
            creationDate = DateTime.Now;

            baseDamage = EditorDefaults.ProjectileBaseDamage;
            useDelay = EditorDefaults.ProjectileLifetime;
            knockbackSelf = EditorDefaults.ProjectileKnockbackSelf;
            knockbackOther = EditorDefaults.ProjectileKnockbackOther;
            
            GenerateID(EditorAssetIDs.ProjectileIdentifier);
        }

        public ProjectileAsset(ProjectileAsset asset)
        {
            id = asset.ID;
            title = asset.Title;
            icon = asset.Icon;
            author = asset.Author;
            creationDate = asset.CreationDate;

            baseDamage = asset.BaseDamage;
            useDelay = asset.UseDelay;
            knockbackSelf = asset.KnockbackSelf;
            knockbackOther = asset.KnockbackOther;
        }

        public ProjectileAsset(string id, string title, Sprite icon, string author, int baseDamage, float useDelay,
            float knockbackSelf, float knockbackOther, DateTime creationDate)
        {
            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;

            this.baseDamage = baseDamage;
            this.useDelay = useDelay;
            this.knockbackSelf = knockbackSelf;
            this.knockbackOther = knockbackOther;
            
        }
        #endregion

        #region Update Values



        #endregion

    }
}
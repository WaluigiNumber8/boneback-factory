using System;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using UnityEngine;

namespace Rogium.Editors.Enemies
{
    /// <summary>
    /// Contains all data needed for an enemy.
    /// </summary>
    public class EnemyAsset : EntityAssetBase
    {
        
        #region Constructors
        public EnemyAsset()
        {
            title = EditorDefaults.EnemyTitle;
            icon = EditorDefaults.EnemyIcon;
            author = EditorDefaults.Author;
            creationDate = DateTime.Now;

            baseDamage = EditorDefaults.EnemyBaseDamage;
            useDelay = EditorDefaults.EnemyAttackDelay;
            knockbackSelf = EditorDefaults.EnemyKnockbackSelf;
            knockbackOther = EditorDefaults.EnemyKnockbackOther;
            
            GenerateID(EditorAssetIDs.EnemyIdentifier);
        }

        public EnemyAsset(EnemyAsset asset)
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

        public EnemyAsset(string id, string title, Sprite icon, string author, int baseDamage, float useDelay,
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
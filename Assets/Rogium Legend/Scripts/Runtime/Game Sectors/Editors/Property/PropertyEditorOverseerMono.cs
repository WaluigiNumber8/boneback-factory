using RedRats.Core;
using Rogium.Editors.Enemies;
using Rogium.Editors.Projectiles;
using Rogium.Editors.PropertyEditor.Builders;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;
using Rogium.Systems.ThemeSystem;
using Rogium.UserInterface.Interactables;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.Editors.PropertyEditor
{
    /// <summary>
    /// Overseers the Property Editor.
    /// </summary>
    public class PropertyEditorOverseerMono : MonoSingleton<PropertyEditorOverseerMono>
    {
        [SerializeField] private Transform importantContent;
        [SerializeField] private Transform propertyContent;
        [SerializeField] private UIInfo ui;

        private PropertyEditorBuilderWeapon weaponBuilder;
        private PropertyEditorBuilderProjectile projectileBuilder;
        private PropertyEditorBuilderEnemy enemyBuilder;
        private PropertyEditorBuilderTile tileBuilder;

        protected override void Awake()
        {
            base.Awake();
            weaponBuilder = new PropertyEditorBuilderWeapon(importantContent, propertyContent);
            projectileBuilder = new PropertyEditorBuilderProjectile(importantContent, propertyContent);
            enemyBuilder = new PropertyEditorBuilderEnemy(importantContent, propertyContent);
            tileBuilder = new PropertyEditorBuilderTile(importantContent, propertyContent);
        }

        private void OnEnable()
        {
            WeaponEditorOverseer.Instance.OnAssignAsset += InitWeapons;
            ProjectileEditorOverseer.Instance.OnAssignAsset += InitProjectiles;
            EnemyEditorOverseer.Instance.OnAssignAsset += InitEnemies;
            TileEditorOverseer.Instance.OnAssignAsset += InitTiles;
        }

        private void OnDisable()
        {
            WeaponEditorOverseer.Instance.OnAssignAsset -= InitWeapons;
            ProjectileEditorOverseer.Instance.OnAssignAsset -= InitProjectiles;
            EnemyEditorOverseer.Instance.OnAssignAsset -= InitEnemies;
            TileEditorOverseer.Instance.OnAssignAsset -= InitTiles;
        }

        
        /// <summary>
        /// Initializes the editor for Weapons.
        /// </summary>
        /// <param name="asset">The Weapon Asset to work with.</param>
        private void InitWeapons(WeaponAsset asset)
        {
            Init();

            ui.saveButton.Action = ButtonType.SaveChangesWeapon;
            ui.cancelButton.Action = ButtonType.CancelChangesWeapon;
            
            weaponBuilder.Build(asset);
        }
        
        /// <summary>
        /// Initializes the editor for Projectiles.
        /// </summary>
        /// <param name="asset">The Projectile Asset to work with.</param>
        private void InitProjectiles(ProjectileAsset asset)
        {
            Init();

            ui.saveButton.Action = ButtonType.SaveChangesProjectile;
            ui.cancelButton.Action = ButtonType.CancelChangesProjectile;
            
            projectileBuilder.Build(asset);
        }
        
        /// <summary>
        /// Initializes the editor for Enemies.
        /// </summary>
        /// <param name="asset">The Enemy Asset to work with.</param>
        private void InitEnemies(EnemyAsset asset)
        {
            Init();

            ui.saveButton.Action = ButtonType.SaveChangesEnemy;
            ui.cancelButton.Action = ButtonType.CancelChangesEnemy;
            
            enemyBuilder.Build(asset);
        }
        
        /// <summary>
        /// Initializes the editor for Tiles.
        /// </summary>
        /// <param name="asset">The Tile Asset to work with.</param>
        private void InitTiles(TileAsset asset)
        {
            Init();

            ui.saveButton.Action = ButtonType.SaveChangesTile;
            ui.cancelButton.Action = ButtonType.CancelChangesTile;
            
            tileBuilder.Build(asset);
        }

        /// <summary>
        /// Initializes the editor.
        /// </summary>
        private void Init()
        {
            ReloadTheme();
            ui.importantScrollbar.SetValue(1);
            ui.propertyScrollbar.SetValue(1);
        }

        /// <summary>
        /// Sets graphics from the current theme.
        /// </summary>
        private void ReloadTheme()
        {
            ThemeUpdaterRogium.UpdateElement(ui.importantColumnBackground);
            ThemeUpdaterRogium.UpdateButtonCard(ui.saveButton);
            ThemeUpdaterRogium.UpdateButtonCard(ui.cancelButton);
            ThemeUpdaterRogium.UpdateScrollbar(ui.importantScrollbar);
            ThemeUpdaterRogium.UpdateScrollbar(ui.propertyScrollbar);
            // ThemeUpdaterRogium.UpdateButtonMenu(ui.undoButton);
            // ThemeUpdaterRogium.UpdateButtonMenu(ui.redoButton);
            // ThemeUpdaterRogium.UpdateButtonMenu(ui.resetButton);
        }
        
        [System.Serializable]
        public struct UIInfo
        {
            public Image importantColumnBackground;
            public InteractableButton saveButton;
            public InteractableButton cancelButton;
            public InteractableButton undoButton;
            public InteractableButton redoButton;
            public InteractableButton resetButton;
            public InteractableScrollbar importantScrollbar;
            public InteractableScrollbar propertyScrollbar;
        }
    }
}
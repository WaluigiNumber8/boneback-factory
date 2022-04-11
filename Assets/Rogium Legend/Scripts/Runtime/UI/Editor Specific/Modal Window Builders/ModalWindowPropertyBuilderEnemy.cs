using System;
using System.Linq;
using BoubakProductions.Core;
using BoubakProductions.UI;
using Rogium.Editors.Enemies;
using Rogium.Editors.Tiles;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Enemy Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderEnemy : ModalWindowPropertyBuilder
    {
        private EnemyEditorOverseer enemyEditor;

        public ModalWindowPropertyBuilderEnemy()
        {
            enemyEditor = EnemyEditorOverseer.Instance;
        }
        
        public override void OpenForCreate()
        {
            OpenWindow(new EnemyAsset(), CreateAsset, "Creating a new Enemy");
        }

        public override void OpenForUpdate()
        {
            OpenWindow(new EnemyAsset(enemyEditor.CurrentAsset), UpdateAsset, $"Updating {enemyEditor.CurrentAsset.Title}");
        }

        private void OpenWindow(EnemyAsset enemy, Action onConfirmAction, string headerText)
        {
            b.BuildInputField("Title", enemy.Title, window.FirstColumnContent, enemy.UpdateTitle);
            b.BuildPlainText("Created by", enemy.Author, window.FirstColumnContent);
            b.BuildPlainText("Created on", enemy.CreationDate.ToString(), window.FirstColumnContent);
            
            editedAssetBase = enemy;
            window.OpenAsPropertiesColumn1(headerText, ThemeType.Red, "Done", "Cancel", onConfirmAction, true);
        }

        protected override void CreateAsset()
        {
            editor.CreateNewEnemy((EnemyAsset)editedAssetBase);
            selectionMenu.OpenForEnemies();
        }

        protected override void UpdateAsset()
        {
            enemyEditor.UpdateAsset((EnemyAsset)editedAssetBase);
            enemyEditor.CompleteEditing();
            selectionMenu.OpenForEnemies();
        }
    }
}
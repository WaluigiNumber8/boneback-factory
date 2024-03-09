using System;
using RedRats.UI.ModalWindows;
using Rogium.Editors.Enemies;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Enemy Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderEnemy : ModalWindowPropertyBuilder
    {
        private readonly EnemyEditorOverseer enemyEditor;

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
            b.BuildInputField("Title", enemy.Title, windowColumn1, enemy.UpdateTitle);
            b.BuildPlainText("Created by", enemy.Author, windowColumn1);
            b.BuildPlainText("Created on", enemy.CreationDate.ToString(), windowColumn1);
            
            editedAssetBase = enemy;
            Open(new PropertyWindowInfo(headerText, PropertyLayoutType.Column1, "Done", "Cancel", onConfirmAction));
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
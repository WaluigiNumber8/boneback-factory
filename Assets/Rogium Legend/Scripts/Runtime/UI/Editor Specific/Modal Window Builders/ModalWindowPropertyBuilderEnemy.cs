using System;
using Rogium.Core;
using Rogium.Editors.Enemies;
using UnityEngine;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Enemy Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderEnemy : ModalWindowPropertyBuilderBase
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

        private void OpenWindow(EnemyAsset enemy, Action onConfirm, string headerText)
        {
            OpenForColumns1(headerText, onConfirm, out Transform col);
            b.BuildInputField("Title", enemy.Title, col, enemy.UpdateTitle);
            b.BuildPlainText("Created by", enemy.Author, col);
            b.BuildPlainText("Created on", enemy.CreationDate.ToString(), col);
            
            editedAssetBase = enemy;
        }

        protected override void CreateAsset()
        {
            editor.CreateNewEnemy((EnemyAsset)editedAssetBase);
            selectionMenu.Open(AssetType.Enemy);
        }

        protected override void UpdateAsset()
        {
            enemyEditor.UpdateAsset((EnemyAsset)editedAssetBase);
            enemyEditor.CompleteEditing();
            selectionMenu.Open(AssetType.Enemy);
        }
    }
}
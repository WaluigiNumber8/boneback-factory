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
        private readonly EnemyEditorOverseer enemyEditor = EnemyEditorOverseer.Instance;

        public override void OpenForCreate(Action whenConfirm = null) => OpenWindow(new EnemyAsset.Builder().Build(), () => CreateAsset(whenConfirm), "Creating a new Enemy");

        public override void OpenForUpdate(Action whenConfirm = null) => OpenWindow(new EnemyAsset.Builder().AsCopy(enemyEditor.CurrentAsset).Build(), () => UpdateAsset(whenConfirm), $"Updating {enemyEditor.CurrentAsset.Title}");

        public override void OpenForClone(Action whenConfirm = null) => OpenWindow(new EnemyAsset.Builder().AsClone(enemyEditor.CurrentAsset).Build(), () => CloneAsset(whenConfirm), $"Creating a clone of {enemyEditor.CurrentAsset.Title}");
        
        private void OpenWindow(EnemyAsset enemy, Action onConfirm, string headerText)
        {
            OpenForColumns1(headerText, onConfirm, out Transform col);
            b.BuildInputField("Title", enemy.Title, col, enemy.UpdateTitle);
            b.BuildPlainText("Created by", enemy.Author, col);
            b.BuildPlainText("Created on", enemy.CreationDate.ToString(), col);
            
            editedAssetBase = enemy;
        }

        protected override void CreateAsset(Action whenConfirm)
        {
            editor.CreateNewEnemy((EnemyAsset)editedAssetBase);
            whenConfirm?.Invoke();
        }

        protected override void UpdateAsset(Action whenConfirm)
        {
            enemyEditor.UpdateAsset((EnemyAsset)editedAssetBase);
            enemyEditor.CompleteEditing();
            whenConfirm?.Invoke();
        }
        
        protected override void CloneAsset(Action whenConfirm)
        {
            editor.CreateNewEnemy((EnemyAsset) editedAssetBase);
            whenConfirm?.Invoke();
        }
    }
}
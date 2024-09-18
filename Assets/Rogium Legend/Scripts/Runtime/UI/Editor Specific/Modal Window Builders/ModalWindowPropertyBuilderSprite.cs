using System;
using Rogium.Core;
using Rogium.Editors.Sprites;
using UnityEngine;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Palette Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderSprite : ModalWindowPropertyBuilderBase
    {
        private readonly SpriteEditorOverseer spriteEditor = SpriteEditorOverseer.Instance;

        public override void OpenForCreate(Action whenConfirm = null) => OpenWindow(new SpriteAsset.Builder().Build(), () => CreateAsset(whenConfirm), "Creating a new Sprite", AssetModificationType.Create);

        public override void OpenForUpdate(Action whenConfirm = null) => OpenWindow(new SpriteAsset.Builder().AsCopy(spriteEditor.CurrentAsset).Build() , () => UpdateAsset(whenConfirm), $"Updating {spriteEditor.CurrentAsset.Title}", AssetModificationType.Update);

        public override void OpenForClone(Action whenConfirm = null) => OpenWindow(new SpriteAsset.Builder().AsClone(spriteEditor.CurrentAsset).Build(), () => CloneAsset(whenConfirm), $"Creating a clone of {spriteEditor.CurrentAsset.Title}", AssetModificationType.Clone);
        
        private void OpenWindow(SpriteAsset asset, Action onConfirm, string headerText, AssetModificationType modification)
        {
            asset.UpdateTitle(GetTitleByModificationType(asset, modification));
            OpenForColumns1(headerText, onConfirm, out Transform col1);
            
            b.BuildInputField("Title", asset.Title, col1, asset.UpdateTitle);
            b.BuildPlainText("Created by", asset.Author, col1);
            b.BuildPlainText("Created on", asset.CreationDate.ToString(), col1);
            
            editedAssetBase = asset;
        }
        
        protected override void CreateAsset(Action whenConfirm)
        {
            editor.CreateNewSprite((SpriteAsset)editedAssetBase);
            whenConfirm?.Invoke();
        }

        protected override void UpdateAsset(Action whenConfirm)
        {
            spriteEditor.UpdateAsset((SpriteAsset)editedAssetBase);
            spriteEditor.CompleteEditing();
            whenConfirm?.Invoke();
        }

        protected override void CloneAsset(Action whenConfirm)
        {
            editor.CreateNewSprite((SpriteAsset) editedAssetBase);
            whenConfirm?.Invoke();
        }
    }
}
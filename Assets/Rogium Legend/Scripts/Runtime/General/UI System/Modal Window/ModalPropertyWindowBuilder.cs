using System;
using BoubakProductions.UI;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.PackData;
using Rogium.Global.UISystem.AssetSelection;
using Rogium.Global.UISystem.Interactables.Properties;

namespace Rogium.Global.UISystem.UI
{
    /// <summary>
    /// Contains Builder methods for various modal windows in the game.
    /// </summary>
    public class ModalPropertyWindowBuilder
    {
        private readonly ModalWindow window;
        private readonly UIPropertyBuilder propertyBuilder;
        private readonly EditorOverseer editor;
        private readonly LibraryOverseer lib;
        private IAsset assetBuilder;

        public ModalPropertyWindowBuilder()
        {
            window = CanvasOverseer.GetInstance().ModalWindow;
            propertyBuilder = UIPropertyBuilder.GetInstance();
            lib = LibraryOverseer.Instance;
            editor = EditorOverseer.Instance;
        }

        /// <summary>
        /// Opens a Modal Window as a Create Pack Window.
        /// </summary>
        public void OpenPackPropertiesCreate()
        {
            OpenPackProperties(new PackInfoAsset(), BuildPack);
        }

        /// <summary>
        /// Opens a Modal Window as aan Edit Pack Window.
        /// </summary>
        public void OpenPackPropertiesEdit()
        {
            OpenPackProperties(new PackInfoAsset(editor.CurrentPack.PackInfo), UpdatePack);
        }

        /// <summary>
        /// Opens a Modal Window as a Pack Properties Window.
        /// <param name="currentPackInfo">The PackInfo to edit.</param>
        /// <param name="onConfirmButton">What happens when the 'Confirm' button is pressed.</param>
        /// </summary>
        private void OpenPackProperties(PackInfoAsset currentPackInfo, Action onConfirmButton)
        {
            propertyBuilder.BuildInputField("Name", currentPackInfo.Title, window.LeftColumnContent, currentPackInfo.UpdateTitle);
            propertyBuilder.BuildInputFieldArea("Description", currentPackInfo.Description, window.LeftColumnContent, currentPackInfo.UpdateDescription);
            propertyBuilder.BuildSprite("",currentPackInfo.Icon, window.RightColumnContent);
            propertyBuilder.BuildPlainText("Created by", currentPackInfo.Author, window.RightColumnContent);
            propertyBuilder.BuildPlainText("Created on", currentPackInfo.CreationDate.ToString(), window.RightColumnContent);

            assetBuilder = currentPackInfo;
            window.OpenAsPropertiesColumn2("Creating a new pack", "Done", "Cancel", onConfirmButton, true);
        }

        /// <summary>
        /// Builds a pack and saves it to the library.
        /// </summary>
        private void BuildPack()
        {
            lib.CreateAndAddPack((PackInfoAsset)assetBuilder);
            AssetSelectionOverseer.GetInstance().OpenForPacks();
        }

        /// <summary>
        /// Updates a given pack.
        /// </summary>
        private void UpdatePack()
        {
            editor.CurrentPack.UpdatePackInfo((PackInfoAsset)assetBuilder);
            editor.CompleteEditing();
            AssetSelectionOverseer.GetInstance().OpenForPacks();
        }

    }
}
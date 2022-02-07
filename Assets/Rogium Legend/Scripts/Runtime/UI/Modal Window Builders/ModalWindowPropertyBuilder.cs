using BoubakProductions.UI;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using Rogium.Systems.GASExtension;
using Rogium.UserInterface.AssetSelection;
using Rogium.UserInterface.Core;
using Rogium.UserInterface.Interactables.Properties;

namespace Rogium.UserInterface.ModalWindowBuilding
{
    /// <summary>
    /// Base for all Modal Window Constructors.
    /// </summary>
    public abstract class ModalWindowPropertyBuilder
    {
        protected readonly ModalWindow window;
        protected readonly UIPropertyBuilder propertyBuilder;
        protected readonly PackEditorOverseer editor;
        protected readonly LibraryOverseer lib;
        protected readonly AssetSelectionMenu selectionMenu;
        protected AssetBase editedAssetBase;

        protected ModalWindowPropertyBuilder()
        {
            window = CanvasOverseer.GetInstance().ModalWindow;
            propertyBuilder = UIPropertyBuilder.GetInstance();
            lib = LibraryOverseer.Instance;
            editor = PackEditorOverseer.Instance;
            selectionMenu = GASContainer.GetInstance().AssetSelection;
        }

        /// <summary>
        /// Opens a Modal Window as a Creation Window.
        /// </summary>
        public abstract void OpenForCreate();

        /// <summary>
        /// Opens a Modal Window as an Edit Window.
        /// </summary>
        public abstract void OpenForUpdate();

        /// <summary>
        /// What happens, when the confirm button is pressed in the creation window variation.
        /// </summary>
        protected abstract void CreateAsset();
        
        /// <summary>
        /// What happens, when the confirm button is pressed in the update window variation.
        /// </summary>
        protected abstract void UpdateAsset();
    }
}
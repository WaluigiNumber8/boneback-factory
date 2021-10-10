using BoubakProductions.UI;
using Rogium.Editors.Core;
using Rogium.Editors.PackData;
using Rogium.Global.UISystem.AssetSelection;
using Rogium.Global.UISystem.Interactables.Properties;

namespace Rogium.Global.UISystem.UI
{
    /// <summary>
    /// Base for all Modal Window Constructors.
    /// </summary>
    public abstract class ModalWindowPropertyBuilder
    {
        protected readonly ModalWindow window;
        protected readonly UIPropertyBuilder propertyBuilder;
        protected readonly EditorOverseer editor;
        protected readonly LibraryOverseer lib;
        protected readonly AssetSelectionOverseerMono selectionMenu;
        protected AssetBase editedAssetBase;

        public ModalWindowPropertyBuilder()
        {
            window = CanvasOverseer.GetInstance().ModalWindow;
            propertyBuilder = UIPropertyBuilder.GetInstance();
            lib = LibraryOverseer.Instance;
            editor = EditorOverseer.Instance;
            selectionMenu = AssetSelectionOverseerMono.GetInstance();
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
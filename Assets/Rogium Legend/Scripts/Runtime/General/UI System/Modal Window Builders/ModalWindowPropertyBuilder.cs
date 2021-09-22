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
        protected readonly AssetSelectionOverseer selectionMenu;
        protected IAsset editedAsset;

        public ModalWindowPropertyBuilder()
        {
            window = CanvasOverseer.GetInstance().ModalWindow;
            propertyBuilder = UIPropertyBuilder.GetInstance();
            lib = LibraryOverseer.Instance;
            editor = EditorOverseer.Instance;
            selectionMenu = AssetSelectionOverseer.GetInstance();
        }

        /// <summary>
        /// Opens a Modal Window as a Creation Window.
        /// </summary>
        public abstract void OpenForCreate();

        /// <summary>
        /// Opens a Modal Window as an Edit Window.
        /// </summary>
        public abstract void OpenForUpdate();
    }
}
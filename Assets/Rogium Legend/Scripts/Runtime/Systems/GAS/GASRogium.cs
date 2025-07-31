using RedRats.Systems.Themes;
using RedRats.UI.ModalWindows;
using Rogium.Core;
using Rogium.Editors.AssetSelection;
using Rogium.UserInterface.ModalWindows;

namespace Rogium.Systems.GASExtension
{
    /// <summary>
    /// A GAS Extension class, containing specific actions to Rogium Legend.
    /// </summary>
    public static class GASRogium
    {
        public static void OpenSelectionMenu(AssetType type) => SelectionMenuOverseerMono.Instance.Open(type);

        public static void ChangeTheme(ThemeType type) => ThemeOverseerMono.Instance.ChangeTheme(type);
        public static void OpenWindow(ModalWindowData data) => ModalWindowBuilder.Instance.OpenWindow(data);
    }
}
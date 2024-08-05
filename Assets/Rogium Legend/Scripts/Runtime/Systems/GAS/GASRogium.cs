using RedRats.Systems.Themes;
using Rogium.Core;
using Rogium.UserInterface.Editors.AssetSelection;

namespace Rogium.Systems.GASExtension
{
    /// <summary>
    /// A GAS Extension class, containing specific actions to Rogium Legend.
    /// </summary>
    public static class GASRogium
    {
        public static void OpenSelectionMenu(AssetType type) => AssetSelectionMenuOverseerMono.GetInstance().Open(type);

        public static void ChangeTheme(ThemeType type) => ThemeOverseerMono.GetInstance().ChangeTheme(type);
    }
}
using RedRats.Safety;
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
        public static AssetSelectionMenu assetSelection;

        public static void OpenSelectionMenu(AssetType type)
        {
            SafetyNet.EnsureIsNotNull(assetSelection, "GAS Asset Selection");
            assetSelection.Open(type);
        }

        public static void ChangeTheme(ThemeType type)
        {
            ThemeOverseerMono.GetInstance().ChangeTheme(type);
        }
    }
}
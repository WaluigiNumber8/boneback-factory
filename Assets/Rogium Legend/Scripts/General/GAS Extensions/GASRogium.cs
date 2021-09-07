using RogiumLegend.Global.MenuSystem;

namespace RogiumLegend.GASExtensions
{
    /// <summary>
    /// A GAS Extension class, containing specific actions to Rogium Legend.
    /// </summary>
    public static class GASRogium
    {
        public static void SwitchMenu(MenuType newMenu)
        {
            MenuSwitcher.GetInstance().SwitchTo(newMenu);
        }
    }
}
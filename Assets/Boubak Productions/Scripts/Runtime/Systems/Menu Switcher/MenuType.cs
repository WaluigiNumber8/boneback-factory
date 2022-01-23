namespace BoubakProductions.UI.MenuSwitching
{
    /// <summary>
    /// All the different menu types, that can appear in the game.
    /// </summary>
    public enum MenuType
    {
        None = 0,
        MainMenu = 1,
        OptionsMenu = 2,

        AssetSelection = 3,
        AssetTypeSelection = 4,
        AssetSelectionPicker = 12,
        CampaignSelection = 11,

        CampaignEditor = 12,
        WeaponEditor = 5,
        EnemyEditor = 6,
        RoomEditor = 7,
        ProjectileEditor = 8,
        PaletteEditor = 9,
        SpriteEditor = 10,
    }
}
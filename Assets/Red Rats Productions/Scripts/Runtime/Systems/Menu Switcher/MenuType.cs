namespace RedRats.UI.MenuSwitching
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
        CampaignSelection = 5,

        CampaignEditor = 6,
        RoomEditor = 7,
        PaletteEditor = 8,
        SpriteEditor = 9,
        PropertyEditor = 10,
    }
}
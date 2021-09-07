using System.Collections;
using UnityEngine;

namespace RogiumLegend.Global.MenuSystem
{
    /// <summary>
    /// All the different menu types, that can appear in the game.
    /// </summary>
    public enum MenuType
    {
        MainMenu,
        OptionsMenu,

        AssetSelection,

        WeaponEditor,
        EnemyEditor,
        RoomEditor,
        ProjectileEditor,
        PaletteEditor,
        SpriteEditor,

        CampaignSelection,
        CampaignEditor
    }
}
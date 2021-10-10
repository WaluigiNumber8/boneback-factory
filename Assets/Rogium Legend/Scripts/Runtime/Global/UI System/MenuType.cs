using System.Collections;
using UnityEngine;

namespace Rogium.Global.UISystem
{
    /// <summary>
    /// All the different menu types, that can appear in the game.
    /// </summary>
    public enum MenuType
    {
        None,
        MainMenu,
        OptionsMenu,

        AssetSelection,
        AssetTypeSelection,

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
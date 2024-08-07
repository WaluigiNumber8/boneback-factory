using System;
using System.Collections.Generic;
using Rogium.Editors.Campaign;
using Rogium.Editors.Enemies;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;
using Rogium.Options.Core;

namespace Rogium.Editors.Core.Defaults
{
    /// <summary>
    /// Stores the Asset Difference Indicators for their IDs.
    /// </summary>
    public static class EditorAssetIDs
    {
        public const string PackIdentifier = "01";
        public const string WeaponIdentifier = "02";
        public const string EnemyIdentifier = "03";
        public const string RoomIdentifier = "04";
        public const string TileIdentifier = "05";
        public const string PaletteIdentifier = "06";
        public const string SpriteIdentifier = "07";
        public const string ProjectileIdentifier = "08";
        public const string CampaignIdentifier = "09";
        public const string PreferencesIdentifier = "10";
        
        private static readonly IDictionary<Type, string> identifiers = new Dictionary<Type, string>
        {
            {typeof(PackAsset), PackIdentifier},
            {typeof(WeaponAsset), WeaponIdentifier},
            {typeof(EnemyAsset), EnemyIdentifier},
            {typeof(RoomAsset), RoomIdentifier},
            {typeof(TileAsset), TileIdentifier},
            {typeof(PaletteAsset), PaletteIdentifier},
            {typeof(SpriteAsset), SpriteIdentifier},
            {typeof(ProjectileAsset), ProjectileIdentifier},
            {typeof(CampaignAsset), CampaignIdentifier},
            {typeof(GameDataAsset), PreferencesIdentifier}
        };
        
        public static string GetIdentifier(Type type) => identifiers[type];
    }
}
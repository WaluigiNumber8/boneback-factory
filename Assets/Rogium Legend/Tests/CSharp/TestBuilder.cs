using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using Rogium.Editors.Tiles;
using Rogium.Systems.GridSystem;

/// <summary>
/// Builds things for testing.
/// </summary>
public static class TestBuilder
{
    /// <summary>
    /// Builds a test Pack Asset.
    /// </summary>
    /// <returns></returns>
    public static PackAsset SetupPackAsset()
    {
        TileAsset tile = new TileAsset("Test Tile", EditorConstants.TileIcon, "Test Author", TileType.Floor);

        ObjectGrid<AssetData> tileGrid = new(15, 10, () => new AssetData(ParameterInfoConstants.ParamsTile));
        ObjectGrid<AssetData> objectGrid = new(15, 10, () => new AssetData(ParameterInfoConstants.ParamsEmpty));
        ObjectGrid<AssetData> enemyGrid = new(15, 10, () => new AssetData(ParameterInfoConstants.ParamsEnemy));
        tileGrid.SetValue(1, 1, new AssetData(tile.ID, ParameterInfoConstants.ParamsTile));
        tileGrid.SetValue(8, 5, new AssetData(tile.ID, ParameterInfoConstants.ParamsTile));
        tileGrid.SetValue(2, 1, new AssetData(tile.ID, ParameterInfoConstants.ParamsTile));
        tileGrid.SetValue(5, 1, new AssetData(tile.ID, ParameterInfoConstants.ParamsTile));

        RoomAsset room = new RoomAsset("Devil Room", EditorConstants.RoomIcon, "Test Author", 0, RoomType.Common, 255, tileGrid, objectGrid, enemyGrid);
        PackAsset pack = new PackAsset(new PackInfoAsset("Test Pack", EditorConstants.PackIcon, "Test Author", "Just a pack"));
        pack.Tiles.Add(tile);
        pack.Rooms.Add(room);
        return pack;
    }
}
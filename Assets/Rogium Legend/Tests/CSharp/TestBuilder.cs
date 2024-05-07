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
        TileAsset tile = new();

        ObjectGrid<AssetData> tileGrid = new(15, 10, () => new AssetData(ParameterInfoConstants.ForTile));
        ObjectGrid<AssetData> objectGrid = new(15, 10, () => new AssetData(ParameterInfoConstants.ForEmpty));
        ObjectGrid<AssetData> enemyGrid = new(15, 10, () => new AssetData(ParameterInfoConstants.ForEnemy));
        tileGrid.SetValue(1, 1, new AssetData(tile.ID, ParameterInfoConstants.ForTile));
        tileGrid.SetValue(8, 5, new AssetData(tile.ID, ParameterInfoConstants.ForTile));
        tileGrid.SetValue(2, 1, new AssetData(tile.ID, ParameterInfoConstants.ForTile));
        tileGrid.SetValue(5, 1, new AssetData(tile.ID, ParameterInfoConstants.ForTile));

        RoomAsset room = new();
        PackAsset pack = new("Test Pack", "Test Author", "Just a pack");
        pack.Tiles.Add(tile);
        pack.Rooms.Add(room);
        return pack;
    }
}
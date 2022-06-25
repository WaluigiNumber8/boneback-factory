using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using Rogium.Editors.Tiles;
using Rogium.Systems.GridSystem;
using UnityEngine;
using UnityEngine.UI;

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
        TileAsset tile = new TileAsset("Test Tile", EditorDefaults.TileIcon, "Test Author", TileType.Floor);

        ObjectGrid<AssetData> tileGrid = new(10, 15, () => new AssetData(ParameterDefaults.ParamsTile));
        ObjectGrid<AssetData> objectGrid = new(10, 15, () => new AssetData(ParameterDefaults.ParamsEmpty));
        ObjectGrid<AssetData> enemyGrid = new(10, 15, () => new AssetData(ParameterDefaults.ParamsEnemy));
        tileGrid.SetValue(1, 1, new AssetData(tile.ID, ParameterDefaults.ParamsTile));
        tileGrid.SetValue(8, 5, new AssetData(tile.ID, ParameterDefaults.ParamsTile));
        tileGrid.SetValue(2, 1, new AssetData(tile.ID, ParameterDefaults.ParamsTile));
        tileGrid.SetValue(5, 1, new AssetData(tile.ID, ParameterDefaults.ParamsTile));

        RoomAsset room = new RoomAsset("Devil Room", EditorDefaults.RoomIcon, "Test Author", 0, RoomType.Common, 255, tileGrid, objectGrid, enemyGrid);
        PackAsset pack = new PackAsset(new PackInfoAsset("Test Pack", EditorDefaults.PackIcon, "Test Author", "Just a pack"));
        pack.Tiles.Add(tile);
        pack.Rooms.Add(room);
        return pack;
    }

    /// <summary>
    /// Builds the canvas.
    /// </summary>
    /// <param name="canvas">The object holding the canvas.</param>
    public static void SetupCanvas(out GameObject canvas)
    {
        //Canvas
        canvas = new GameObject("Canvas");
        canvas.AddComponent<Canvas>();
        canvas.AddComponent<GraphicRaycaster>();
        CanvasScaler scaler = canvas.AddComponent<CanvasScaler>();

        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
    }
    
}
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using Rogium.Editors.TileData;
using Rogium.Global.GridSystem;
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

        ObjectGrid<string> tileGrid = new ObjectGrid<string>(10, 15, () => "-1");
        tileGrid.SetValue(1, 1, tile.ID);
        tileGrid.SetValue(8, 5, tile.ID);
        tileGrid.SetValue(2, 1, tile.ID);
        tileGrid.SetValue(5, 1, tile.ID);

        RoomAsset room = new RoomAsset("Devil Room", EditorDefaults.RoomIcon, "Test Author", 0, RoomType.Normal, tileGrid);
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
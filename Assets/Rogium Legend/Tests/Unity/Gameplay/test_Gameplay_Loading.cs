using System;
using System.Collections;
using NUnit.Framework;
using Rogium.Editors.Campaign;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.PackData;
using Rogium.Gameplay.Core;
using Rogium.Gameplay.DataLoading;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

public class test_Gameplay_Loading
{

    private GameplayOverseer gameplay;
    private Grid grid;
    private CampaignAsset campaign;
    private TilemapLayer[] tilemaps;
    
    [SetUp]
    public void Setup()
    {
        PackAsset pack = TestBuilder.SetupPackAsset();
        campaign = new CampaignAsset("New Campaign", EditorDefaults.CampaignIcon, "Test Author", DateTime.Now, pack);
        
        grid = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/UI/Menus/pref_Gameplay_Grid.prefab")).GetComponent<Grid>();
        
        gameplay = GameplayOverseer.Instance;
        tilemaps = new TilemapLayer[2];
        tilemaps[0] = new TilemapLayer(grid.transform.GetChild(0).GetComponent<Tilemap>());
        tilemaps[1] = new TilemapLayer(grid.transform.GetChild(1).GetComponent<Tilemap>());
    }

    [UnityTest]
    public IEnumerator prepare_game_correctly_full_tour()
    {
        gameplay.PrepareGame(campaign, tilemaps, new Vector3Int(-19, 10, 0));
        yield return new WaitForSeconds(2f);
        Assert.AreEqual(campaign.DataPack.Tiles[0].Tile, tilemaps[0].Tilemap.GetTile(new Vector3Int(-18, 11, 0)));
    }
}
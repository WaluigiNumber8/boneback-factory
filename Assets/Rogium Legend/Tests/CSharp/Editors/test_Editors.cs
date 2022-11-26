using System;
using System.Collections;
using NUnit.Framework;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using Rogium.Editors.Tiles;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Rogium_Legend.Tests.CSharp.Editors
{
    public class test_Editors
    {
        private PackEditorOverseer editor;

        [SetUp]
        public void Setup()
        {
            editor = PackEditorOverseer.Instance;

            const string packName = "Test Pack";
            const string packDescription = "Created this pack for testing purposes.";
            const string packAuthor = "TestAuthor";
            Sprite packIcon = EditorDefaults.PackIcon;

            ExternalLibraryOverseer.Instance.CreateAndAddPack(new PackInfoAsset(packName, packIcon, packAuthor, packDescription));
            ExternalLibraryOverseer.Instance.ActivatePackEditor(0);
        }

        [TearDown]
        public void Teardown()
        {
            editor.CompleteEditing();
            ExternalLibraryOverseer.Instance.DeletePack(0);
        }
        
        [Test]
        public void find_tile_with_same_ID()
        {
            editor.CreateNewTile();
            editor.CreateNewTile(new TileAsset("Devil Tile", EditorDefaults.TileIcon, "TestAuthor", TileType.Floor));
            TileAsset foundTile = editor.CurrentPack.Tiles.FindValue(editor.CurrentPack.Tiles[0].ID);
            
            Assert.AreEqual(editor.CurrentPack.Tiles[0].ID, foundTile.ID);
        }
        
        [Test]
        public void fail_when_same_id_is_not_found()
        {
            editor.CreateNewTile();
            editor.CreateNewTile(new TileAsset("Devil Tile", EditorDefaults.TileIcon, "TestAuthor", TileType.Floor));
            TileAsset tile = new TileAsset("Bob tile", EditorDefaults.TileIcon, "TestAuthor", TileType.Floor);

            try
            {
                editor.CurrentPack.Tiles.FindValue(tile.ID);
                Assert.Fail();
            }
            catch (InvalidOperationException) { }
        }

        
    }
}
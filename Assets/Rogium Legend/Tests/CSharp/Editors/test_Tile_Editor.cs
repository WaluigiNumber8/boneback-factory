using System.Collections;
using NUnit.Framework;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Editors.Tiles;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Rogium_Legend.Tests.CSharp.Editors
{
    public class test_Tile_Editor
    {
        private LibraryOverseer lib;
        private PackEditorOverseer editor;
        private TileEditorOverseer tileEditor;
        
        private PackAsset pack;
        
        [SetUp]
        public void Setup()
        {
            lib = LibraryOverseer.Instance;
            editor = PackEditorOverseer.Instance;
            tileEditor = TileEditorOverseer.Instance;
            
            if (lib.GetPacksCopy.TryFinding("Test Pack", "NO AUTHOR") != null)  lib.DeletePack("Test Pack", "NO AUTHOR");
            lib.CreateAndAddPack(new PackInfoAsset("Test Pack", EditorDefaults.PackIcon, "NO AUTHOR", "Blalalala"));
            lib.ActivatePackEditor(0);
        }

        [TearDown]
        public void Teardown()
        {
            lib.DeletePack("Test Pack", "NO AUTHOR");
        }
        
        [Test]
        public void tile_data_save_load_correctly()
        {
            TileAsset tile = new TileAsset("Test Tile", EditorDefaults.TileIcon, "NO AUTHOR", TileType.Floor);
            editor.CreateNewTile(tile);
            editor.CompleteEditing();
            lib.ReloadFromExternalStorage();
            lib.ActivatePackEditor(0);
            editor.ActivateTileEditor(0);
            
            Assert.AreEqual(tile.ID, tileEditor.CurrentAsset.ID);
            Assert.AreEqual(tile.Title, tileEditor.CurrentAsset.Title);
            Assert.AreEqual(tile.Author, tileEditor.CurrentAsset.Author);
        }

        
    }
}
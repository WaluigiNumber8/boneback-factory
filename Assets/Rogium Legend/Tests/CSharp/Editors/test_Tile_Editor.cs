using System.Collections;
using NUnit.Framework;
using Rogium.Core;
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
        private ExternalLibraryOverseer lib;
        private PackEditorOverseer editor;
        private TileEditorOverseer tileEditor;
        
        private PackAsset pack;
        
        [SetUp]
        public void Setup()
        {
            lib = ExternalLibraryOverseer.Instance;
            editor = PackEditorOverseer.Instance;
            tileEditor = TileEditorOverseer.Instance;
            
            if (lib.GetPacksCopy.FindAssetFirst("Test Pack", "NO_AUTHOR") != null)  lib.DeletePack("Test Pack", "NO_AUTHOR");
            lib.CreateAndAddPack(new PackInfoAsset("Test Pack", EditorConstants.PackIcon, "NO_AUTHOR", "Blalalala"));
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
            TileAsset tile = new TileAsset("Test Tile", EditorConstants.TileIcon, "NO AUTHOR", TileType.Floor);
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
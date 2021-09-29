using BoubakProductions.Safety;
using NUnit.Framework;
using Rogium.Editors.Core;
using Rogium.Editors.PackData;
using Rogium.ExternalStorage;
using System.IO;
using UnityEngine;
using UnityEngine.TestTools;

public class test_Pack_Save_and_Load
{
    private LibraryOverseer lib;
    private EditorOverseer editor;
    private PackInfoAsset packInfo;
    private string path;

    [SetUp]
    public void Setup()
    {
        lib = LibraryOverseer.Instance;
        editor = EditorOverseer.Instance;

        string packName = "Test Pack";
        string packDescription = "Created this pack for testing purposes.";
        string packAuthor = "TestAuthor";
        Sprite packIcon = Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f));
        packInfo = new PackInfoAsset(packName, packIcon, packAuthor, packDescription);

        path = Path.Combine(ExternalStorageOverseer.Instance.packDirectoryPath, this.packInfo.Title + ExternalStorageOverseer.Instance.packExtension);
    }

    [TearDown]
    public void Teardown()
    {
        try
        {
            lib.RemovePack(packInfo.Title, packInfo.Author);
        }
        catch (SafetyNetException)
        {
            Debug.Log($"{packInfo.Title} does not exist anymore. It might have been deleted already.");
        }
    }

    [Test]
    public void save_pack_to_storage()
    {
        lib.CreateAndAddPack(packInfo);
        Assert.AreEqual(true, File.Exists(path));
    }

    [Test]
    public void remove_pack_from_storage()
    {
        lib.CreateAndAddPack(packInfo);
        lib.RemovePack("Test Pack", "TestAuthor");

        Assert.AreEqual(true, !File.Exists(path));
    }

    [Test]
    public void fail_when_removing_non_existing_packs_1()
    {
        try
        {
            lib.RemovePack("Stupid Pack", "Idiota");
            Assert.Fail();
        }
        catch (SafetyNetException)
        {

        }
    }

    [Test]
    public void fail_when_removing_non_existing_packs_2()
    {
        try
        {
            lib.RemovePack("Test Pack", "TestAuthor");
            Assert.Fail();
        }
        catch (SafetyNetException)
        {

        }
    }

    [Test]
    public void try_to_save_pack_with_same_name()
    {
        lib.CreateAndAddPack(packInfo);
        try
        {
            lib.CreateAndAddPack(packInfo);
            Assert.Fail();
        }
        catch (SafetyNetCollectionException) { }
    }

    [Test]
    public void pack_data_did_load()
    {
        lib.CreateAndAddPack(packInfo);
        lib.ReloadFromExternalStorage();

        Assert.AreEqual(1, lib.PackCount);
    }

    [Test]
    public void pack_data_loaded_correctly()
    {
        lib.CreateAndAddPack(packInfo);
        lib.ReloadFromExternalStorage();

        PackAsset foundPack = lib.GetCopy.TryFinding("Test Pack", "TestAuthor");

        byte[] currentBytes = ImageConversion.EncodeToPNG(packInfo.Icon.texture);
        byte[] foundBytes = ImageConversion.EncodeToPNG(foundPack.Icon.texture);

        Assert.AreEqual(packInfo.Title, foundPack.Title);
        Assert.AreEqual(packInfo.Description, foundPack.PackInfo.Description);
        Assert.AreEqual(packInfo.Author, foundPack.Author);
        Assert.AreEqual(currentBytes, foundBytes);
    }

    [Test]
    public void update_pack_details()
    {
        PackInfoAsset newInfo = new PackInfoAsset(packInfo.ID, packInfo.Title, packInfo.Icon, packInfo.Author, "This pack has now been updated", packInfo.CreationDate);
        lib.CreateAndAddPack(packInfo);
        lib.ActivatePackEditor(0);
        PackInfoAsset oldInfo = editor.CurrentPack.PackInfo;

        editor.CurrentPack.UpdatePackInfo(newInfo);
        
        Assert.AreNotEqual(oldInfo.Description, editor.CurrentPack.PackInfo.Description);
    }

}

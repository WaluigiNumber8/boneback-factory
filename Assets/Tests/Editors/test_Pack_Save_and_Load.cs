using NUnit.Framework;
using RogiumLegend.Editors.PackData;
using RogiumLegend.ExternalStorage;
using RogiumLegend.Global.SafetyChecks;
using System.IO;
using UnityEngine;
using UnityEngine.TestTools;

public class test_Pack_Save_and_Load
{
    private PackAsset pack;
    private string path;
    private LibraryOverseer lib;

    [SetUp]
    public void Setup()
    {
        lib = LibraryOverseer.Instance;

        string packName = "Test Pack";
        string packDescription = "Created this pack for testing purposes.";
        string packAuthor = "TestAuthor";
        Sprite packIcon = Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f));

        PackBuilder packBuilder = new PackBuilder();
        pack = packBuilder.BuildPack(packName, packDescription, packAuthor, packIcon);
        
        path = Path.Combine(ExternalStorageOverseer.Instance.packDirectoryPath, pack.packName + ExternalStorageOverseer.Instance.packExtension);
    }

    [TearDown]
    public void Teardown()
    {
        try
        {
            lib.Library.Remove(pack.packName, pack.author);
        }
        catch (SafetyNetException)
        {
            Debug.Log($"{pack.packName} does not exist anymore. It might have been deleted already.");
        }
    }

    [Test]
    public void save_pack_to_storage()
    {
        lib.Library.Add(pack);
        Assert.AreEqual(true, File.Exists(path));
    }

    [Test]
    public void remove_pack_from_storage()
    {
        lib.Library.Add(pack);
        lib.Library.Remove("Test Pack", "TestAuthor");

        Assert.AreEqual(true, !File.Exists(path));
    }

    [Test]
    public void fail_when_removing_non_existing_packs_1()
    {
        try
        {
            lib.Library.Remove("Stupid Pack", "Idiota");
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
            lib.Library.Remove("Test Pack", "TestAuthor");
            Assert.Fail();
        }
        catch (SafetyNetException)
        {

        }
    }

    [Test]
    public void try_to_save_pack_with_same_name()
    {
        lib.Library.Add(pack);
        try
        {
            lib.Library.Add(pack);
            Assert.Fail();
        }
        catch (SafetyNetCollectionException) { }
    }

    [Test]
    public void pack_data_did_load()
    {
        lib.Library.Add(pack);
        lib.ReloadFromExternalStorage();

        Assert.AreEqual(1, lib.Library.Count);
    }

    [Test]
    public void pack_data_loaded_correctly()
    {
        lib.Library.Add(pack);
        lib.ReloadFromExternalStorage();

        PackAsset foundPack = lib.Library.TryFinding("Test Pack", "TestAuthor");

        byte[] currentBytes = ImageConversion.EncodeToPNG(pack.icon.texture);
        byte[] foundBytes = ImageConversion.EncodeToPNG(foundPack.icon.texture);

        Assert.AreEqual(pack.packName, foundPack.packName);
        Assert.AreEqual(pack.description, foundPack.description);
        Assert.AreEqual(pack.author, foundPack.author);
        Assert.AreEqual(currentBytes, foundBytes);
    }

}

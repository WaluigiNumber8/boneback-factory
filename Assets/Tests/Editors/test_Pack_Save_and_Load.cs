using NUnit.Framework;
using RogiumLegend.Editors.PackData;
using RogiumLegend.ExternalStorage;
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
        
        path = Path.Combine(ExternalStorageOverseer.Instance.packDirectoryPath, pack.PackName + ExternalStorageOverseer.Instance.packExtension);
    }

    [TearDown]
    public void Teardown()
    {
        try
        {
            lib.Library.Remove(pack.PackName, pack.PackName);

        }
        catch (MissingReferenceException)
        {

            Debug.Log($"{pack.PackName} does not exist anymore. It might have been deleted already.");
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
    public void try_to_save_pack_with_same_name()
    {
        lib.Library.Add(pack);
        lib.Library.Add(pack);

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

        PackAsset foundPack = lib.Library.Find("Test Pack", "TestAuthor");

        byte[] currentBytes = ImageConversion.EncodeToPNG(pack.Icon.texture);
        byte[] foundBytes = ImageConversion.EncodeToPNG(foundPack.Icon.texture);

        Assert.AreEqual(pack.PackName, foundPack.PackName);
        Assert.AreEqual(pack.Description, foundPack.Description);
        Assert.AreEqual(pack.Author, foundPack.Author);
        Assert.AreEqual(currentBytes, foundBytes);
    }

}

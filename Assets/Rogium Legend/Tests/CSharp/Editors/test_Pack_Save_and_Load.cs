using RedRats.Safety;
using NUnit.Framework;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using Rogium.ExternalStorage;
using System.IO;
using Rogium.Core;
using UnityEngine;
using UnityEngine.TestTools;

public class test_Pack_Save_and_Load
{
    private ExternalLibraryOverseer lib;
    private PackEditorOverseer editor;
    private string path;
    private string packTitle;
    private string packDescription;
    private string packAuthor;

    [SetUp]
    public void Setup()
    {
        lib = ExternalLibraryOverseer.Instance;
        editor = PackEditorOverseer.Instance;

        packTitle = "Test Pack";
        packDescription = "Created this pack for testing purposes.";
        packAuthor = "TestAuthor";

        path = Path.Combine(Application.persistentDataPath, "Packs", $"{packTitle}.bumpack");
    }

    [TearDown]
    public void Teardown()
    {
        try
        {
            lib.DeletePack(packTitle, packAuthor);
        }
        catch (SafetyNetException)
        {
            Debug.Log($"{packTitle} does not exist anymore. It might have been deleted already.");
        }
    }

    [Test]
    public void save_pack_to_storage()
    {
        lib.CreateAndAddPack(new PackAsset(packTitle, packAuthor, packDescription));
        Assert.AreEqual(true, File.Exists(path));
    }

    [Test]
    public void remove_pack_from_storage()
    {
        lib.CreateAndAddPack(new PackAsset(packTitle, packAuthor, packDescription));
        lib.DeletePack("Test Pack", "TestAuthor");

        Assert.AreEqual(true, !File.Exists(path));
    }

    [Test]
    public void fail_when_removing_non_existing_packs_1()
    {
        try
        {
            lib.DeletePack("Stupid Pack", "Idiota");
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
            lib.DeletePack("Test Pack", "TestAuthor");
            Assert.Fail();
        }
        catch (SafetyNetException)
        {

        }
    }

    [Test]
    public void try_to_save_pack_with_same_name()
    {
        lib.CreateAndAddPack(new PackAsset(packTitle, packAuthor, packDescription));
        try
        {
            lib.CreateAndAddPack(new PackAsset(packTitle, packAuthor, packDescription));
            Assert.Fail();
        }
        catch (FoundDuplicationException) { }
    }

    [Test]
    public void pack_data_did_load()
    {
        lib.CreateAndAddPack(new PackAsset(packTitle, packAuthor, packDescription));
        lib.ReloadFromExternalStorage();

        Assert.AreEqual(1, lib.PackCount);
    }

    [Test]
    public void pack_data_loaded_correctly()
    {
        PackAsset pack = new PackAsset(packTitle, packAuthor, packDescription);
        lib.CreateAndAddPack(pack);
        lib.ReloadFromExternalStorage();

        PackAsset foundPack = lib.GetPacksCopy.FindValueFirst(pack.ID);

        byte[] currentBytes = ImageConversion.EncodeToPNG(pack.Icon.texture);
        byte[] foundBytes = ImageConversion.EncodeToPNG(foundPack.Icon.texture);

        Assert.AreEqual(packTitle, foundPack.Title);
        Assert.AreEqual(packDescription, foundPack.Description);
        Assert.AreEqual(packAuthor, foundPack.Author);
        Assert.AreEqual(currentBytes, foundBytes);
    }

}

using NUnit.Framework;
using Rogium.Editors.Packs;
using UnityEngine;
using UnityEngine.TestTools;

public class test_Pack_Library
{
    private PackInfoAsset packInfo;
    private LibraryOverseer lib;

    [SetUp]
    public void Setup()
    {
        lib = LibraryOverseer.Instance;

        string packName = "Test Pack";
        string packDescription = "Created this pack for testing purposes.";
        string packAuthor = "TestAuthor";
        Sprite packIcon = Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f));
        
        packInfo = new PackInfoAsset(packName, packIcon, packAuthor, packDescription);
    }

    [TearDown]
    public void Teardown()
    {
        lib.DeletePack(packInfo.Title, packInfo.Author);
    }

    [Test]
    public void create_new_pack_and_it_gets_saved()
    {
        int amountBefore = lib.PackCount;

        lib.CreateAndAddPack(packInfo);

        Assert.AreEqual(amountBefore + 1, lib.PackCount);
    }

    [Test]
    public void create_new_pack_and_check_if_data_is_correct()
    {
        lib.CreateAndAddPack(packInfo);
        PackAsset foundPack = lib.GetPacksCopy.TryFinding("Test Pack", "TestAuthor");
        Assert.AreEqual(packInfo.Title, foundPack.Title);
    }
}

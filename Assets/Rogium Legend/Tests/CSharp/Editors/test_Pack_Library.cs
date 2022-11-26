using NUnit.Framework;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using UnityEngine;

public class test_Pack_Library
{
    private PackInfoAsset packInfo;
    private ExternalLibraryOverseer lib;

    [SetUp]
    public void Setup()
    {
        lib = ExternalLibraryOverseer.Instance;

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
        PackAsset foundPack = lib.GetPacksCopy.FindValue(packInfo.ID);
        Assert.AreEqual(packInfo.Title, foundPack.Title);
    }
}

using NUnit.Framework;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using UnityEngine;

public class test_Pack_Library
{
    private ExternalLibraryOverseer lib;
    private string packTitle;
    private string packDescription;
    private string packAuthor;

    [SetUp]
    public void Setup()
    {
        lib = ExternalLibraryOverseer.Instance;

        packTitle = "Test Pack";
        packDescription = "Created this pack for testing purposes.";
        packAuthor = "TestAuthor";
        
    }

    [TearDown]
    public void Teardown()
    {
        lib.DeletePack(packTitle, packAuthor);
    }

    [Test]
    public void create_new_pack_and_it_gets_saved()
    {
        int amountBefore = lib.PackCount;

        lib.CreateAndAddPack(new PackAsset(packTitle, packAuthor, packDescription));

        Assert.AreEqual(amountBefore + 1, lib.PackCount);
    }

    [Test]
    public void create_new_pack_and_check_if_data_is_correct()
    {
        PackAsset pack = new(packTitle, packAuthor, packDescription);
        lib.CreateAndAddPack(pack);
        PackAsset foundPack = lib.GetPacksCopy.FindValueFirst(pack.ID);
        Assert.AreEqual(pack.Title, foundPack.Title);
    }
}

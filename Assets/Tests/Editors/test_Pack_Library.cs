using NUnit.Framework;
using RogiumLegend.Editors.PackData;
using UnityEngine;
using UnityEngine.TestTools;

public class test_Pack_Library
{
    private PackAsset pack;
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
    }

    [TearDown]
    public void Teardown()
    {
        lib.Library.Remove(pack.PackName, pack.Author);
    }

    [Test]
    public void create_new_pack_and_it_gets_saved()
    {
        int amountBefore = lib.Library.Count;

        lib.Library.Add(pack);

        Assert.AreEqual(amountBefore + 1, lib.Library.Count);
    }

    [Test]
    public void create_new_pack_and_check_if_data_is_correct()
    {
        lib.Library.Add(pack);
        PackAsset foundPack = lib.Library.Find("Test Pack", "TestAuthor");
        Assert.AreEqual(pack.PackName, foundPack.PackName);
    }

}

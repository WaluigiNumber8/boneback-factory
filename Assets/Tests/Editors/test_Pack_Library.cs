using NUnit.Framework;
using RogiumLegend.Editors.PackData;
using UnityEngine;
using UnityEngine.TestTools;

public class test_Pack_Library
{

    [Test]
    public void create_new_pack_and_it_gets_saved()
    {
        int amountBefore = PackLibraryOverseer.Instance.GetPackAmount;

        //Create Pack
        string packName = "Test Pack";
        string packDescription = "Created this pack for testing purposes.";
        string packAuthor = "TestAuthor";
        Sprite packIcon = Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f));

        PackBuilder packBuilder = new PackBuilder();

        packBuilder.CreateNewPack(packName, packDescription, packAuthor, packIcon);

        //Test if pack was put to the library.
        Assert.AreEqual(amountBefore + 1, PackLibraryOverseer.Instance.GetPackAmount);

        PackLibraryOverseer.Instance.RemovePackByName(packName);
    }

    [Test]
    public void create_new_pack_and_check_if_data_is_correct()
    {
        //Create Pack
        string packName = "Test Pack";
        string packDescription = "Created this pack for testing purposes.";
        string packAuthor = "TestAuthor";
        Sprite packIcon = Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f));

        PackBuilder packBuilder = new PackBuilder();

        packBuilder.CreateNewPack(packName, packDescription, packAuthor, packIcon);
        PackAsset foundPack = PackLibraryOverseer.Instance.FindPackByName("Test Pack");

        //Test if pack is the same.
        Assert.AreEqual(packName, foundPack.packName);

        PackLibraryOverseer.Instance.RemovePackByName(packName);
    }

    [Test]
    public void is_the_library_empty()
    {
        Assert.AreEqual(0, PackLibraryOverseer.Instance.GetPackAmount);
    }


}

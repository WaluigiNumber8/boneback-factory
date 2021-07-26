using NUnit.Framework;
using RogiumLegend.Editors.PackData;
using UnityEngine;
using UnityEngine.TestTools;

public class test_Pack_Save_and_Load
{
    [Test]
    public void save_pack_to_harddrive()
    {
        //Create Pack
        string packName = "Test Pack";
        string packDescription = "Created this pack for testing purposes.";
        string packAuthor = "TestAuthor";
        Sprite packIcon = Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f));

        PackBuilder packBuilder = new PackBuilder();

        packBuilder.CreateNewPack(packName, packDescription, packAuthor, packIcon);       

        PackLibraryOverseer.Instance.RemovePackByName(packName);
    }
}

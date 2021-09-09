using NUnit.Framework;
using RogiumLegend.Editors.Core;
using RogiumLegend.Editors.PackData;
using RogiumLegend.Editors.RoomData;
using UnityEngine;
using UnityEngine.TestTools;

public class test_Room_Editor
{
    private LibraryOverseer lib;
    private EditorOverseer editor;
    private RoomEditorOverseer roomEditor;
    private PackInfoAsset packInfo;

    [SetUp]
    public void Setup()
    {
        lib = LibraryOverseer.Instance;
        editor = EditorOverseer.Instance;
        roomEditor = RoomEditorOverseer.Instance;

        string packName = "Test Pack";
        string packDescription = "Created this pack for testing purposes.";
        string packAuthor = "TestAuthor";
        Sprite packIcon = Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f));

        packInfo = new PackInfoAsset(packName, packIcon, packAuthor, packDescription);
    }

    [TearDown]
    public void Teardown()
    {
            lib.RemovePack(packInfo.Title, packInfo.Author);
    }

    [Test]
    public void create_new_room()
    {
        lib.CreateAndAddPack(packInfo);
        lib.ActivatePackEditor(0);

        int roomsBefore = editor.CurrentPack.Rooms.Count;
        editor.CreateNewRoom();

        Assert.AreEqual(roomsBefore + 1, editor.CurrentPack.Rooms.Count);
    }

    [Test]
    public void ensure_room_data_saves_correctly1()
    {
        lib.CreateAndAddPack(packInfo);
        lib.ActivatePackEditor(0);

        editor.CreateNewRoom();
        string roomName = editor.CurrentPack.Rooms[0].Title;

        editor.CompleteEditing();
        lib.ActivatePackEditor(0);

        Assert.AreEqual(roomName, editor.CurrentPack.Rooms[0].Title);
    }

    [Test]
    public void ensure_room_data_saves_correctly2()
    {
        lib.CreateAndAddPack(packInfo);
        lib.ActivatePackEditor(0);

        editor.CreateNewRoom();
        string roomName = editor.CurrentPack.Rooms[0].Title;

        editor.CompleteEditing();
        lib.ReloadFromExternalStorage();
        lib.ActivatePackEditor(0);

        Assert.AreEqual(roomName, editor.CurrentPack.Rooms[0].Title);
    }

    [Test]
    public void remove_room_from_pack()
    {
        lib.CreateAndAddPack(packInfo);
        lib.ActivatePackEditor(0);

        editor.CreateNewRoom();
        int roomsBefore = editor.CurrentPack.Rooms.Count;

        editor.CurrentPack.Rooms.RemoveAt(0);

        Assert.Less(editor.CurrentPack.Rooms.Count, roomsBefore);
    }

}

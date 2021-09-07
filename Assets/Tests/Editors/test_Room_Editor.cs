using NUnit.Framework;
using RogiumLegend.Editors.Core;
using RogiumLegend.Editors.PackData;
using RogiumLegend.Editors.RoomData;
using UnityEngine;
using UnityEngine.TestTools;

public class test_Room_Editor
{
    private LibraryOverseer lib;
    private EditorOverseer packEditor;
    private RoomEditorOverseer roomEditor;
    private PackAsset pack;

    [SetUp]
    public void Setup()
    {
        lib = LibraryOverseer.Instance;
        packEditor = EditorOverseer.Instance;
        roomEditor = RoomEditorOverseer.Instance;

        string packName = "Test Pack";
        string packDescription = "Created this pack for testing purposes.";
        string packAuthor = "TestAuthor";
        Sprite packIcon = Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f));

        pack = new PackAsset(new PackInfoAsset(packName, packDescription, packAuthor, packIcon));
        lib.Library.Add(pack);

        lib.ActivatePackEditor(0);
    }

    [TearDown]
    public void Teardown()
    {
            lib.Library.Remove(pack.PackInfo.packName, pack.PackInfo.author);
    }

    [Test]
    public void create_new_room()
    {
        int roomsBefore = packEditor.CurrentPack.Rooms.Count;
        packEditor.CreateNewRoom();

        Assert.AreEqual(roomsBefore + 1, packEditor.CurrentPack.Rooms.Count);
    }

    [Test]
    public void ensure_room_data_saves_correctly1()
    {
        packEditor.CreateNewRoom();
        string roomName = packEditor.CurrentPack.Rooms[0].Title;

        packEditor.CompleteEditing();
        lib.ActivatePackEditor(0);

        Assert.AreEqual(roomName, packEditor.CurrentPack.Rooms[0].Title);
    }

    [Test]
    public void ensure_room_data_saves_correctly2()
    {
        packEditor.CreateNewRoom();
        string roomName = packEditor.CurrentPack.Rooms[0].Title;

        packEditor.CompleteEditing();
        lib.ReloadFromExternalStorage();
        lib.ActivatePackEditor(0);

        Assert.AreEqual(roomName, packEditor.CurrentPack.Rooms[0].Title);
    }

    [Test]
    public void remove_room_from_pack()
    {
        packEditor.CreateNewRoom();
        int roomsBefore = packEditor.CurrentPack.Rooms.Count;

        packEditor.CurrentPack.Rooms.RemoveAt(0);

        Assert.Less(packEditor.CurrentPack.Rooms.Count, roomsBefore);
    }

}

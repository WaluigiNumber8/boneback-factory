using NUnit.Framework;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using UnityEngine;
using UnityEngine.TestTools;

public class test_Room_Editor
{
    private ExternalLibraryOverseer lib;
    private PackEditorOverseer editor;
    private RoomEditorOverseer roomEditor;
    private PackInfoAsset packInfo;

    [SetUp]
    public void Setup()
    {
        lib = ExternalLibraryOverseer.Instance;
        editor = PackEditorOverseer.Instance;
        roomEditor = RoomEditorOverseer.Instance;

        const string packName = "Test Pack";
        const string packDescription = "Created this pack for testing purposes.";
        const string packAuthor = "TestAuthor";
        Sprite packIcon = EditorDefaults.PackIcon;

        lib.CreateAndAddPack(new PackInfoAsset(packName, packIcon, packAuthor, packDescription));
        lib.ActivatePackEditor(0);
    }

    [TearDown]
    public void Teardown()
    {
        lib.DeletePack(packInfo.Title, packInfo.Author);
    }

    [Test]
    public void create_new_room()
    {
        int roomsBefore = editor.CurrentPack.Rooms.Count;
        editor.CreateNewRoom();

        Assert.AreEqual(roomsBefore + 1, editor.CurrentPack.Rooms.Count);
    }

    [Test]
    public void ensure_room_data_saves_correctly1()
    {
        editor.CreateNewRoom();
        string roomName = editor.CurrentPack.Rooms[0].Title;

        editor.CompleteEditing();
        lib.ActivatePackEditor(0);

        Assert.AreEqual(roomName, editor.CurrentPack.Rooms[0].Title);
    }

    [Test]
    public void ensure_room_data_saves_correctly2()
    {
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
        editor.CreateNewRoom();
        int roomsBefore = editor.CurrentPack.Rooms.Count;

        editor.CurrentPack.Rooms.RemoveAt(0);

        Assert.Less(editor.CurrentPack.Rooms.Count, roomsBefore);
    }

    public void grid_fills_correctly_with_all_data()
    {
        
    }

}

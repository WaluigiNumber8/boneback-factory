
namespace Rogium.UserInterface.Interactables
{
    public enum ButtonType
    {
        DoNothing = 0,
        OpenOptionsMenu = 75,
        OpenChangelog = 81,

        #region Open Selection Menus
        SelectionOpenPack = 1,
        SelectionOpenPalette = 2,
        SelectionOpenSprite = 3,
        SelectionOpenWeapon = 4,
        SelectionOpenEnemy = 5,
        SelectionOpenRoom = 6,
        SelectionOpenTile = 7,
        SelectionOpenProjectile = 8,
        SelectionOpenCampaign = 9,
        SelectionOpenAssetType = 10,
        #endregion

        #region Return from Menus
        ReturnToAssetTypeSelection = 11,
        ReturnToMainMenuFromSelection = 12,
        ReturnToMainMenuFromOptions = 74,
        ReturnToMainMenuFromChangelog = 82,

        #endregion

        #region Create Assets
        CreatePack = 13,
        CreatePalette = 14,
        CreateSprite = 15,
        CreateWeapon = 16,
        CreateEnemy = 17,
        CreateRoom = 18,
        CreateTile = 19,
        CreateProjectile = 20,
        CreateCampaign = 21,
        #endregion

        #region Edit Asset Properties
        EditPackProperties = 22,
        EditPaletteProperties = 23,
        EditSpriteProperties = 24,
        EditWeaponProperties = 25,
        EditEnemyProperties = 26,
        EditRoomProperties = 27,
        EditTileProperties = 28,
        EditProjectileProperties = 29,
        EditCampaignProperties = 30,
        #endregion

        #region Delete Assets
        DeletePack = 31,
        DeletePalette = 32,
        DeleteSprite = 33,
        DeleteWeapon = 34,
        DeleteEnemy = 35,
        DeleteRoom = 36,
        DeleteTile = 37,
        DeleteProjectile = 38,
        DeleteCampaign = 39,
        #endregion

        #region Open Editors
        EditorOpenPalette = 40,
        EditorOpenSprite = 41,
        EditorOpenWeapon = 42,
        EditorOpenEnemy = 43,
        EditorOpenRoom = 44,
        EditorOpenTile = 45,
        EditorOpenProjectile = 46,
        EditorOpenCampaign = 47,
        #endregion

        #region Save Editor Changes

        SaveChangesPalette = 48,
        SaveChangesSprite = 49,
        SaveChangesWeapon = 50,
        SaveChangesEnemy = 51,
        SaveChangesRoom = 52,
        SaveChangesTile = 53,
        SaveChangesProjectile = 54,
        SaveChangesCampaign = 55,

        #endregion

        #region Cancel Editor Changes
        CancelChangesPalette = 56,
        CancelChangesSprite = 57,
        CancelChangesWeapon = 58,
        CancelChangesEnemy = 59,
        CancelChangesRoom = 60,
        CancelChangesTile = 61,
        CancelChangesProjectile = 62,
        CancelChangesCampaign = 63,
        #endregion

        #region Campaign Selection Menu
        CampaignEditorSelectAll = 66,
        CampaignEditorSelectNone = 67,
        CampaignEditorSelectRandom = 68,
        CampaignEditorChangeImportState = 69,
        #endregion
        
        #region Campaign Editor
        CampaignShowNext = 64,
        CampaignShowPrevious = 65,
        #endregion

        #region Sprite Editor
        SpriteSwitchTool = 70,
        SpriteSwitchPalette = 72,
        SpriteClearActiveLayer = 77,
        #endregion

        #region Room Editor
        RoomSwitchTool = 71,
        RoomSwitchPalette = 73,
        RoomClearActiveLayer = 76,
        #endregion

        #region Gameplay
        GameplayPauseResume = 78,
        GameplayPauseQuit = 79,
        GameplaySelectWeapon = 80,
        #endregion
        
        QuitGame = 9999,
        Play = 10000,
        TEST = 10001
        
        //Latest : 82
    }   
}
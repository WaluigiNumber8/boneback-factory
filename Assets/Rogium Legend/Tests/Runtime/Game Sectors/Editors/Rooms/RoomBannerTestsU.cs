using System.Collections;
using Rogium.Editors.AssetSelection;
using Rogium.Editors.Rooms;
using Rogium.Systems.GASExtension;
using Rogium.Tests.Core;
using Rogium.Tests.Editors.AssetSelection;
using UnityEngine;

namespace Rogium.Tests.Editors.Rooms
{
    public static class RoomBannerTestsU
    {
        public static IEnumerator SelectRoomAndUpdateTileGridThenSave()
        {
            yield return TUtilsMenuLoader.PrepareSelectionMenu();
            yield return SelectionInfoColumnTestsU.OpenPackAndSelectRoom();
            ((EditableAssetCardController)SelectionMenuOverseerMono.Instance.CurrentSelector.GetCard(0)).Edit();
            yield return null;
            RoomEditorOverseerMono.Instance.UpdateGridCell(new Vector2Int(0, 0));
            RoomEditorOverseerMono.Instance.UpdateGridCell(new Vector2Int(1, 0));
            GASActions.SaveChangesRoom();
            yield return null;
        }
    }
}
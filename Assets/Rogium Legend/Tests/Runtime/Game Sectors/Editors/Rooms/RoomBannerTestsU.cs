using System.Collections;
using Rogium.Editors.NewAssetSelection;
using Rogium.Editors.Packs;
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
            yield return MenuLoader.PrepareSelectionMenuV2();
            yield return SelectionInfoColumnTestsU.OpenPackAndSelectRoom();
            ((EditableAssetCardControllerV2)SelectionMenuOverseerMono.GetInstance().CurrentSelector.GetCard(0)).Edit();
            yield return null;
            RoomEditorOverseerMono.GetInstance().UpdateGridCell(new Vector2Int(0, 0));
            RoomEditorOverseerMono.GetInstance().UpdateGridCell(new Vector2Int(1, 0));
            GASButtonActions.SaveChangesRoom();
            yield return null;
        }
    }
}
using Rogium.Core;
using Rogium.Editors.AssetSelection;

namespace Rogium.Tests.Editors.AssetSelection
{
    public static class SelectionMenuOverseerMonoTestsU
    {
        public static void OpenPackSelectionAndEditFirstPack()
        {
            SelectionMenuOverseerMono.Instance.Open(AssetType.Pack);
            SelectionMenuOverseerMono.Instance.CurrentSelector.Content.GetChild(1).GetComponent<EditableAssetCardController>().Edit();
        }
    }
}
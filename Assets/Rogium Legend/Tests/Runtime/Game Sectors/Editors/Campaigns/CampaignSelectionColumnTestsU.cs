using System.Collections;
using Rogium.Editors.Campaign;

namespace Rogium.Tests.Editors.Campaigns
{
    public static class CampaignSelectionColumnTestsU
    {
        public static IEnumerator SelectCard(int index = 0)
        {
            CampaignEditorOverseerMono.GetInstance().SelectionPicker.Selector.GetCard(index).SetToggle(true);
            return null;
        }
    }
}
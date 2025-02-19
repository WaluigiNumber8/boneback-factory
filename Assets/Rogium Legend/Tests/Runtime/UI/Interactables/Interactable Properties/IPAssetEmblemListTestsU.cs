using System.Collections;
using System.Linq;
using Rogium.Editors.Packs;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Tests.UI.Interactables.Properties
{
    public static class IPAssetEmblemListTestsU
    {
        public static IEnumerator CreateEmblemListOfPalettes()
        {
            UIPropertyBuilder.Instance.BuildAssetEmblemList("Test", PackEditorOverseer.Instance.CurrentPack.Palettes.Select(pal => pal.Icon).ToList(), Object.FindFirstObjectByType<Canvas>().transform);
            yield return null;
        }
    }
}
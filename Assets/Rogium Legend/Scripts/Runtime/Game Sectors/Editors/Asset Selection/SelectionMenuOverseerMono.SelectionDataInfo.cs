using System;

namespace Rogium.Editors.AssetSelection
{
    public sealed partial class SelectionMenuOverseerMono
    {
        [Serializable]
        public struct SelectionDataInfo
        {
            public SelectionMenuData packSelection;
            public SelectionMenuData paletteSelection;
            public SelectionMenuData spriteSelection;
            public SelectionMenuData weaponSelection;
            public SelectionMenuData projectileSelection;
            public SelectionMenuData enemySelection;
            public SelectionMenuData roomSelection;
            public SelectionMenuData tileSelection;
            
            public void SubscribeToCardSelection(Action<int> action)
            {
                packSelection.AssetSelector.OnSelectCard += action;
                paletteSelection.AssetSelector.OnSelectCard += action;
                spriteSelection.AssetSelector.OnSelectCard += action;
                weaponSelection.AssetSelector.OnSelectCard += action;
                projectileSelection.AssetSelector.OnSelectCard += action;
                enemySelection.AssetSelector.OnSelectCard += action;
                roomSelection.AssetSelector.OnSelectCard += action;
                tileSelection.AssetSelector.OnSelectCard += action;
            }
            
            public void SubscribeToNoSelection(Action action)
            {
                packSelection.AssetSelector.OnSelectNone += action;
                paletteSelection.AssetSelector.OnSelectNone += action;
                spriteSelection.AssetSelector.OnSelectNone += action;
                weaponSelection.AssetSelector.OnSelectNone += action;
                projectileSelection.AssetSelector.OnSelectNone += action;
                enemySelection.AssetSelector.OnSelectNone += action;
                roomSelection.AssetSelector.OnSelectNone += action;
                tileSelection.AssetSelector.OnSelectNone += action;
            }
            
            public void UnsubscribeFromCardSelection(Action<int> action)
            {
                packSelection.AssetSelector.OnSelectCard -= action;
                paletteSelection.AssetSelector.OnSelectCard -= action;
                spriteSelection.AssetSelector.OnSelectCard -= action;
                weaponSelection.AssetSelector.OnSelectCard -= action;
                projectileSelection.AssetSelector.OnSelectCard -= action;
                enemySelection.AssetSelector.OnSelectCard -= action;
                roomSelection.AssetSelector.OnSelectCard -= action;
                tileSelection.AssetSelector.OnSelectCard -= action;
            }
            
            public void UnsubscribeFromNoSelection(Action action)
            {
                packSelection.AssetSelector.OnSelectNone -= action;
                paletteSelection.AssetSelector.OnSelectNone -= action;
                spriteSelection.AssetSelector.OnSelectNone -= action;
                weaponSelection.AssetSelector.OnSelectNone -= action;
                projectileSelection.AssetSelector.OnSelectNone -= action;
                enemySelection.AssetSelector.OnSelectNone -= action;
                roomSelection.AssetSelector.OnSelectNone -= action;
                tileSelection.AssetSelector.OnSelectNone -= action;
            }
        }
    }
}
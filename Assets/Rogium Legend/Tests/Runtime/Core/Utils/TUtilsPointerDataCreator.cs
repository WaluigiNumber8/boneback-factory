using UnityEngine.EventSystems;

namespace Rogium.Tests.Core
{
    /// <summary>
    /// Creates test <see cref="PointerEventData"/>.
    /// </summary>
    public static class TUtilsPointerDataCreator
    {
        public static PointerEventData LeftClick() => new(EventSystem.current) { button = PointerEventData.InputButton.Left };
        public static PointerEventData RightClick() => new(EventSystem.current) { button = PointerEventData.InputButton.Right };
    }
}
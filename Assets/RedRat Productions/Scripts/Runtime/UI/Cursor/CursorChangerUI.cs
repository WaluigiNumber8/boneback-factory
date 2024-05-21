using UnityEngine;

namespace RedRats.UI.Core.Cursors
{
    /// <summary>
    /// Changes the cursor to a type when hovering over a UI element.
    /// </summary>
    public class CursorChangerUI : CursorChangerBase
    {
        [SerializeField] private CursorType type = CursorType.Interact;
        
        protected override CursorType CursorToSet => type;
    }
}
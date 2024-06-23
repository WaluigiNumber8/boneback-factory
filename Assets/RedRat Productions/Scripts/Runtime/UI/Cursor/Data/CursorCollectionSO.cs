using UnityEngine;

namespace RedRats.UI.Core.Cursors
{
    [CreateAssetMenu(fileName = "asset_CursorCollection", menuName = "Cursor Data", order = 100)]
    public class CursorCollectionSO : ScriptableObject
    {
        [SerializeField] private CursorInfo[] cursors;
        
        public CursorInfo[] Cursors { get => cursors; }
    }

}
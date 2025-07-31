using System.Collections.Generic;
using RedRats.Core;
using UnityEngine;

namespace RedRats.UI.Core.Cursors
{
    /// <summary>
    /// Overseers the game's cursor.
    /// </summary>
    public class CursorOverseerMono : MonoSingleton<CursorOverseerMono>
    {
        [SerializeField] private CursorCollectionSO data;

        private IDictionary<CursorType, CursorInfo> cursors;
        private CursorType currentCursorType;

        protected override void Awake()
        {
            base.Awake();
            cursors = new Dictionary<CursorType, CursorInfo>();
            foreach (CursorInfo cursor in data.Cursors)
            {
                cursors.Add(cursor.type, cursor);
            }
        }

        private void Start() => Set(CursorType.Default);

        /// <summary>
        /// Sets the cursor to the given type.
        /// </summary>
        /// <param name="type">The type of cursor.</param>
        public void Set(CursorType type)
        {
            if (cursors == null || cursors.Count == 0) return;
            cursors[type].Use();
            currentCursorType = type;
        }

        /// <summary>
        /// Resets the cursor to default.
        /// </summary>
        public void SetDefault() => Set(CursorType.Default);
        
        public CursorType CurrentCursor { get => currentCursorType; }
    }
}
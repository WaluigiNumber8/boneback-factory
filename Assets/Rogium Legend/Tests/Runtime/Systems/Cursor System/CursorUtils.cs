using RedRats.UI.Core.Cursors;
using Rogium.UserInterface.Cursors;
using UnityEngine;

namespace Rogium.Tests.Systems.Cursors
{
    public static class CursorUtils
    {
        public static CursorChangerToolbox CreateChangerToolbox() => Object.Instantiate(new GameObject(), Vector3.zero, Quaternion.identity).AddComponent<CursorChangerToolbox>();
        public static CursorChangerUI CreateChangerUI() => Object.Instantiate(new GameObject(), Vector3.zero, Quaternion.identity).AddComponent<CursorChangerUI>();
        public static CursorChangerGameplay CreateChangerGameplay() => Object.Instantiate(new GameObject(), Vector3.zero, Quaternion.identity).AddComponent<CursorChangerGameplay>();
    }
}
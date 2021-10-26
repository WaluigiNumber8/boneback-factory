using Rogium.Gameplay.Core;
using UnityEngine;

namespace Rogium.Gameplay.DataLoading
{
    public class RRGDebugTestLoader : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameplayOverseer.Instance.LoadNextNormalRoom();
            }
        }
    }
}
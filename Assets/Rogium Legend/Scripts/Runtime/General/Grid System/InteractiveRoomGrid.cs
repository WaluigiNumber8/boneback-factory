using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using Rogium.Editors.RoomData;

namespace Rogium.Global.GridSystem
{
    [RequireComponent(typeof(Image))]
    public class InteractiveRoomGrid : MonoBehaviour
    {
        [SerializeField] private GameObject tile;

        private RoomAsset room;
        private Image gridCanvas;

        private void OnEnable()
        {
            RoomEditorOverseer.Instance.OnAssignRoom += (room) => this.room = room;
            
        }

        

    }
}
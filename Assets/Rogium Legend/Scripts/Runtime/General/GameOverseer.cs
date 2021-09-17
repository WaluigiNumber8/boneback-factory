using Rogium.Global.PlayerData;
using System.Collections;
using UnityEngine;

namespace Rogium.Global
{
    public class GameOverseer : MonoBehaviour
    {
        [SerializeField] private PlayerProfileAsset playerData;

        private void Awake()
        {
            PlayerProfile.SetPlayerData(playerData);
        }
    }
}
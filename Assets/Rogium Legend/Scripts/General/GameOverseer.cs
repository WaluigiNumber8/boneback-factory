using RogiumLegend.Global.PlayerData;
using System.Collections;
using UnityEngine;

namespace RogiumLegend.Global
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
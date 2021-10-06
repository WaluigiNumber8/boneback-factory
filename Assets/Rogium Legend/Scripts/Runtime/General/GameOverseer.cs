using Rogium.Global.PlayerData;
using Rogium.Global.UISystem.Interactables;
using UnityEngine;

namespace Rogium.Global
{
    public class GameOverseer : MonoBehaviour
    {
        [SerializeField] private PlayerProfileAsset playerData;

        private void Awake()
        {
            PlayerProfile.SetPlayerData(playerData);
            GASButtonActions.GameStart();
        }
    }
}
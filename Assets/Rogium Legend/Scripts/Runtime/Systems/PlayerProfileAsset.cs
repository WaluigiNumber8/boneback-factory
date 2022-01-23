using UnityEngine;

namespace Rogium.Systems.PlayerData
{
    [CreateAssetMenu(fileName = "New Player Profile", menuName = "Rogium Legend/Player Profile", order = 1000)]
    public class PlayerProfileAsset : ScriptableObject
    {
        public string username;
    }
}


namespace Rogium.Systems.PlayerData
{
    public class PlayerProfile
    {
        private static PlayerProfileAsset playerData;

        public static void SetPlayerData(PlayerProfileAsset data)
        {
            playerData = data;
        }

        public static string GetUsername => playerData.username;

    }
}

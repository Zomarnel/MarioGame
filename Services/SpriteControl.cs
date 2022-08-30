using Models;

namespace Services
{
    public static class SpriteControl
    {
        private static int SpriteUpdateTime { get; set; } = 100;

        private static bool IsUpdating = false;
        public static void UpdatePlayerSprite(Player player)
        {
            if (player.VerticalAction != Player.VerticalActions.IsStanding)
            {
                player.CurrentSpriteID = 3;

                return;
            }

            if (player.HorizontalAction == Player.HorizontalActions.IsStanding)
            {
                player.CurrentSpriteID = 0;
            }
            else if (player.HorizontalAction == Player.HorizontalActions.ChangeOfDirection)
            {
                player.CurrentSpriteID = 4;
            }
            else
            {
                if (!IsUpdating && player.HorizontalAction != Player.HorizontalActions.IsStanding)
                {
                    UpdatePlayerSpriteRunningAsync(player);

                    IsUpdating = true;
                }
            }
        }
        private static async void UpdatePlayerSpriteRunningAsync(Player player)
        {
            await Task.Delay(SpriteUpdateTime);

            if (player.CurrentSpriteID < 2)
            {
                player.CurrentSpriteID++;
            }
            else
            {
                player.CurrentSpriteID = 0;
            }

            IsUpdating = false;
        }
    }
}

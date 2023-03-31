using Models;


namespace Services
{
    public static class UpdateService
    {
        #region Player Sprite Update
        private static int SpriteUpdateTime { get; set; } = 100;

        private static bool IsPlayerUpdating = false;
        public static void UpdatePlayerSprite(Player player)
        {
            if (player.CurrentSpriteID > 0)
            {
                if (player.HorizontalSpeed >= 0)
                {
                    
                    if (player.VerticalAction != Player.VerticalActions.IsStanding)
                    {
                        player.CurrentSpriteID = 4;

                        return;
                    }

                    if (player.HorizontalAction == Player.HorizontalActions.IsStanding)
                    {
                        player.CurrentSpriteID = 1;
                    }
                    else if (player.HorizontalAction == Player.HorizontalActions.ChangeOfDirection)
                    {
                        player.CurrentSpriteID = 5;
                    }
                    else
                    {
                        if (!IsPlayerUpdating && player.HorizontalAction != Player.HorizontalActions.IsStanding)
                        {
                            IsPlayerUpdating = true;

                            UpdatePlayerSpriteRunningAsync(player);
                        }
                    }
                    
                }
                else
                {
                    player.CurrentSpriteID = -1;
                }
            }

            if (player.CurrentSpriteID < 0)
            {
                if (player.HorizontalSpeed <= 0)
                {
                    if (player.VerticalAction != Player.VerticalActions.IsStanding)
                    {
                        player.CurrentSpriteID = -4;

                        return;
                    }

                    if (player.HorizontalAction == Player.HorizontalActions.IsStanding)
                    {
                        player.CurrentSpriteID = -1;
                    }
                    else if (player.HorizontalAction == Player.HorizontalActions.ChangeOfDirection)
                    {
                        player.CurrentSpriteID = -5;
                    }
                    else
                    {
                        if (!IsPlayerUpdating && player.HorizontalAction != Player.HorizontalActions.IsStanding)
                        {
                            UpdatePlayerSpriteRunningAsync(player);

                            IsPlayerUpdating = true;
                        }
                    }
                }
                else
                {
                    player.CurrentSpriteID = 1;
                }
            }
        }
        private static async void UpdatePlayerSpriteRunningAsync(Player player)
        {
            await Task.Delay(SpriteUpdateTime);

            if (player.CurrentSpriteID > 0)
            {
                if (player.CurrentSpriteID < 3)
                {
                    player.CurrentSpriteID++;
                }
                else
                {
                    player.CurrentSpriteID = 1;
                }

            }
            else
            {
                if (player.CurrentSpriteID > -3)
                {
                    player.CurrentSpriteID--;
                }
                else
                {
                    player.CurrentSpriteID = -1;
                }
            }

            IsPlayerUpdating = false;
        }

        #endregion Player Sprite Update

        private static bool IsMobsUpdating = false;

        public static async void UpdateMobsSprite(List<Enemy> enemies)
        {
            await Task.Delay(1000);

            foreach (Enemy enemy in enemies)
            {
                if (enemy.FileName == "Mushroom")
                {
                    enemy.SpriteID += 1;

                    if (enemy.SpriteID > 51)
                    {
                        enemy.SpriteID = 50;
                    }
                }
            }
        } 
    }
}

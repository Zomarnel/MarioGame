using Models;

namespace Services
{
    public static class PlayerMovement
    {
        public static void MovePlayer(Player player)
        {
            player.XCoordinate += player.HorizontalSpeed;
            player.YCoordinate += player.VerticalSpeed;

            ChangeMomentum(player);
        }
        private static void ChangeMomentum(Player player)
        {
            // When speed reaches 0
            if (player.HorizontalSpeed == 0 && player.HorizontalDirection != "Idle")
            {
                switch (player.HorizontalDirection)
                {
                    case "Left":
                        player.HorizontalSpeed = -1;
                        break;
                    case "Right":
                        player.HorizontalSpeed = 1;
                        break;
                }

                // Start building momentum
                player.IsBuildingMomentum = true;

                return;
            }

            if (player.IsBuildingMomentum)
            {
                if (player.HorizontalSpeed < 0 && player.HorizontalSpeed > -3)
                {
                    player.HorizontalSpeed -= 0.1;
                }
                else if (player.HorizontalSpeed > 0 && player.HorizontalSpeed < 3)
                {
                    player.HorizontalSpeed += 0.1;
                }

                return;
            }

            // Lower momentum
            if (!player.IsBuildingMomentum)
            {
                if (player.HorizontalSpeed < 0)
                {
                    player.HorizontalSpeed += 0.1;
                }
                else if (player.HorizontalSpeed > 0)
                {
                    player.HorizontalSpeed -= 0.1;
                }

                return;
            }
        }
    }
}

using Core;
using Models;

namespace Services
{
    public static class Collisions
    {
        #region COLLISIONSCHECK
        public static void HorizontalBoundariesCheck(Player player, List<Block> blocks)
        {
            int xCoordinate = (int)(player.XCoordinate + Math.Round(Math.Abs(MapService.MapXCoordinate), 1));
            int yCoordinate = (int)player.YCoordinate;

            // Check borders
            if (player.XCoordinate < 0)
            {
                player.XCoordinate = 0;

                Movement.StopMovingHorizontally(player);

                return;
            }
            else if (player.XCoordinate > GameInfo.SCREEN_WIDTH - GameInfo.SPRITE_WIDTH)
            {
                player.XCoordinate = GameInfo.SCREEN_WIDTH - GameInfo.SPRITE_WIDTH;

                Movement.StopMovingHorizontally(player);

                return;
            }

            foreach(Block block in blocks)
            {
                if (IsPlayerInsideBlock(block, xCoordinate, yCoordinate))
                {
                    if (player.HorizontalSpeed > 0)
                    {
                        player.XCoordinate = block.XCoordinate - Math.Abs(MapService.MapXCoordinate) - GameInfo.SPRITE_WIDTH;

                        Movement.StopMovingHorizontally(player);

                        break;
                    }
                    else if (player.HorizontalSpeed < 0)
                    {
                        player.XCoordinate = block.XCoordinate + block.Width - Math.Abs(MapService.MapXCoordinate);

                        Movement.StopMovingHorizontally(player);

                        break;
                    }
                }
            }
        }
        public static void VerticalBoundariesCheck(Player player, List<Block> blocks)
        {
            int xCoordinate = (int)(player.XCoordinate + Math.Round(Math.Abs(MapService.MapXCoordinate), 1));
            int yCoordinate = (int)(player.YCoordinate);

            foreach (Block block in blocks)
            {
                if (IsPlayerInsideBlock(block, xCoordinate, yCoordinate))
                {
                    if (player.VerticalSpeed > 0)
                    {
                        player.YCoordinate = block.YCoordinate - 32;

                        block.PlayerHasBumped = true;

                        Movement.StopMovingVertically(player, true);

                        break;
                    }
                    else if (player.VerticalSpeed < 0)
                    {
                        player.YCoordinate = block.YCoordinate + block.Height;

                        Movement.StopMovingVertically(player);

                        break;
                    }
                }
            }

            if (!blocks.Any(b => IsPlayerInsideBlock(b, xCoordinate, yCoordinate - 1)) && player.VerticalAction == Player.VerticalActions.IsStanding)
            {
                Movement.StopMovingVertically(player, true);
            }
        }

        #endregion COLLISIONSCHECK
        public static bool CanPlayerMoveHorizontally(Player player, List<Block> blocks)
        {
            int xCoordinate = (int)(player.XCoordinate + Math.Round(Math.Abs(MapService.MapXCoordinate), 1));
            int yCoordinate = (int)player.YCoordinate;

            if ((blocks.Any(b => IsPlayerInsideBlock(b, xCoordinate + 1, yCoordinate)) && player.HorizontalSpeed > 0) ||
                (blocks.Any(b => IsPlayerInsideBlock(b, xCoordinate - 1, yCoordinate)) && player.HorizontalSpeed < 0))
            {
                Movement.StopMovingHorizontally(player);

                return false;
            }

            return true;
        }
        private static bool IsPlayerInsideBlock(Block block, int xCoordinate, int yCoordinate)
        {
            // Checks if any of the player's rectangle's points are in the block

            if (xCoordinate == block.XCoordinate && xCoordinate + 32 == block.XCoordinate + block.Width && yCoordinate > block.YCoordinate && yCoordinate < block.YCoordinate + block.Height)
            {
                return true;
            }

            if (xCoordinate > block.XCoordinate && xCoordinate < block.XCoordinate + block.Width && yCoordinate > block.YCoordinate && yCoordinate < block.YCoordinate + block.Height)
            {
                return true;
            }

            if (xCoordinate > block.XCoordinate && xCoordinate < block.XCoordinate + block.Width && yCoordinate + 32 > block.YCoordinate && yCoordinate + 32 < block.YCoordinate + block.Height)
            {
                return true;
            }

            if (xCoordinate + 32 > block.XCoordinate && xCoordinate + 32 < block.XCoordinate + block.Width && yCoordinate > block.YCoordinate && yCoordinate < block.YCoordinate + block.Height)
            {
                return true;
            }

            if (xCoordinate + 32 > block.XCoordinate && xCoordinate + 32 < block.XCoordinate + block.Width && yCoordinate + 32 > block.YCoordinate && yCoordinate + 32 < block.YCoordinate + block.Height)
            {
                return true;
            }

            return false;
        }
    }
}

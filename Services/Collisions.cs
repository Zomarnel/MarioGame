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

            Rectangle playerRect = new Rectangle(player.Width, player.Height, xCoordinate, yCoordinate);

            List<int> rectSizes = new List<int>();

            foreach (Block block in blocks)
            {

                Rectangle blockRect = Rectangle.ConvertEntityToRectangle(block);

                Rectangle? intersectRect = Rectangle.Intersect(playerRect, blockRect);

                if (intersectRect is null)
                {
                    rectSizes.Add(0);
                    continue;
                }

                rectSizes.Add(intersectRect.Width + intersectRect.Height);
            }

            int maxSize = rectSizes.Max();

            if (maxSize == 0)
            {
                return;
            }

            Block intersectBlock = blocks[rectSizes.IndexOf(maxSize)];

            if (player.HorizontalSpeed > 0)
            {
                player.XCoordinate = intersectBlock.XCoordinate - Math.Abs(MapService.MapXCoordinate) - GameInfo.SPRITE_WIDTH;

                Movement.StopMovingHorizontally(player);

            }
            else if (player.HorizontalSpeed < 0)
            {
                player.XCoordinate = intersectBlock.XCoordinate + intersectBlock.Width - Math.Abs(MapService.MapXCoordinate);

                Movement.StopMovingHorizontally(player);
            }

            /*foreach(Block block in blocks)
            {
                if (IsPlayerInsideBlock(block, xCoordinate, yCoordinate))
                {
                    if (player.HorizontalSpeed > 0)
            {
                player.XCoordinate = intersectBlock.XCoordinate - Math.Abs(MapService.MapXCoordinate) - GameInfo.SPRITE_WIDTH;

                Movement.StopMovingHorizontally(player);

            }
            else if (player.HorizontalSpeed < 0)
            {
                player.XCoordinate = intersectBlock.XCoordinate + intersectBlock.Width - Math.Abs(MapService.MapXCoordinate);

                Movement.StopMovingHorizontally(player);
            }
                }
            }*/
        }
        public static void VerticalBoundariesCheck(Player player, List<Block> blocks)
        {
            int xCoordinate = (int)(player.XCoordinate + Math.Round(Math.Abs(MapService.MapXCoordinate), 1));
            int yCoordinate = (int)(player.YCoordinate);

            Rectangle playerRect = new Rectangle(player.Width, player.Height, xCoordinate, yCoordinate);

            List<int> rectSizes = new List<int>();

            foreach (Block block in blocks)
            {

                Rectangle blockRect = Rectangle.ConvertEntityToRectangle(block);

                Rectangle? intersectRect = Rectangle.Intersect(playerRect, blockRect);

                if (intersectRect is null)
                {
                    rectSizes.Add(0);
                    continue;
                }

                rectSizes.Add(intersectRect.Width + intersectRect.Height);
            }

            int maxSize = rectSizes.Max();

            if (!blocks.Any(b => IsPlayerInsideBlock(b, xCoordinate, yCoordinate - 1)) && player.VerticalAction == Player.VerticalActions.IsStanding)
            {
                Movement.StopMovingVertically(player, true);
            }

            if (maxSize == 0)
            {
                return;
            }

            Block intersectBlock = blocks[rectSizes.IndexOf(maxSize)];

            if (player.VerticalSpeed > 0)
            {
                player.YCoordinate = intersectBlock.YCoordinate - 32;

                intersectBlock.PlayerHasBumped = true;

                Movement.StopMovingVertically(player, true);
            }
            else if (player.VerticalSpeed < 0)
            {
                player.YCoordinate = intersectBlock.YCoordinate + intersectBlock.Height;

                Movement.StopMovingVertically(player);
            }

            /*
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
            */
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

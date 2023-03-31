﻿using Core;
using Models;

namespace Services
{
    public static class Collisions
    {
        #region COLLISIONSCHECK
        public static void HorizontalPlayerBoundariesCheck(Player player, List<Block> blocks)
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

            List<int> rectSizes = RectangleIntersection(playerRect, blocks);

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
        }
        public static void VerticalPlayerBoundariesCheck(Player player, List<Block> blocks)
        {
            int xCoordinate = (int)(player.XCoordinate + Math.Round(Math.Abs(MapService.MapXCoordinate), 1));
            int yCoordinate = (int)(player.YCoordinate);

            Rectangle playerRect = new Rectangle(player.Width, player.Height, xCoordinate, yCoordinate);

            List<int> rectSizes = RectangleIntersection(playerRect, blocks);

            int maxSize = rectSizes.Max();

            if (!blocks.Any(b => IsEntityInsideBlock(b, xCoordinate, yCoordinate - 1)) && player.VerticalAction == Player.VerticalActions.IsStanding)
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

                intersectBlock.PlayerHasInteracted = true;

                Movement.StopMovingVertically(player, true);
            }
            else if (player.VerticalSpeed < 0)
            {
                player.YCoordinate = intersectBlock.YCoordinate + intersectBlock.Height;

                Movement.StopMovingVertically(player);
            }

        }
        public static bool CanPlayerMoveHorizontally(Player player, List<Block> blocks)
        {
            int xCoordinate = (int)(player.XCoordinate + Math.Round(Math.Abs(MapService.MapXCoordinate), 1));
            int yCoordinate = (int)player.YCoordinate;

            if ((blocks.Any(b => IsEntityInsideBlock(b, xCoordinate + 1, yCoordinate)) && player.HorizontalSpeed > 0) ||
                (blocks.Any(b => IsEntityInsideBlock(b, xCoordinate - 1, yCoordinate)) && player.HorizontalSpeed < 0))
            {
                Movement.StopMovingHorizontally(player);

                return false;
            }

            return true;
        }

        public static void HorizontalEnemyBoundariesCheck(Enemy enemy, List<Block> blocks)
        {
            int xCoordinate = (int)enemy.XCoordinate;
            int yCoordinate = (int)enemy.YCoordinate;

            // Check borders
            if (xCoordinate < 0)
            {
                enemy.XCoordinate = 0;

                enemy.HorizontalSpeed = -enemy.HorizontalSpeed;

                return;
            }
            else if (xCoordinate > GameInfo.SCREEN_WIDTH - GameInfo.SPRITE_WIDTH)
            {
                enemy.XCoordinate = GameInfo.SCREEN_WIDTH - GameInfo.SPRITE_WIDTH;

                enemy.HorizontalSpeed = -enemy.HorizontalSpeed;

                return;
            }

            Rectangle enemyRect = Rectangle.ConvertEntityToRectangle(enemy);

            List<int> rectSizes = RectangleIntersection(enemyRect, blocks);

            int maxSize = rectSizes.Max();

            if (maxSize == 0)
            {
                return;
            }

            Block intersectBlock = blocks[rectSizes.IndexOf(maxSize)];

            if (enemy.HorizontalSpeed > 0)
            {
                enemy.XCoordinate = intersectBlock.XCoordinate - GameInfo.SPRITE_WIDTH;

                enemy.HorizontalSpeed = -enemy.HorizontalSpeed;

            }
            else if (enemy.HorizontalSpeed < 0)
            {
                enemy.XCoordinate = intersectBlock.XCoordinate + intersectBlock.Width;

                enemy.HorizontalSpeed = -enemy.HorizontalSpeed;
            }
        }
        public static void VerticalEnemyBoundariesCheck(Enemy enemy, List<Block> blocks)
        {
            int xCoordinate = (int)enemy.XCoordinate;
            int yCoordinate = (int)enemy.YCoordinate;

            Rectangle enemyRect = Rectangle.ConvertEntityToRectangle(enemy);

            List<int> rectSizes = RectangleIntersection(enemyRect, blocks);

            int maxSize = rectSizes.Max();

            if (!blocks.Any(b => IsEntityInsideBlock(b, xCoordinate, yCoordinate - 1)) && enemy.VerticalSpeed == 0)
            {
                enemy.InitialY = enemy.YCoordinate;

                enemy.VerticalSpeed = -1;
            }

            if (maxSize == 0)
            {
                return;
            }

            Block intersectBlock = blocks[rectSizes.IndexOf(maxSize)];

            if (enemy.VerticalSpeed < 0)
            {
                enemy.YCoordinate = intersectBlock.YCoordinate + intersectBlock.Height;

                enemy.VerticalSpeed = 0;
            }
        }

        #endregion COLLISIONSCHECK

        private static bool IsEntityInsideBlock(Block block, int xCoordinate, int yCoordinate)
        {
            // Checks if any of the entity's rectangle's points are in the block

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
        private static List<int> RectangleIntersection(Rectangle entityRect, List<Block> blocks)
        {
            List<int> rectSizes = new List<int>();

            foreach (Block block in blocks)
            {

                Rectangle blockRect = Rectangle.ConvertEntityToRectangle(block);

                Rectangle? intersectRect = Rectangle.Intersect(entityRect, blockRect);

                if (intersectRect is null)
                {
                    rectSizes.Add(0);
                    continue;
                }

                rectSizes.Add(intersectRect.Width + intersectRect.Height);
            }

            return rectSizes;
        }
    }
}

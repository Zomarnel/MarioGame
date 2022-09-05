using Core;
using Models;
using System.Collections.Generic;
using static System.Reflection.Metadata.BlobBuilder;

namespace Services
{
    public static class Collisions
    {
        /*private static List<Boundary> _boundaries = new List<Boundary>();
        static Collisions()
        {
            // Floors
            AddNewBoundary(0, 0, 2208, 64);
            AddNewBoundary(2272, 0, 2752, 64);
            AddNewBoundary(2848, 0, 4896, 64);
            AddNewBoundary(4960, 0, 6752, 64);

            // Blocks
            AddNewBoundary(512, 160, 544, 192);
            AddNewBoundary(640, 160, 800, 192);
            AddNewBoundary(702, 288, 734, 320);
            AddNewBoundary(2464, 160, 2560, 192);
            AddNewBoundary(2560, 288, 2816, 320);
            AddNewBoundary(2912, 288, 3040, 320);
            AddNewBoundary(3008, 160, 3040, 192);
            AddNewBoundary(3200, 160, 3264, 192);
            AddNewBoundary(3392, 160, 3424, 192);
            AddNewBoundary(3488, 160, 3520, 192);
            AddNewBoundary(3488, 288, 3520, 320);
            AddNewBoundary(3584, 160, 3616, 192);
            AddNewBoundary(3776, 160, 3808, 192);
            AddNewBoundary(3872, 288, 3968, 320);
            AddNewBoundary(4096, 288, 4224, 320);
            AddNewBoundary(4128, 160, 4192, 192);
            AddNewBoundary(5376, 160, 5504, 192);
            AddNewBoundary(6336, 0, 6368, 96);

            Boundary bnd = _boundaries[20];

            List<int> matrix = new List<int>()
            {
                0,
                0,
                1,
                0
            };

            List<string> textToAdd = new List<string>();

            string fileName = "";

            int limit = (int)(bnd.XEnd - bnd.XStart) / 32;

            for (int i = 0; i < limit; i++)
            {
                switch (matrix[i])
                {
                    case 0:
                        fileName = "Brick";
                        break;
                    case 1:
                        fileName = "LuckyBlock";
                        break;
                }

                textToAdd.Add($"AddNewBlock(\"{fileName}\", 0, {bnd.XStart + 32 * i}, {bnd.YStart}, 32, 32);");
            }

            if (File.Exists("Block.txt"))
            {
                File.Delete("Block.txt");
            }

            File.WriteAllLines("Block.txt", textToAdd);

            // Stairs
            AddNewBoundary(4288, 0, 4416, 96);
            AddNewBoundary(4320, 64, 4416, 128);
            AddNewBoundary(4352, 96, 4416, 160);
            AddNewBoundary(4384, 128, 4416, 192);

            AddNewBoundary(4480, 0, 4608, 96);
            AddNewBoundary(4480, 64, 4576, 128);
            AddNewBoundary(4480, 96, 4544, 160);
            AddNewBoundary(4480, 128, 4512, 192);

            AddNewBoundary(4736, 0, 4896, 96);
            AddNewBoundary(4768, 64, 4896, 128);
            AddNewBoundary(4800, 96, 4896, 160);
            AddNewBoundary(4832, 128, 4896, 192);

            AddNewBoundary(4960, 0, 5088, 96);
            AddNewBoundary(4960, 64, 5056, 128);
            AddNewBoundary(4960, 96, 5024, 160);
            AddNewBoundary(4960, 128, 4992, 192);

            AddNewBoundary(5792, 0, 6080, 96);
            AddNewBoundary(5792 + 32, 64, 6080, 96 + 32);
            AddNewBoundary(5792 + 32*2, 64 + 32, 6080, 96 + 32*2);
            AddNewBoundary(5792 + 32*3, 64 + 32*2, 6080, 96 + 32*3);
            AddNewBoundary(5792 + 32*4, 64 + 32*3, 6080, 96 + 32*4);
            AddNewBoundary(5792 + 32*5, 64 + 32*4, 6080, 96 + 32*5);
            AddNewBoundary(5792 + 32*6, 64 + 32*5, 6080, 96 + 32*6);
            AddNewBoundary(5792 + 32*7, 64 + 32*6, 6080, 96 + 32*7);

            //Tunnels

            AddNewBoundary(900, 64, 955, 129);
            AddNewBoundary(1220, 64, 1275, 159);
            AddNewBoundary(1477, 64, 1532, 189);
            AddNewBoundary(1828, 64, 1883, 189);
            AddNewBoundary(5222, 64, 5277, 129);
            AddNewBoundary(5732, 64, 5787, 129);

        }*/

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
                    }
                    else if (player.HorizontalSpeed < 0)
                    {
                        player.XCoordinate = block.XCoordinate + block.Width - Math.Abs(MapService.MapXCoordinate);

                        Movement.StopMovingHorizontally(player);
                    }
                }
            }

            /*foreach (Boundary bnd in _boundaries)
            {
                if (IsPlayerInsideBoundary(bnd, xCoordinate, yCoordinate))
                {
                    if (player.HorizontalSpeed > 0)
                    {
                        player.XCoordinate = bnd.XStart - Math.Abs(MapService.MapXCoordinate) - GameInfo.SPRITE_WIDTH;

                        Movement.StopMovingHorizontally(player);
                    }
                    else if (player.HorizontalSpeed < 0)
                    {
                        player.XCoordinate = bnd.XEnd - Math.Abs(MapService.MapXCoordinate);

                        Movement.StopMovingHorizontally(player);
                    }
                }
            }*/
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

                        Movement.StopMovingVertically(player, true);
                    }
                    else if (player.VerticalSpeed < 0)
                    {
                        player.YCoordinate = block.YCoordinate + block.Height;

                        Movement.StopMovingVertically(player);
                    }
                }

                if (!blocks.Any(b => IsPlayerInsideBlock(b, xCoordinate, yCoordinate - 1)) && player.VerticalAction == Player.VerticalActions.IsStanding)
                {
                    Movement.StopMovingVertically(player, true);
                }
            }

            /*foreach (Boundary bnd in _boundaries)
            {
                if (IsPlayerInsideBoundary(bnd, xCoordinate, yCoordinate))
                {
                    if (player.VerticalSpeed > 0)
                    {
                        player.YCoordinate = bnd.YStart - 32;

                        Movement.StopMovingVertically(player, true);
                    }
                    else if (player.VerticalSpeed < 0)
                    {
                        player.YCoordinate = bnd.YEnd;

                        Movement.StopMovingVertically(player);
                    }
                }

                if (!_boundaries.Any(b => IsPlayerInsideBoundary(b, xCoordinate, yCoordinate - 1)) && player.VerticalAction == Player.VerticalActions.IsStanding)
                {
                    Movement.StopMovingVertically(player, true);
                }
            }*/
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

        /*private static bool IsPlayerInsideBoundary(Boundary boundary, int xCoordinate, int yCoordinate)
        {

            if (xCoordinate == boundary.XStart && xCoordinate + 32 == boundary.XEnd && yCoordinate > boundary.YStart && yCoordinate < boundary.YEnd) 
            {
                return true;
            }

            if (xCoordinate > boundary.XStart && xCoordinate < boundary.XEnd && yCoordinate > boundary.YStart && yCoordinate < boundary.YEnd)
            {
                return true;
            }

            if (xCoordinate > boundary.XStart && xCoordinate < boundary.XEnd && yCoordinate + 32 > boundary.YStart && yCoordinate + 32 < boundary.YEnd)
            {
                return true;
            }

            if (xCoordinate + 32 > boundary.XStart && xCoordinate + 32 < boundary.XEnd && yCoordinate > boundary.YStart && yCoordinate < boundary.YEnd)
            {
                return true;
            }

            if (xCoordinate + 32 > boundary.XStart && xCoordinate + 32 < boundary.XEnd && yCoordinate + 32 > boundary.YStart && yCoordinate + 32 < boundary.YEnd)
            {
                return true;
            }

            return false;
        }
        private static void AddNewBoundary(double xStart, double yStart, double xEnd, double yEnd)
        {
            _boundaries.Add(new Boundary(xStart, yStart, xEnd, yEnd));
        }*/

    }
}

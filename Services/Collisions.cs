using Core;
using Models;

namespace Services
{
    public static class Collisions
    {
        private static List<Boundary> _boundaries = new List<Boundary>();
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
        }
        public static void HorizontalBoundariesCheck(Player player)
        {
            int xCoordinate = (int)(player.XCoordinate + Math.Round(Math.Abs(MapService.MapXCoordinate), 1));
            int yCoordinate = (int)player.YCoordinate;

            // Check borders
            if (player.XCoordinate < 0)
            {
                player.XCoordinate = 0;

                player.StopMovingHorizontally();

                return;
            }
            else if (player.XCoordinate > GameInfo.SCREEN_WIDTH - GameInfo.SPRITE_WIDTH)
            {
                player.XCoordinate = GameInfo.SCREEN_WIDTH - GameInfo.SPRITE_WIDTH;

                player.StopMovingHorizontally();

                return;
            }

            foreach (Boundary bnd in _boundaries)
            {
                if (player.HorizontalSpeed > 0)
                {
                    if (bnd.IsPointInsideBoundary(xCoordinate + player.Width, yCoordinate) || bnd.IsPointInsideBoundary(xCoordinate + player.Width, yCoordinate + player.Height))
                    {
                        player.XCoordinate = bnd.XStart - Math.Abs(MapService.MapXCoordinate) - GameInfo.SPRITE_WIDTH;

                        player.StopMovingHorizontally();
                    }
                }
                else if (player.HorizontalSpeed < 0)
                {
                    if (bnd.IsPointInsideBoundary(xCoordinate, yCoordinate) || bnd.IsPointInsideBoundary(xCoordinate, yCoordinate + player.Height))
                    {
                        player.XCoordinate = bnd.XEnd - Math.Abs(MapService.MapXCoordinate);

                        player.StopMovingHorizontally();
                    }
                }
            }
        }
        public static void VerticalBoundariesCheck(Player player)
        {

            int xCoordinate = (int)(player.XCoordinate + Math.Round(Math.Abs(MapService.MapXCoordinate), 1));
            int yCoordinate = (int)(player.YCoordinate);

            int count = 0;

            foreach (Boundary bnd in _boundaries)
            {
                if (player.VerticalSpeed > 0)
                {
                    if (bnd.IsPointInsideBoundary(xCoordinate, yCoordinate + player.Height) || bnd.IsPointInsideBoundary(xCoordinate + player.Width, yCoordinate + player.Height))
                    {
                          player.YCoordinate = bnd.YStart - 32;

                        player.StopMovingVertically(true);
                    }
                }
                else if (player.VerticalSpeed < 0)
                {
                    if (bnd.IsPointInsideBoundary(xCoordinate, yCoordinate) || bnd.IsPointInsideBoundary(xCoordinate + player.Width, yCoordinate))
                    {
                        player.YCoordinate = bnd.YEnd;

                        player.StopMovingVertically();
                    }
                }
                else
                {
                    if (!bnd.IsPointInsideBoundary(xCoordinate, yCoordinate - 1) && !bnd.IsPointInsideBoundary(xCoordinate + player.Width, yCoordinate - 1)) 
                    {
                        count++;
                    }
                }
            }

            if (count == _boundaries.Count() && player.VerticalAction == Player.VerticalActions.IsStanding)
            {
                player.StopMovingVertically(true);
            }

        }
        private static void AddNewBoundary(double xStart, double yStart, double xEnd, double yEnd)
        {
            _boundaries.Add(new Boundary(xStart, yStart, xEnd, yEnd));
        }
    }
}

using Core;

namespace Services
{
    public static class MapService
    {
        public static double MapXCoordinate { get; set; }
        public static double MapWidth { get; set; }

        public static bool HasMapReachedEnd = false;

        public static EventHandler<double> DrawMap;
        public static void MoveMap(double speed)
        {
            MapXCoordinate -= speed;

            if (Math.Abs(MapXCoordinate) + GameInfo.SCREEN_WIDTH >= MapWidth)
            {
                MapXCoordinate = -MapWidth + GameInfo.SCREEN_WIDTH;

                HasMapReachedEnd = true;
            }

            DrawMap.Invoke(new object(), MapXCoordinate);
        }
    }
}


namespace Models
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Location PointLocation { get; set; }

        public enum Location
        {
            BottomLeft = 0,
            BottomRight = 1,
            TopRight = 2,
            TopLeft = 3
        };

        public Point(double x, double y, Location location)
        {
            X = x;
            Y = y;

            PointLocation = location;
        }
    }
}

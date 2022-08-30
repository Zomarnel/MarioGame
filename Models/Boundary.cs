namespace Models
{
    public class Boundary
    {
        public double XStart { get; init; }
        public double YStart { get; init; }
        public double XEnd { get; init; }
        public double YEnd { get; init; }
        public Boundary(double xStart, double yStart, double xEnd, double yEnd)
        {
            XStart = xStart;
            YStart = yStart;
            XEnd = xEnd;
            YEnd = yEnd;
        }
    }
}

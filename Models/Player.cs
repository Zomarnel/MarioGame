
namespace Models
{
    public class Player
    {
        public double XCoordinate { get; set; }
        public double YCoordinate { get; set; }

        public Player(int xCoordinate, int yCoordinate)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
        }
    }
}
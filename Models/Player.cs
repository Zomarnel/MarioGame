
namespace Models
{
    public class Player
    {
        public double XCoordinate { get; set; }
        public double YCoordinate { get; set; }

        public double HorizontalSpeed
        {
            get => Math.Round(_horizontalSpeed, 1);
            set
            {
                _horizontalSpeed = value;
            }
        }
        public double VerticalSpeed { get; set; }

        public string HorizontalDirection { get; set; }

        public bool IsBuildingMomentum { get; set; }


        private double _horizontalSpeed;

        public Player(int xCoordinate, int yCoordinate)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;

            HorizontalDirection = "Idle";

            IsBuildingMomentum = false;

            HorizontalSpeed = 0;
            VerticalSpeed = 0;
        }
    }
}
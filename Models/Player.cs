
namespace Models
{
    public class Player
    {
        public double XCoordinate
        {
            get => Math.Round(_xCoordinate, 1);
            set
            {
                _xCoordinate = value;
            }
        }
        public double YCoordinate
        {
            get => Math.Round(_yCoordinate, 1);
            set
            {
                _yCoordinate = value;
            }
        }

        public double HorizontalSpeed
        {
            get => Math.Round(_horizontalSpeed, 1);
            set
            {
                _horizontalSpeed = value;
            }
        }
        public double VerticalSpeed
        {
            get => Math.Round(_verticalSpeed, 1);
            set
            {
                _verticalSpeed = value;
            }
        }

        private double _horizontalSpeed;
        private double _verticalSpeed;

        private double _xCoordinate;
        private double _yCoordinate;

        public enum HorizontalActions
        {
            IsStanding,
            IsSpeeding,
            IsSlowing,
            ChangeOfDirection
        };
        public enum VerticalActions
        {
            IsStanding,
            IsJumping,
            IsFalling
        };

        public HorizontalActions HorizontalAction { get; set; }
        public VerticalActions VerticalAction { get; set; }

        public Player(int xCoordinate, int yCoordinate)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;

            HorizontalAction = HorizontalActions.IsStanding;
            VerticalAction = VerticalActions.IsStanding;

            HorizontalSpeed = 0;
            VerticalSpeed = 0;
        }
        public void StopMovingHorizontally()
        {
            HorizontalAction = Player.HorizontalActions.IsStanding;
            HorizontalSpeed = 0;
        }
    }
}
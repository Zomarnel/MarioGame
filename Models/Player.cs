
namespace Models
{
    public class Player
    {
        public double XCoordinate
        {
            get => Math.Round(_xCoordinate, 2);
            set
            {
                _xCoordinate = value;
            }
        }
        public double YCoordinate
        {
            get => Math.Round(_yCoordinate, 2);
            set
            {
                _yCoordinate = value;
            }
        }

        public double HorizontalSpeed
        {
            get => Math.Round(_horizontalSpeed, 2);
            set
            {
                _horizontalSpeed = value;
            }
        }
        public double VerticalSpeed
        {
            get => Math.Round(_verticalSpeed, 2);
            set
            {
                _verticalSpeed = value;
            }
        }

        public double JumpLimit { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public int CurrentSpriteID { get; set; }

        public bool? HasChangedSprite = null;

        private double _horizontalSpeed;
        private double _verticalSpeed;

        private double _xCoordinate;
        private double _yCoordinate;

        public enum HorizontalActions
        {
            IsStanding,
            IsSpeeding,
            IsSlowing,
            ChangeOfDirection,
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

            CurrentSpriteID = 1;

            Width = 32;
            Height = 32;
        }
    }
}
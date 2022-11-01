
namespace Models
{
    public class Player : Entity
    {
        public double JumpLimit { get; set; }

        public int CurrentSpriteID { get; set; }

        public bool? HasChangedSprite = null;

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

        public Player(double xCoordinate, double yCoordinate, double horizontalSpeed, double verticalSpeed, int width, int height)
                    : base(xCoordinate, yCoordinate, horizontalSpeed, verticalSpeed, width, height)
        {
            XCoordinate = 512;
            YCoordinate = yCoordinate;

            HorizontalSpeed = horizontalSpeed;
            VerticalSpeed = verticalSpeed;

            Width = width;
            Height = height;

            HorizontalAction = HorizontalActions.IsStanding;
            VerticalAction = VerticalActions.IsStanding;

            CurrentSpriteID = 1;

            Width = 32;
            Height = 32;
        }
    }
}

namespace Models
{
    public class Player : BaseEntity
    {
        public double JumpLimit { get; set; }
        public bool CanJumpCooldown { get; set; } = true;

        public bool HasKilledEnemyCooldown { get; set; } = false;

        public int CurrentSpriteID { get; set; }

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
            XCoordinate = xCoordinate;
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
        public async void JumpCooldown()
        {
            await Task.Delay(250);

            CanJumpCooldown = true;
        }
    }
}
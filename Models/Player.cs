using Core;

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
            VerticalSpeed = -GameInfo.GAME_GRAVITY;

            CurrentSpriteID = 0;
        }
        public void ChangeSprite()
        {
            if (VerticalAction != VerticalActions.IsStanding)
            {
                CurrentSpriteID = 3;
            }
            else if (HorizontalAction == HorizontalActions.ChangeOfDirection)
            {
                CurrentSpriteID = 4;
            }
            else if (HorizontalAction != HorizontalActions.IsStanding && CurrentSpriteID < 2)
            {
                CurrentSpriteID++;
            }
            else
            {
                CurrentSpriteID = 0;
            }

            HasChangedSprite = true;
        }
        public void StopMovingHorizontally()
        {
            HorizontalAction = HorizontalActions.IsStanding;
            HorizontalSpeed = 0;
            CurrentSpriteID = 0;
        }
        public void StopMovingVertically()
        {
            VerticalAction = VerticalActions.IsFalling;
            VerticalSpeed = -GameInfo.GAME_GRAVITY;
        }
    }
}
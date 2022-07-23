using Models;

namespace Services
{
    public class Movement
    {
        private bool HasChangedDirection { get; set; }
        private string Direction
        {
            get => _direction;

            set
            {
                if (_direction != null)
                {
                    HasChangedDirection = true;
                }else
                {
                    HasChangedDirection = false;
                }

                if (_direction != value)
                {
                    _direction = value;
                }
            }
        }
        private double SpeedX
        {
            get => Math.Round(_speedX);

            set
            {
                _speedX = value;
            }
        }
        private double SpeedY
        {
            get => Math.Round(_speedY);

            set
            {
                _speedX = value;
            }
        }
        
        private double _speedX;

        private double _speedY;

        private string? _direction;
        public Movement()
        {
            SpeedX = 0;
            SpeedY = 0;

            Direction = null;
        }
        public void MovePlayer(Player player)
        {
            player.XCoordinate += _speedX;
            player.YCoordinate += _speedY;

            ChangeMomentum();
        }
        public void SetDirection(string direction)
        {
            Direction = direction;
        }
        public string ReturnDirection()
        {
            return Direction;
        }
        private void ChangeMomentum()
        {
            if (SpeedX == 0 && !string.IsNullOrEmpty(Direction))
            {
                switch (Direction)
                {
                    case "Left":
                        SpeedX = -1;
                        break;
                    case "Right":
                        SpeedX = 1;
                        break;
                }

                return;
            }

            if (!HasChangedDirection)
            {
                if (SpeedX < 0 && SpeedX >= -3)
                {
                    SpeedX -= 0.1;
                }
                else if (SpeedX > 0 && SpeedX <= 3)
                {
                    SpeedX += 0.1;
                }

                return;
            }

            if (HasChangedDirection)
            {
                if (SpeedX < 0)
                {
                    SpeedX += 0.1;
                }
                else if (SpeedX > 0)
                {
                    SpeedX -= 0.1;
                }

                return;
            }
        }
    }
}

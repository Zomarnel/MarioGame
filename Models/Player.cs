using System;

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
        public double SpeedX
        {
            get => Math.Round(_speedX, 1);
            set
            {
                _speedX = value;
            }
        }
        public double SpeedY
        {
            get => Math.Round(_speedY, 1);
            set
            {
                _speedY = value;
            }
        }
        private string Direction { get; set; }
        private bool HasChangedDirection { get; set; } = false;

        private double _xCoordinate { get; set; }
        private double _yCoordinate { get; set; }
        private double _speedX { get; set; }
        private double _speedY { get; set; }
        public Player(int xCoordinate, int yCoordinate)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;

            SpeedX = 0;
            SpeedY = 0;
        }

        public void Move()
        {
            XCoordinate += SpeedX;
            YCoordinate += SpeedY;

            ChangeSpeeds();
        }
        public void ChangeDirection(string direction)
        {
            if (Direction == direction)
            {
                return;
            }

            Direction = direction;

            HasChangedDirection = true;
        }
        private void ChangeSpeeds()
        {
            // When no momentum
            if (SpeedX == 0)
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

                HasChangedDirection = false;
            }

            // Build up momentum
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
            }

            // Lower momentum
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
            }
        }
    }
}
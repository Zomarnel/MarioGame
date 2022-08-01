using Models;

namespace Services
{
    public class PlayerMovement
    {
        private bool BuildMomentum { get; set; }
        private string Direction { get; set; }
        private double SpeedX
        {
            get => Math.Round(_speedX, 1);

            set
            {
                _speedX = value;
            }
        }
        private double SpeedY
        {
            get => Math.Round(_speedY, 1);

            set
            {
                _speedX = value;
            }
        }
        
        private double _speedX;

        private double _speedY;
        public PlayerMovement()
        {
            SpeedX = 0;
            SpeedY = 0;

            Direction = "Stop";
        }
        public void MovePlayer(Player player)
        {
            player.XCoordinate += _speedX;
            player.YCoordinate += _speedY;

            ChangeMomentum();
        }
        public void SetDirection(string direction)
        {
            if (Direction == direction)
            {
                return;
            }

            Direction = direction;

            BuildMomentum = false;
        }
        public string ReturnDirection()
        {
            return Direction;
        }
        private void ChangeMomentum()
        {
            // When speed reaches 0
            if (SpeedX == 0 && Direction != "Stop")
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

                // Start building momentum
                BuildMomentum = true;

                return;
            }

            if (BuildMomentum)
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

            // Lower momentum
            if (!BuildMomentum)
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

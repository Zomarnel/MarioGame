using Models;
using Services;

namespace ViewModels
{
    public class GameSession
    {
        public Player CurrentPlayer { get; set; }   
        public GameSession()
        {
            CurrentPlayer = new Player(100, 72);
        }
        public void MovePlayer()
        {
            PlayerMovement.MovePlayer(CurrentPlayer);
        }
        public void SetHorizontalDirection(string direction)
        {
            CurrentPlayer.HorizontalDirection = direction;

            if (direction == "Idle")
            {
                CurrentPlayer.IsBuildingMomentum = false;
            }
        }
    }
}

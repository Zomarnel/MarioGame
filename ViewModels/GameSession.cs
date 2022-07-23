using Models;
using Services;

namespace ViewModels
{
    public class GameSession
    {
        public Player CurrentPlayer { get; set; }   
        public Movement _playerMovement { get; set; }
        public GameSession()
        {
            CurrentPlayer = new Player(100, 100);

            _playerMovement = new Movement();
        }
        public void MovePlayer()
        {
            _playerMovement.MovePlayer(CurrentPlayer);
        }
    }
}

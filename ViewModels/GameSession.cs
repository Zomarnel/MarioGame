using Models;
using Services;

namespace ViewModels
{
    public class GameSession
    {
        public Player CurrentPlayer { get; set; }   
        public PlayerMovement _playerMovement { get; set; }
        public GameSession()
        {
            CurrentPlayer = new Player(100, 100);

            _playerMovement = new PlayerMovement();
        }
        public void MovePlayer()
        {
            _playerMovement.MovePlayer(CurrentPlayer);
        }
    }
}

using Models;
using Services;

namespace ViewModels
{
    public class GameSession
    {
        public Player CurrentPlayer { get; set; }

        private PlayerMovement _playerMovement;

        public EventHandler<string> OnKeyDown;

        public EventHandler<string> OnKeyUp;
        public GameSession()
        {
            CurrentPlayer = new Player(100, 72);

            _playerMovement = new PlayerMovement(CurrentPlayer);

            OnKeyDown += _playerMovement.OnKeyPressed;
            OnKeyUp += _playerMovement.OnKeyRemoved;
        }
        public void MovePlayer()
        {
            _playerMovement.MovePlayer();

            Boundaries.IsPlayerInsideBoundaries(CurrentPlayer);
        }
    }
}

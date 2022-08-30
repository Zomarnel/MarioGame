using Models;
using Services;
using Core;

namespace ViewModels
{
    public class GameSession
    {
        public Player CurrentPlayer { get; set; }
        public GameSession()
        {
            CurrentPlayer = new Player(100, 64);
        }

        #region EVENTS
        public void OnKeyPressed(string direction)
        {
            if (direction == "Space" && CurrentPlayer.VerticalAction == Player.VerticalActions.IsStanding)
            {
                CurrentPlayer.VerticalAction = Player.VerticalActions.IsJumping;

                CurrentPlayer.VerticalSpeed = GameInfo.PLAYER_VERTICAL_SPEED;

                CurrentPlayer.JumpLimit = CurrentPlayer.YCoordinate + 5 * GameInfo.SPRITE_HEIGHT;

            }

            if ((direction == "Left" || direction == "Right") && CurrentPlayer.HorizontalAction == Player.HorizontalActions.IsStanding)
            {
                CurrentPlayer.HorizontalAction = Player.HorizontalActions.IsSpeeding;
                switch (direction)
                {
                    case "Left":
                        CurrentPlayer.HorizontalSpeed = -GameInfo.PLAYER_HORIZONTAL_MIN_SPEED;
                        break;
                    case "Right":
                        CurrentPlayer.HorizontalSpeed = GameInfo.PLAYER_HORIZONTAL_MIN_SPEED;
                        break;
                }
            }
        }
        public void OnKeyRemoved(string direction)
        {
            if (direction == "Space")
            {
                CurrentPlayer.VerticalAction = Player.VerticalActions.IsFalling;

                CurrentPlayer.VerticalSpeed = -GameInfo.GAME_GRAVITY;
            }

            if ((direction == "Left" || direction == "Right") && CurrentPlayer.HorizontalSpeed != 0)
            {
                CurrentPlayer.HorizontalAction = Player.HorizontalActions.IsSlowing;

            }
        }

        #endregion EVENTS

        #region PLAYERMOVEMENT
        public void MovePlayer()
        {
            MovePlayerHorizontally();

            MovePlayerVertically();

            SpriteControl.UpdatePlayerSprite(CurrentPlayer);

            Movement.MovementBoost(CurrentPlayer);
        }
        private void MovePlayerHorizontally()
        {
            if (Collisions.CanPlayerMoveHorizontally(CurrentPlayer))
            {
                Movement.MoveXCoordinate(CurrentPlayer);

                Collisions.HorizontalBoundariesCheck(CurrentPlayer);
            }
        }
        private void MovePlayerVertically()
        {
                Movement.MoveYCoordinate(CurrentPlayer);

                Collisions.VerticalBoundariesCheck(CurrentPlayer);
        }

        #endregion PLAYERMOVEMENT
    } 
}                  

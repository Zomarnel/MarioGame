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
        public void MovePlayer()
        {
            Movement.MoveXCoordinate(CurrentPlayer);

            Collisions.HorizontalBoundariesCheck(CurrentPlayer);

            Movement.MoveYCoordinate(CurrentPlayer);

            Collisions.VerticalBoundariesCheck(CurrentPlayer);

            Movement.MovementBoost(CurrentPlayer);
        }
        public void OnKeyPressed(string direction)
        {
            if (direction == "Space" && CurrentPlayer.VerticalAction == Player.VerticalActions.IsStanding)
            {
                CurrentPlayer.VerticalAction = Player.VerticalActions.IsJumping;

                CurrentPlayer.VerticalSpeed = GameInfo.PLAYER_VERTICAL_SPEED;

                CurrentPlayer.JumpLimit = CurrentPlayer.YCoordinate + 5 * GameInfo.SPRITE_HEIGHT;

                CurrentPlayer.CurrentSpriteID = 3;
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

                CurrentPlayer.CurrentSpriteID = 3;
            }

            if ((direction == "Left" || direction == "Right") && CurrentPlayer.HorizontalSpeed != 0)
            {
                CurrentPlayer.HorizontalAction = Player.HorizontalActions.IsSlowing;

                CurrentPlayer.CurrentSpriteID = 4;
            }
        }
    }
}

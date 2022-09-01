using Models;
using Services;
using Core;

namespace ViewModels
{
    public class GameSession
    {
        public Player CurrentPlayer { get; set; }
        private bool HasJumped { get; set; } = false;
        public GameSession()
        {
            CurrentPlayer = new Player(100, 64);
        }

        #region EVENTS
        public void OnKeyPressed(string direction)
        {
            if (direction == "Space" && CurrentPlayer.VerticalAction == Player.VerticalActions.IsStanding && !HasJumped)
            {
                CurrentPlayer.VerticalAction = Player.VerticalActions.IsJumping;

                CurrentPlayer.VerticalSpeed = GameInfo.PLAYER_VERTICAL_SPEED;

                CurrentPlayer.JumpLimit = CurrentPlayer.YCoordinate + 5 * GameInfo.SPRITE_HEIGHT;

                HasJumped = true;
            }
            else if (direction == "Left" || direction == "Right")
            {
                if (CurrentPlayer.VerticalAction != Player.VerticalActions.IsStanding)
                {
                    if ((direction == "Left" && CurrentPlayer.HorizontalSpeed > 0) || (direction == "Left" && CurrentPlayer.CurrentSpriteID > 0))
                    {
                        return;
                    }

                    if ((direction == "Right" && CurrentPlayer.HorizontalSpeed < 0) || (direction == "Right" && CurrentPlayer.CurrentSpriteID < 0))
                    {
                        return;
                    }

                }

                if (CurrentPlayer.HorizontalAction == Player.HorizontalActions.IsStanding)
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
                else if (CurrentPlayer.HorizontalAction == Player.HorizontalActions.IsSlowing)
                {
                    if ((CurrentPlayer.HorizontalSpeed > 0 && direction == "Left") || (CurrentPlayer.HorizontalSpeed < 0 && direction == "Right"))
                    {
                        CurrentPlayer.HorizontalAction = Player.HorizontalActions.ChangeOfDirection;
                    }
                }
            }
        }
        public void OnKeyRemoved(string direction)
        {
            if (direction == "Space")
            {
                CurrentPlayer.VerticalAction = Player.VerticalActions.IsFalling;

                CurrentPlayer.VerticalSpeed = -GameInfo.GAME_GRAVITY;

                HasJumped = false;
            }

            if ((direction == "Left" || direction == "Right") && CurrentPlayer.HorizontalAction != Player.HorizontalActions.IsStanding)
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

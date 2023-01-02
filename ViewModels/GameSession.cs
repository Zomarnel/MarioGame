using Models;
using Services;
using Core;

namespace ViewModels
{
    public class GameSession
    {
        public Player CurrentPlayer { get; set; }
        public World CurrentWorld { get; init; }
        private bool HasJumped { get; set; } = false;
        public GameSession()
        {
            CurrentPlayer = new Player(100, 64, 0, 0, 32, 32);

            CurrentWorld = WorldFactory.GetWorldByID(0);
        }

        #region EVENTS
        public void OnKeyPressed(string direction)
        {
            if (direction == "Space" && CurrentPlayer.VerticalAction == Player.VerticalActions.IsStanding && !HasJumped && CurrentPlayer.CanJumpCooldown)
            {
                CurrentPlayer.VerticalAction = Player.VerticalActions.IsJumping;

                Movement.OnJump(CurrentPlayer);

                HasJumped = true;
            }

            else if ((direction == "Left" || direction == "Right") && CurrentPlayer.HorizontalAction != Player.HorizontalActions.ChangeOfDirection)
            {

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

                HasJumped = false;
            }

            if ((direction == "Left" || direction == "Right") && CurrentPlayer.HorizontalAction != Player.HorizontalActions.IsStanding
                && CurrentPlayer.HorizontalAction != Player.HorizontalActions.ChangeOfDirection)
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

            Movement.MovementBoost(CurrentPlayer);
        }
        private void MovePlayerHorizontally()
        {
            if (Collisions.CanPlayerMoveHorizontally(CurrentPlayer, CurrentWorld.Blocks))
            {
                Movement.MoveXCoordinate(CurrentPlayer);

                CurrentWorld.WorldXCoordinate = Math.Abs(MapService.MapXCoordinate);

                Collisions.HorizontalBoundariesCheck(CurrentPlayer, CurrentWorld.Blocks);
            }

            UpdateService.UpdatePlayerSprite(CurrentPlayer);
        }
        private void MovePlayerVertically()
        {
            Movement.MoveYCoordinate(CurrentPlayer);

            Collisions.VerticalBoundariesCheck(CurrentPlayer, CurrentWorld.Blocks);

            UpdateService.UpdatePlayerSprite(CurrentPlayer);
        }

        #endregion PLAYERMOVEMENT

        public void UpdateCurrentWorld()
        {
            WorldFactory.UpdateWorld(CurrentWorld);
        }
    } 
}                  

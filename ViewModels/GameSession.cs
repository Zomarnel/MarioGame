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

                void localFunction()
                {
                    Thread.Sleep(150);

                    if (CurrentPlayer.VerticalAction != Player.VerticalActions.IsStanding)
                    {
                        CurrentPlayer.VerticalAction = Player.VerticalActions.IsFalling;
                    }

                    HasJumped = false;
                }

                Thread e = new Thread(localFunction);

                e.Start();
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

            Movement.PlayerMovementBoost(CurrentPlayer);
        }
        private void MovePlayerHorizontally()
        {
            if (Collisions.CanPlayerMoveHorizontally(CurrentPlayer, CurrentWorld.Blocks) && !CurrentPlayer.IsDead)
            {
                Movement.MovePlayerXCoordinate(CurrentPlayer);

                CurrentWorld.WorldXCoordinate = Math.Abs(MapService.MapXCoordinate);

                Collisions.HorizontalPlayerBoundariesCheck(CurrentPlayer, CurrentWorld.Blocks);

                Collisions.EntitiesCollisionsCheck(CurrentPlayer, WorldFactory.ReturnVisibleEnemies(CurrentWorld));
            }

            UpdateService.UpdatePlayerSprite(CurrentPlayer);
        }
        private void MovePlayerVertically()
        {
            Movement.MovePlayerYCoordinate(CurrentPlayer);

            if (!CurrentPlayer.IsDead)
            {
                Collisions.VerticalPlayerBoundariesCheck(CurrentPlayer, CurrentWorld.Blocks, CurrentWorld.Enemies);

                Collisions.EntitiesCollisionsCheck(CurrentPlayer, CurrentWorld.Enemies);
            }

            UpdateService.UpdatePlayerSprite(CurrentPlayer);
        }

        #endregion PLAYERMOVEMENT

        public void UpdateCurrentWorld()
        {
            UpdateService.UpdateWorld(CurrentWorld);
        }
    } 
}                  

using Models;
using Core;

namespace Services
{
    public static class Movement
    {
        #region PLAYER  
        private static double _initialY;
        public static void MovePlayerXCoordinate(Player player)
        {
            if (player.HorizontalSpeed < 0 || player.XCoordinate < GameInfo.SCREEN_WIDTH / 2 || MapService.HasMapReachedEnd)
            {
                player.XCoordinate += player.HorizontalSpeed;
            }
            else
            {
                player.XCoordinate = GameInfo.SCREEN_WIDTH / 2;

                MapService.MoveMap(player.HorizontalSpeed);
            }
        }
        public static void MovePlayerYCoordinate(Player player)
        {   
            player.YCoordinate += player.VerticalSpeed;
            
            if (player.VerticalAction != Player.VerticalActions.IsStanding)
            {
                if (player.VerticalAction == Player.VerticalActions.IsFalling)
                {
                    player.VerticalSpeed = CalculateVerticalSpeed(player, true);
                }
                else
                {
                    player.VerticalSpeed = CalculateVerticalSpeed(player);
                }
            }

             
            if (player.VerticalAction == Player.VerticalActions.IsJumping)
            {
                if (player.YCoordinate >= player.JumpLimit || player.VerticalSpeed == 0)
                {
                    player.VerticalAction = Player.VerticalActions.IsFalling;
                }
            }
        }

        public static void PlayerMovementBoost(Player player)
        {
            if (player.HorizontalAction == Player.HorizontalActions.IsSpeeding)
            {
                if (player.HorizontalSpeed > 0)
                {
                    if (player.HorizontalSpeed < GameInfo.PLAYER_HORIZONTAL_MAX_SPEED)
                    {
                        player.HorizontalSpeed += GameInfo.PLAYER_HORIZONTAL_ACCELERATION_SPEED;
                    }
                }

                else if (player.HorizontalSpeed < 0)
                {
                    if (player.HorizontalSpeed > -GameInfo.PLAYER_HORIZONTAL_MAX_SPEED)
                    {
                        player.HorizontalSpeed -= GameInfo.PLAYER_HORIZONTAL_ACCELERATION_SPEED;
                    }
                }
            }

            else if (player.HorizontalAction == Player.HorizontalActions.IsSlowing || player.HorizontalAction == Player.HorizontalActions.ChangeOfDirection)
            {
                if (player.HorizontalSpeed > 0)
                {
                    player.HorizontalSpeed -= GameInfo.PLAYER_HORIZONTAL_DECELERATION_SPEED;

                    if (player.HorizontalSpeed <= 0)
                    {
                        player.HorizontalSpeed = 0;

                        player.HorizontalAction = Player.HorizontalActions.IsStanding;
                    }
                }
                else if (player.HorizontalSpeed < 0)
                {
                    player.HorizontalSpeed += GameInfo.PLAYER_HORIZONTAL_DECELERATION_SPEED;

                    if (player.HorizontalSpeed >= 0)
                    {
                        player.HorizontalSpeed = 0;

                        player.HorizontalAction = Player.HorizontalActions.IsStanding;
                    }
                }
            }

            
        }

        public static void StopMovingHorizontally(Player player)
        {
            player.HorizontalAction = Player.HorizontalActions.IsStanding;
            player.HorizontalSpeed = 0;

            UpdateService.UpdatePlayerSprite(player);
        }
        public static void StopMovingVertically(Player player, bool fall = false)
        {
            if (!fall)
            {
                player.VerticalAction = Player.VerticalActions.IsStanding;
                player.VerticalSpeed = 0;

                player.CanJumpCooldown = false;

                player.JumpCooldown();
            }
            else
            {
                player.VerticalAction = Player.VerticalActions.IsFalling;

                _initialY = player.YCoordinate;

                player.VerticalSpeed = CalculateVerticalSpeed(player, fall);
            }

            UpdateService.UpdatePlayerSprite(player);
        }

        public static void OnJump(Player player)
        {
            _initialY = player.YCoordinate;

            player.JumpLimit = player.YCoordinate + 4 * GameInfo.SPRITE_HEIGHT;

            player.VerticalSpeed = CalculateVerticalSpeed(player);
        }

        #endregion PLAYER

        public static double CalculateVerticalSpeed(BaseEntity entity, bool isFalling = false)
        {
            double u = GameInfo.PLAYER_VERTICAL_SPEED * GameInfo.PLAYER_VERTICAL_SPEED;

            double gravity;

            if (entity is Player)
            {
                gravity = -2 * GameInfo.GAME_GRAVITY * (entity.YCoordinate - _initialY);
            }
            else
            {
                gravity = -2 * GameInfo.GAME_GRAVITY * (entity.YCoordinate - ((Enemy)entity).InitialY);
            }

            double speed;

            if (isFalling)
            {
                speed = u + gravity;

                return -Math.Sqrt(Math.Abs(speed));
            }

            speed = u + gravity;

            return Math.Sqrt(Math.Abs(speed));

        }
    }
}

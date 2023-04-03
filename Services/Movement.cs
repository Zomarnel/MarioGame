using Models;
using Core;
using System.Diagnostics;

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

            if (player.VerticalAction == Player.VerticalActions.IsStanding && player.HasKilledEnemyCooldown)
            {
                player.HasKilledEnemyCooldown = false;
            }
            
            if (player.VerticalAction != Player.VerticalActions.IsStanding)
            {
                if (player.VerticalAction == Player.VerticalActions.IsFalling)
                {
                    if (player.HasKilledEnemyCooldown)
                    {
                        player.VerticalSpeed = CalculateVerticalSpeed(Math.Sqrt(10), 0.5,
                                                                      player.YCoordinate, _initialY, true);
                    }
                    else
                    {
                        player.VerticalSpeed = CalculateVerticalSpeed(GameInfo.PLAYER_VERTICAL_SPEED, GameInfo.GAME_GRAVITY,
                                              player.YCoordinate, _initialY, true);
                    }

                }
                else
                {
                    if (player.HasKilledEnemyCooldown)
                    {
                        player.VerticalSpeed = CalculateVerticalSpeed(Math.Sqrt(10), 0.5,
                                                                      player.YCoordinate, _initialY);
                    }
                    else
                    {
                        player.VerticalSpeed = CalculateVerticalSpeed(GameInfo.PLAYER_VERTICAL_SPEED, GameInfo.GAME_GRAVITY,
                                              player.YCoordinate, _initialY);
                    }
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

                player.VerticalSpeed = CalculateVerticalSpeed(GameInfo.PLAYER_VERTICAL_SPEED, GameInfo.GAME_GRAVITY,
                                                              player.YCoordinate, _initialY, fall);
            }

            UpdateService.UpdatePlayerSprite(player);
        }

        public static void OnJump(Player player)
        {
            _initialY = player.YCoordinate;

            if (!player.HasKilledEnemyCooldown)
            {
                player.JumpLimit = player.YCoordinate + 4 * GameInfo.SPRITE_HEIGHT;

                player.VerticalSpeed = CalculateVerticalSpeed(GameInfo.PLAYER_VERTICAL_SPEED, GameInfo.GAME_GRAVITY,
                                              player.YCoordinate, _initialY);
            }
            else
            {
                player.JumpLimit = player.YCoordinate + 10;
                player.VerticalAction = Player.VerticalActions.IsJumping;
                player.VerticalSpeed = 0;

                player.VerticalSpeed = CalculateVerticalSpeed(Math.Sqrt(10), 0.5,
                              player.YCoordinate, _initialY);
            }


        }

        #endregion PLAYER

        public static double CalculateVerticalSpeed(double model_vertical_speed, double model_gravity, double yCoordinate, double initialY, bool isFalling = false)
        {
            double u = model_vertical_speed * model_vertical_speed;
            double gravity = -2 * model_gravity * (yCoordinate - initialY);

            double speed = Math.Sqrt(Math.Abs(u + gravity));

            if (isFalling)
            {
                return -speed;
            }

            return speed;
            /*
            double player_vertical_speed = GameInfo.PLAYER_VERTICAL_SPEED;
            double game_gravity = GameInfo.GAME_GRAVITY;


            if (entity is Player)
            {
                if (((Player)entity).HasKilledEnemyCooldown)
                {
                    player_vertical_speed = Math.Sqrt(10);
                    game_gravity = 0.5;
                }
            }
            double u = player_vertical_speed * player_vertical_speed;

            double gravity;

            if (entity is Player)
            {
                gravity = -2 * game_gravity * (entity.YCoordinate - _initialY);
            }
            else
            {
                gravity = -2 * game_gravity * (entity.YCoordinate - ((Enemy)entity).InitialY);
            }

            double speed;

            if (isFalling)
            {
                speed = u + gravity;

                return -Math.Sqrt(Math.Abs(speed));
            }

            speed = u + gravity;

            return Math.Sqrt(Math.Abs(speed));
            */
        }
    }
}

﻿using Models;
using Core;

namespace Services
{
    public static class Movement
    {
        public static void MoveXCoordinate(Player player)
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

            Collisions.HorizontalBoundariesCheck(player);
        }
        public static void MoveYCoordinate(Player player)
        {
            player.YCoordinate += player.VerticalSpeed;

            Collisions.VerticalBoundariesCheck(player);
        }
        public static void MovementBoost(Player player)
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

            if (player.VerticalAction == Player.VerticalActions.IsJumping)
            {
                if (player.YCoordinate >= player.JumpLimit)
                {
                    player.YCoordinate = player.JumpLimit;

                    player.VerticalAction = Player.VerticalActions.IsFalling;

                    player.VerticalSpeed = -GameInfo.GAME_GRAVITY;
                }
            }
        }

        public static void StopMovingHorizontally(Player player)
        {
            player.HorizontalAction = Player.HorizontalActions.IsStanding;
            player.HorizontalSpeed = 0;
        }
        public static void StopMovingVertically(Player player, bool fall = false)
        {
            if (!fall)
            {
                player.VerticalAction = Player.VerticalActions.IsStanding;
                player.VerticalSpeed = 0;

                if (player.CurrentSpriteID > 0)
                {
                    player.CurrentSpriteID = 1;
                }
                else
                {
                    player.CurrentSpriteID = -1;
                }
            }
            else
            {
                player.VerticalAction = Player.VerticalActions.IsFalling;
                player.VerticalSpeed = -GameInfo.GAME_GRAVITY;

                if (player.CurrentSpriteID > 0)
                {
                    player.CurrentSpriteID = 4;
                }
                else
                {
                    player.CurrentSpriteID = -4;
                }
            }
        }
    }
}

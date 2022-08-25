using Models;
using Core;

namespace Services
{
    public class PlayerMovement
    {
        private Player _player;

        private double _playerJumpLimit;
        public PlayerMovement(Player player)
        {
            _player = player;
        }
        public void MovePlayerAsync()
        {
            if (_player.HasChangedSprite == null ||_player.HasChangedSprite == true)
            {
                _player.HasChangedSprite = false;

                Task.Delay(100).ContinueWith(_ => _player.ChangeSprite());
            }

            MoveXCoordinate();

            MoveYCoordinate();

            MovementBoost();
        }

        private void MoveXCoordinate()
        {
            if (_player.HorizontalSpeed < 0 || _player.XCoordinate < GameInfo.SCREEN_WIDTH / 2 || MapService.HasMapReachedEnd)
            {
                _player.XCoordinate += _player.HorizontalSpeed;
            }
            else
            {
                _player.XCoordinate = GameInfo.SCREEN_WIDTH / 2;

                MapService.MoveMap(_player.HorizontalSpeed);
            }

            Boundaries.HorizontalBoundariesCheck(_player);
        }
        private void MoveYCoordinate()
        {
            _player.YCoordinate += _player.VerticalSpeed;

            Boundaries.VerticalBoundariesCheck(_player);
        }
        private void MovementBoost()
        {
            if (_player.HorizontalAction == Player.HorizontalActions.IsSpeeding)
            {
                if (_player.HorizontalSpeed > 0)
                {
                    if (_player.HorizontalSpeed < GameInfo.PLAYER_HORIZONTAL_MAX_SPEED)
                    {
                        _player.HorizontalSpeed += GameInfo.PLAYER_HORIZONTAL_ACCELERATION_SPEED;
                    }
                }

                else if (_player.HorizontalSpeed < 0)
                {
                    if (_player.HorizontalSpeed > -GameInfo.PLAYER_HORIZONTAL_MAX_SPEED)
                    {
                        _player.HorizontalSpeed -= GameInfo.PLAYER_HORIZONTAL_ACCELERATION_SPEED;
                    }
                }
            }

            else if (_player.HorizontalAction == Player.HorizontalActions.IsSlowing)
            {
                if (_player.HorizontalSpeed > 0)
                {
                    _player.HorizontalSpeed -= GameInfo.PLAYER_HORIZONTAL_DECELERATION_SPEED;

                    if (_player.HorizontalSpeed <= 0)
                    {
                        _player.HorizontalSpeed = 0;

                        _player.HorizontalAction = Player.HorizontalActions.IsStanding;
                    }
                }
                else if (_player.HorizontalSpeed < 0)
                {
                    _player.HorizontalSpeed += GameInfo.PLAYER_HORIZONTAL_DECELERATION_SPEED;

                    if (_player.HorizontalSpeed >= 0)
                    {
                        _player.HorizontalSpeed = 0;

                        _player.HorizontalAction = Player.HorizontalActions.IsStanding;
                    }
                }
            }

            if (_player.VerticalAction == Player.VerticalActions.IsJumping)
            {
                if (_player.YCoordinate >= _playerJumpLimit)
                {
                    _player.YCoordinate = _playerJumpLimit;

                    _player.VerticalAction = Player.VerticalActions.IsFalling;

                    _player.VerticalSpeed = -GameInfo.GAME_GRAVITY;
                }
            }
        }

        #region EVENTS
        public void OnKeyPressed(object sender, string direction)
        {
            if (direction == "Space" && _player.VerticalAction == Player.VerticalActions.IsStanding)
            {
                _player.VerticalAction = Player.VerticalActions.IsJumping;

                _player.VerticalSpeed = GameInfo.PLAYER_VERTICAL_SPEED;

                _playerJumpLimit = _player.YCoordinate + 5*GameInfo.SPRITE_HEIGHT;
            }

            if ((direction == "Left" || direction == "Right") && _player.HorizontalAction == Player.HorizontalActions.IsStanding)
            {
                _player.HorizontalAction = Player.HorizontalActions.IsSpeeding;
                switch (direction)
                {
                    case "Left":
                        _player.HorizontalSpeed = -GameInfo.PLAYER_HORIZONTAL_MIN_SPEED;
                        break;
                    case "Right":
                        _player.HorizontalSpeed = GameInfo.PLAYER_HORIZONTAL_MIN_SPEED;
                        break;
                }
            }
        }
        public void OnKeyRemoved(object sender, string direction)
        {
            if (direction == "Space")
            {
                _player.VerticalAction = Player.VerticalActions.IsFalling;

                _player.VerticalSpeed = -GameInfo.GAME_GRAVITY;
            }

            if ((direction == "Left" || direction == "Right") && _player.HorizontalSpeed != 0)
            {
                _player.HorizontalAction = Player.HorizontalActions.IsSlowing;
            }
        }

        #endregion EVENTS
    }
}

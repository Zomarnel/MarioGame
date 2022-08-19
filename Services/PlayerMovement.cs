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
        public void MovePlayer()
        {
            MovePlayerXCoordinate();

            Boundaries.HorizontalBoundariesCheck(_player);

            _player.YCoordinate += _player.VerticalSpeed;

            Boundaries.VerticalBoundariesCheck(_player);

            MovementBoost();
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
        private void MovePlayerXCoordinate()
        {
            if (_player.HorizontalSpeed < 0 || _player.XCoordinate < GameInfo.SCREEN_WIDTH / 2 || MapService.HasMapReachedEnd)
            {
                _player.XCoordinate += _player.HorizontalSpeed;

                return;
            }

            _player.XCoordinate = GameInfo.SCREEN_WIDTH / 2;

            MapService.MoveMap(_player.HorizontalSpeed);
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

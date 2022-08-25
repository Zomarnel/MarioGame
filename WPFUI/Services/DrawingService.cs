using System.Windows.Media;
using System.Windows.Controls;

namespace WPFUI.Services
{
    public class DrawingService
    {
        private Image _playerSprite { get; set; }
        public void DrawPlayer(Canvas canvas, int id, double xCoordinate, double yCoordinate)
        {
            if (_playerSprite == null)
            {
                InitializePlayerSprite(id, canvas);
            }
            else
            {
                _playerSprite.Source = SpritesFactory.GetSprite(id);
            }

            Canvas.SetLeft(_playerSprite, xCoordinate);
            Canvas.SetBottom(_playerSprite, yCoordinate);
        }
        private void InitializePlayerSprite(int id, Canvas canvas)
        {
            _playerSprite = new Image()
            {
                Width = 32,
                Height = 32,
                Source = SpritesFactory.GetSprite(id)
            };

            RenderOptions.SetBitmapScalingMode(_playerSprite, BitmapScalingMode.NearestNeighbor);

            canvas.Children.Add(_playerSprite);

            Canvas.SetZIndex(_playerSprite, 99);
        }
    }
}

using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

namespace WPFUI.Services
{
    public class DrawingService
    {
        private Canvas _drawingBoard { get; init; }
        private UIElement? _playerSpriteCache { get; set; }

        private const int SPRITEWIDTH = 32;
        private const int SPRITEHEIGHT = 32;

        public DrawingService(Canvas drawingBoard)
        {
            _drawingBoard = drawingBoard;
        }
        public void DrawPlayer(double xCoordinate, double yCoordinate)
        {
            _drawingBoard.Children.Remove(_playerSpriteCache);

            Rectangle rect = new Rectangle()
            {
                Width = SPRITEWIDTH,
                Height = SPRITEHEIGHT,
                Fill = Brushes.Red
            };

            _drawingBoard.Children.Add(rect);

            Canvas.SetLeft(rect, xCoordinate);
            Canvas.SetBottom(rect, yCoordinate);

            _playerSpriteCache = rect;
        }
    }
}

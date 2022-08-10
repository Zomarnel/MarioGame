using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Core;

namespace WPFUI.Services
{
    public class MapService
    {
        private double _mapXCoordinate;

        private readonly Image _mapImage;

        public EventHandler OnMapEnd;
        public MapService(string imageName, Canvas canvas)
        {
            _mapXCoordinate = 0;

            BitmapImage bitmap = new BitmapImage(new Uri($"/Images/Worlds/{imageName}.png", UriKind.Relative));

            bitmap.BaseUri = Application.Current.StartupUri;

            _mapImage = new Image()
            {
                Source = bitmap,
                Height = bitmap.PixelHeight * 2,
                Width = bitmap.PixelWidth * 2
            };

            RenderOptions.SetBitmapScalingMode(_mapImage, BitmapScalingMode.NearestNeighbor);

            canvas.Children.Add(_mapImage);

            Canvas.SetBottom(_mapImage, 0);
            Canvas.SetLeft(_mapImage, 0);
        }
        public void MoveMap(object sender, double speed)
        {
            _mapXCoordinate -= speed;

            if (Math.Abs(_mapXCoordinate)+GameInfo.SCREEN_WIDTH >= _mapImage.Width)
            {
                _mapXCoordinate = -_mapImage.Width + GameInfo.SCREEN_WIDTH;

                OnMapEnd.Invoke(this, new EventArgs());
            }

            Canvas.SetLeft(_mapImage, _mapXCoordinate);
        }
    }
}

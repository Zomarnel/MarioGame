using System.Windows.Media.Imaging;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFUI.Services
{
    public class MapService
    {
        private int XCoordinate { get; set; }
        private BitmapImage MapImage { get; set; }

        private Canvas _canvas;
        public MapService(string mapName, Canvas canvas)
        {
            MapImage = new BitmapImage(new Uri($"/Images/Worlds/{mapName}.png", UriKind.Relative));

            MapImage.BaseUri = Application.Current.StartupUri;

            XCoordinate = 0;

            _canvas = canvas;
        }
        public void DrawMapOnCanvas()
        {
            CroppedBitmap croppedBitmap = new CroppedBitmap(MapImage, new Int32Rect(XCoordinate, 0, 512, 238));

            Image image = new Image()
            {
                Height = croppedBitmap.PixelHeight * 2,
                Width = croppedBitmap.PixelWidth * 2,
                Source = croppedBitmap
            };

            RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.NearestNeighbor);

            _canvas.Children.Add(image);

            Canvas.SetBottom(image, 0);
            Canvas.SetLeft(image, 0);
        }


    }
}

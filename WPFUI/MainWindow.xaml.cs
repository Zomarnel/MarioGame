using System.Windows;
using System.Windows.Input;
using System;
using ViewModels;
using Services;
using WPFUI.Services;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WPFUI
{
    public partial class MainWindow : Window
    {
        private GameSession _gameSession = new GameSession();

        private DrawingService _drawingService;

        private Image _mapImage;
        public MainWindow()
        {
            InitializeComponent();
            InitializeServices();
            InitializeMap();

            CompositionTarget.Rendering += Update;

            MapService.DrawMap += DrawMap;
            MapService.MapWidth = _mapImage.Width;
        }

        #region INITIALIZATIONS
        private void InitializeServices()
        {
            _drawingService = new DrawingService(Background);
        }
        private void InitializeMap()
        {
            BitmapImage bitmap = new BitmapImage(new Uri($"/Images/Worlds/World-1.png", UriKind.Relative));

            bitmap.BaseUri = Application.Current.StartupUri;

            _mapImage = new Image()
            {
                Source = bitmap,
                Height = bitmap.PixelHeight * 2,
                Width = bitmap.PixelWidth * 2
            };

            RenderOptions.SetBitmapScalingMode(_mapImage, BitmapScalingMode.NearestNeighbor);

            Background.Children.Add(_mapImage);

            Canvas.SetBottom(_mapImage, 0);
            Canvas.SetLeft(_mapImage, 0);
        }

        #endregion INITIALIZATIONS  

        #region KEYINPUT
        private void On_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Space)
            {
                _gameSession.OnKeyDown.Invoke(this, e.Key.ToString());
            }
        }
        private void On_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Space)
            {
                _gameSession.OnKeyUp.Invoke(this, e.Key.ToString());
            }
        }

        #endregion KEYINPUT
        private void Update(object sender, EventArgs e)
        {
            _gameSession.MovePlayer();

            _drawingService.DrawPlayer(_gameSession.CurrentPlayer.XCoordinate, _gameSession.CurrentPlayer.YCoordinate);
        }
        private void DrawMap(object sender, double xCoordinate)
        {
            Canvas.SetLeft(_mapImage, xCoordinate);
        }
    }
}

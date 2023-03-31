using System.Windows;
using System.Windows.Input;
using System;
using ViewModels;
using Services;
using WPFUI.Services;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Diagnostics;

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
            _drawingService = new DrawingService();
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
        private void On_KeyDown(object sender, KeyEventArgs? e)
        {
            if (Keyboard.IsKeyDown(Key.Left) || Keyboard.IsKeyDown(Key.A))
            {
                _gameSession.OnKeyPressed("Left");
            }

            if (Keyboard.IsKeyDown(Key.Right) || Keyboard.IsKeyDown(Key.D))
            {
                _gameSession.OnKeyPressed("Right");
            }

            if (Keyboard.IsKeyDown(Key.Space) || Keyboard.IsKeyDown(Key.Up) || Keyboard.IsKeyDown(Key.W))
            {
                _gameSession.OnKeyPressed("Space");
            }
        }
        private void On_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.A) 
            {
                _gameSession.OnKeyRemoved("Left");
            }

            if (e.Key == Key.Right || e.Key == Key.D)
            {
                _gameSession.OnKeyRemoved("Right");
            }

            if (e.Key == Key.Space || e.Key == Key.Up || e.Key == Key.W)
            {
                _gameSession.OnKeyRemoved("Space");
            }
        }

        private List<double> times = new List<double>();

        #endregion KEYINPUT
        private void Update(object sender, EventArgs e)
        {
            UpdatePlayer();

            UpdateWorldMap();
        }
        private void UpdatePlayer()
        {
            On_KeyDown(this, null);

            _gameSession.MovePlayer();

            UpdateService.UpdatePlayerSprite(_gameSession.CurrentPlayer);

            _drawingService.DrawPlayer(Background, _gameSession.CurrentPlayer.CurrentSpriteID,
                                                   _gameSession.CurrentPlayer.XCoordinate,
                                                   _gameSession.CurrentPlayer.YCoordinate);
        }
        private void UpdateWorldMap()
        {
            _gameSession.UpdateCurrentWorld();

            _drawingService.DrawBlocks(Background, WorldFactory.ReturnUpdatedBlocks(_gameSession.CurrentWorld));

            _drawingService.DisposeBlocks(Background, WorldFactory.ReturnDisposableBlocks(_gameSession.CurrentWorld));

            _drawingService.DrawEnemies(Background, _gameSession.CurrentWorld.Enemies);
        }
        private void DrawMap(object sender, double xCoordinate)
        {

            Canvas.SetLeft(_mapImage, xCoordinate);

            _drawingService.UpdateCurrentBlocks(Math.Abs(xCoordinate));

        }
    }
}



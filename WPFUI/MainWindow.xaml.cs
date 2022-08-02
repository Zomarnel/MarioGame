using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System;
using ViewModels;
using WPFUI.Services;

namespace WPFUI
{
    public partial class MainWindow : Window
    {
        private GameSession _gameSession = new GameSession();

        private DrawingService _drawingService;
        public MainWindow()
        {
            InitializeComponent();
            InitializeGameTimer();
            InitializeServices();
        }

        #region INITIALIZATIONS
        private void InitializeGameTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += Update;
            timer.Start();
        }
        private void InitializeServices()
        {
            _drawingService = new DrawingService(Background);
        }

        #endregion INITIALIZATIONS  

        #region KEYINPUT
        private void On_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Left)
            {
                _gameSession.SetHorizontalDirection("Left");
            }
            if (e.Key == Key.Right)
            {
                _gameSession.SetHorizontalDirection("Right");
            }
        }
        private void On_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Right)
            {
                _gameSession.SetHorizontalDirection("Idle");
            }
        }

        #endregion KEYINPUT
        private void Update(object sender, EventArgs e)
        {
            _gameSession.MovePlayer();

            _drawingService.DrawPlayer(_gameSession.CurrentPlayer.XCoordinate, _gameSession.CurrentPlayer.YCoordinate);
        }
    }
}

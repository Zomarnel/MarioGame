using System.Windows;
using System.Windows.Input;
using System;
using ViewModels;
using WPFUI.Services;
using System.Windows.Media;

namespace WPFUI
{
    public partial class MainWindow : Window
    {
        private GameSession _gameSession = new GameSession();

        private DrawingService _drawingService;
        public MainWindow()
        {
            InitializeComponent();
            InitializeServices();

            CompositionTarget.Rendering += Update;
        }

        #region INITIALIZATIONS
        private void InitializeServices()
        {
            _drawingService = new DrawingService(Background);
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
    }
}

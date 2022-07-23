using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Linq;
using System;
using ViewModels;
using Models;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace WPFUI
{
    public partial class MainWindow : Window
    {
        private GameSession _gameSession = new GameSession();

        private List<UIElement> _playerCache = new List<UIElement>(); 
        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += OnRaised;
            timer.Start();
        }
        private void OnRaised(object sender, EventArgs e)
        {
            Draw();

            _gameSession.MovePlayer();
        }

        private void Draw()
        {
            Background.Children.Remove(_playerCache.FirstOrDefault());

            _playerCache.Clear();

            Rectangle rect = new Rectangle()
            {
                Width = 32,
                Height = 32,
                Fill = Brushes.Red
            };

            Background.Children.Add(rect);

            Canvas.SetLeft(rect, _gameSession.CurrentPlayer.XCoordinate);
            Canvas.SetBottom(rect, _gameSession.CurrentPlayer.YCoordinate);

            _playerCache.Add(rect);
        }
        private void On_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Left)
            {
                _gameSession._playerMovement.SetDirection("Left");
            }
            if (e.Key == Key.Right)
            {
                _gameSession._playerMovement.SetDirection("Right");
            }
        }
        private void On_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

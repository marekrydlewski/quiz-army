using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.UI.Menu
{
    /// <summary>
    ///     Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl, ISwitchable
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        #region ISwitchable Methods
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void statisticButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Statistics());
        }

        private void addTestButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new AddTest());
        }

        private void loginTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Switcher.Switch(new Login());
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new UserSettings());
        }

        private void takeTestButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new TestList());
        }

        private void registerTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Switcher.Switch(new SignUp());
        }
    }
}

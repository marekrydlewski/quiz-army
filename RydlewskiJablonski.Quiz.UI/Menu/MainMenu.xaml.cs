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
        private IUser _user;

        public MainMenu()
        {
            InitializeComponent();
        }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            IUser user = state as IUser;
            if (user != null) _user = user;
            else
            {
                throw new ArgumentException("state is not IUser! it is: " + state.GetType().ToString());
            }
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

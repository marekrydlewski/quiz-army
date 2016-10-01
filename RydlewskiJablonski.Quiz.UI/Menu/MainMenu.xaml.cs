using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.ViewModels;

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

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            UserViewModel user = state as UserViewModel;
            if (user != null)
            {
                DataContext = user;
            }
            else
            {
                throw new ArgumentException("state is not UserViewModel! it is: " + state.GetType().ToString());
            }
        }
        #endregion

        private void statisticButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Statistics());
        }

        private void addTestButton_Click(object sender, RoutedEventArgs e)
        {
            var state = DataContext;
            Switcher.Switch(new AddTest(), state);
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
            var state = DataContext;
            Switcher.Switch(new TestList(), state);
        }

        private void editTestButton_Click(object sender, RoutedEventArgs e)
        {
            var state = DataContext;
            Switcher.Switch(new TestListEdit(), state);
        }
    }
}

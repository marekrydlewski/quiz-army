using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.ViewModels;

namespace RydlewskiJablonski.Quiz.UI.Menu
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl, ISwitchable
    {

        public Login()
        {
            InitializeComponent();
            
        }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            var userListViewModel = new UserListViewModel();
            List<IUser> users = userListViewModel.GetUsers();
            if (users.Exists(x => usernameTextBox.Text.Equals(x.Login) && passwordBox.Password.Equals(x.Password)))
            {
                Switcher.Switch(new MainMenu());
            }
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.ViewModels;

namespace RydlewskiJablonski.Quiz.UI.Menu
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : UserControl, ISwitchable
    {
        public SignUp()
        {
            InitializeComponent();
        }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            SignUpViewModel context = DataContext as SignUpViewModel;
            if (context != null)
            {
                context.AddUser(passwordBox1.Password, passwordBox2.Password);
            }
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.UI.Menu
{
    /// <summary>
    /// Interaction logic for AddTest.xaml
    /// </summary>
    public partial class AddTest : UserControl, ISwitchable
    {
        private IUser _user;

        public AddTest()
        {
            InitializeComponent();
        }

        private void AddQuestionsButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new AddQuestion());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
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
    }
}

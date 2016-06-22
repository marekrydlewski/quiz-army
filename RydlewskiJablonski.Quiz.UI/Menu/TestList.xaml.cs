using System;
using System.Windows;
using System.Windows.Controls;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.UI.Menu
{
    /// <summary>
    /// Interaction logic for TestList.xaml
    /// </summary>
    public partial class TestList : UserControl, ISwitchable
    {
        private IUser _user;

        public TestList()
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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }
    }
}

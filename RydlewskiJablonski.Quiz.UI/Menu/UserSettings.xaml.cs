using System;
using System.Windows.Controls;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.ViewModels;

namespace RydlewskiJablonski.Quiz.UI.Menu
{
    /// <summary>
    /// Interaction logic for UserSettings.xaml
    /// </summary>
    public partial class UserSettings : UserControl, ISwitchable
    {
        private object state;
        private IUser _user;

        public UserSettings()
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
    }
}

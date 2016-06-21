using System;
using System.Windows.Controls;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.UI.Menu
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : UserControl, ISwitchable
    {
        private IUser _user;

        public Statistics()
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
    }
}

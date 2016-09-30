using System;
using System.Windows.Controls;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.ViewModels;

namespace RydlewskiJablonski.Quiz.UI.Menu
{
    /// <summary>
    /// Interaction logic for AddTest.xaml
    /// </summary>
    public partial class AddTest : UserControl, ISwitchable
    {
        public AddTest()
        {
            InitializeComponent();
        }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            UserViewModel user = state as UserViewModel;
            AddTestViewModel context = DataContext as AddTestViewModel;
            if (user != null)
            {
                context.UserViewModel = user;
            }    
            else
            {
                throw new ArgumentException("state is not UserViewModel! it is: " + state.GetType());
            }
        }
        #endregion
    }
}

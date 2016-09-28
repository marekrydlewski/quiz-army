using System;
using System.Windows;
using System.Windows.Controls;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.ViewModels;

namespace RydlewskiJablonski.Quiz.UI.Menu
{
    /// <summary>
    /// Interaction logic for TestListEdit.xaml
    /// </summary>
    public partial class TestListEdit : UserControl, ISwitchable
    {
        public TestListEdit()
        {
            InitializeComponent();
        }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            UserViewModel user = state as UserViewModel;
            TestListViewModel context = DataContext as TestListViewModel;
            if (user != null)
            {
                context.UserViewModel = user;
            }
            else
            {
                throw new ArgumentException("state is not UserViewModel! it is: " + state.GetType().ToString());
            }
        }
        #endregion
    }
}

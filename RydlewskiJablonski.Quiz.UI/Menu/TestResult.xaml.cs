using System;
using System.Windows.Controls;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.ViewModels;

namespace RydlewskiJablonski.Quiz.UI.Menu
{
    /// <summary>
    /// Interaction logic for TestResult.xaml
    /// </summary>
    public partial class TestResult : UserControl, ISwitchable
    {
        public TestResult()
        {
            InitializeComponent();
        }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            TestViewModel context = state as TestViewModel;
            if (context != null)
            {
                DataContext = context;
            }
            else
            {
                throw new ArgumentException("state is not UserViewModel! it is: " + state.GetType().ToString());
            }
        }
        #endregion
    }
}

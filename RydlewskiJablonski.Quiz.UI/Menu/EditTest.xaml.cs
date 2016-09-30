using System;
using System.Windows.Controls;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.ViewModels;

namespace RydlewskiJablonski.Quiz.UI.Menu
{
    /// <summary>
    /// Interaction logic for EditTest.xaml
    /// </summary>
    public partial class EditTest : UserControl, ISwitchable
    {
        public EditTest()
        {
            InitializeComponent();  
        }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            TestViewModel test = state as TestViewModel;
            if (test != null)
            {
                DataContext = test;
            }
            else
            {
                throw new ArgumentException("state is not TestViewModel! it is: " + state.GetType().ToString());
            }
        }
        #endregion
    }
}

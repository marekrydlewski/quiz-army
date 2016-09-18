using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.ViewModels;

namespace RydlewskiJablonski.Quiz.UI.Menu
{
    /// <summary>
    /// Interaction logic for TakeTest.xaml
    /// </summary>
    public partial class TakeTest : UserControl, ISwitchable
    {
        public TakeTest()
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

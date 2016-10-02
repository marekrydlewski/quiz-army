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
    /// Interaction logic for TestStatistics.xaml
    /// </summary>
    public partial class TestStatistics : UserControl, ISwitchable
    {
        public TestStatistics()
        {
            InitializeComponent();
        }

    #region ISwitchable members
        public void UtilizeState(object state)
        {
            StatiscticsViewModel vm = state as StatiscticsViewModel;
            if (vm != null)
            {
                DataContext = vm;
            }
            else
            {
                throw new ArgumentException("State is not StatisticsViewModel! It is: " + state.GetType());
            }
        }
    #endregion
    }
}

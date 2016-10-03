using System;
using System.Windows.Controls;
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

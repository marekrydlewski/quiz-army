using System;
using System.Windows.Controls;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.ViewModels;

namespace RydlewskiJablonski.Quiz.UI.Menu
{
    /// <summary>
    /// Interaction logic for AddQuestion.xaml
    /// </summary>
    public partial class AddQuestion : UserControl, ISwitchable
    {
        public AddQuestion()
        {
            InitializeComponent();
        }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            var context = state as AddTestViewModel;
            if (context != null)
            {
                DataContext = context;
            }
            else
            {
                throw new ArgumentException("Wrong state type - not AddTestViewModel");
            }
        }
        #endregion
    }
}

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
        public bool IsActiveCheckbox;
        public bool IsMultipleChoice;
        public AddQuestion()
        {
            InitializeComponent();
            IsActiveCheckbox = true;
            IsMultipleChoice = true;
    }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            var context = state as AddTestViewModel;
            if (context != null)
            {
                DataContext = context;
                if (!context.TestViewModel.IsMultipleChoice)
                    IsMultipleChoice = false;
            }
            else
            {
                throw new ArgumentException("Wrong state type - not AddTestViewModel");
            }
        }
        #endregion

        private void IsCorrectCheckbox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!IsActiveCheckbox)
            {
            var chk = sender as CheckBox;
            chk.IsChecked = false;
            }
            if (!IsMultipleChoice) IsActiveCheckbox = false;
        }
    }
}

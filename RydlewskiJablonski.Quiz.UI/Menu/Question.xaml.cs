using System;
using System.Windows.Controls;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.ViewModels;

namespace RydlewskiJablonski.Quiz.UI.Menu
{
    /// <summary>
    /// Interaction logic for Question.xaml
    /// </summary>
    public partial class Question : UserControl, ISwitchable
    {
        public bool IsMultipliedAnswer { get; set; }
        public CheckBox lastChecked;

        public Question()
        {
            IsMultipliedAnswer = false;
            InitializeComponent();
        }

        public void UtilizeState(object state)
        {
            QuestionViewModel question = state as QuestionViewModel;
            if (question != null)
            {
                IsMultipliedAnswer = question.Test.IsMultipleChoice;
                DataContext = question;
            }
            else
            {
                throw new ArgumentException("state is not QuestionViewModel! it is: " + state.GetType());
            }
        }

        private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if(!IsMultipliedAnswer)
            {
                CheckBox activeCheckBox = sender as CheckBox;
                if (activeCheckBox != lastChecked && lastChecked != null) lastChecked.IsChecked = false;
                lastChecked = activeCheckBox.IsChecked == true ? activeCheckBox : null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.ViewModels;

namespace RydlewskiJablonski.Quiz.UI.Menu
{
    /// <summary>
    /// Interaction logic for AddQuestion.xaml
    /// </summary>
    public partial class AddQuestion : UserControl, ISwitchable
    {
        private IUser _user;
        public TestViewModel TestViewModel { get; set; }
        public QuestionViewModel QuestionViewModel { get; set; }

        public AddQuestion()
        {
            TestViewModel = new TestViewModel();
            QuestionViewModel = new QuestionViewModel();
            InitializeComponent();
        }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            Type stateType = state.GetType();
            PropertyInfo testProperty = stateType.GetProperty("Test");
            TestViewModel = testProperty.GetValue(state, null) as TestViewModel;

            //IUser user = state as IUser;
            //if (user != null) _user = user;
            //else
            //{
            //    throw new ArgumentException("state is not IUser! it is: " + state.GetType().ToString());
            //}
        }
        #endregion

        private void NextQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            TestViewModel.AddQuestion(QuestionViewModel);
            var state = new {Test = TestViewModel};
            Switcher.Switch(new AddQuestion(), state);
        }

        private void CompleteTestButton_Click(object sender, RoutedEventArgs e)
        {
            TestViewModel.AddTest();
            Switcher.Switch(new MainMenu());
        }

        private void AddAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            QuestionViewModel.AddCurrentAnswer();
            AnswerTextBox.Clear();
            IsCorrectCheckbox.IsChecked = false;
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dialog.DefaultExt = ".png";
            dialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dialog.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dialog.FileName;
                QuestionViewModel.ImagePath = filename;
            }
        }
    }
}

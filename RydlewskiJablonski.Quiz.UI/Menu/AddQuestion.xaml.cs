using System;
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
            InitializeComponent();
            QuestionViewModel = new QuestionViewModel();
        }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            Type stateType = state.GetType();
            PropertyInfo testProperty = stateType.GetProperty("Test");
            TestViewModel = testProperty.GetValue(state, null) as TestViewModel;

            TestNameTextBlock.SetBinding(TextBlock.TextProperty, "TestViewModel.Name");
            QuestionTextBox.SetBinding(TextBox.TextProperty, "QuestionViewModel.Text");
            PointsTextBox.SetBinding(TextBox.TextProperty, "QuestionViewModel.Points");
            ImagePathTextBox.SetBinding(TextBox.TextProperty, "QuestionViewModel.ImagePath");
            AnswerTextBox.SetBinding(TextBox.TextProperty, "QuestionViewModel.CurrentAnswer.Text");
            IsCorrectCheckbox.SetBinding(ToggleButton.IsCheckedProperty, "QuestionViewModel.CurrentAnswer.IsCorrect");
            AnswersListBox.SetBinding(ItemsControl.ItemsSourceProperty, "QuestionViewModel.AnswerViewModels");
            AnswersListBox.DisplayMemberPath = "Text";
            AnswersListBox.SelectedValuePath = "Id";



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

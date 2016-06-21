using System;
using System.Windows;
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

        private void NextQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            var context = DataContext as AddTestViewModel;
            if (context != null)
            {
                context.AddCurrentQuestion();
            }
        }

        private void CompleteTestButton_Click(object sender, RoutedEventArgs e)
        {
            var context = DataContext as AddTestViewModel;
            if (context != null)
            {
                context.AddCurrentTest();
            }
        }

        private void AddAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            var context = DataContext as AddTestViewModel;
            if (context != null)
            {
                context.AddCurrentAnswer();
            }
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            var context = DataContext as AddTestViewModel;
            if (context != null)
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
                    context.QuestionViewModel.ImagePath = filename;
                }
            }
        }
    }
}

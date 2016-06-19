using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using RydlewskiJablonski.Quiz.DAO.BO;
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
    }
}

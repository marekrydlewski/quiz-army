using System;
using System.Windows.Controls;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.ViewModels;
using System.Windows;
using System.Windows.Threading;

namespace RydlewskiJablonski.Quiz.UI.Menu
{
    /// <summary>
    /// Interaction logic for Question.xaml
    /// </summary>
    public partial class Question : UserControl, ISwitchable
    {
        public Question()
        {
            InitializeComponent();
        }

        public void UtilizeState(object state)
        {
            QuestionViewModel question = state as QuestionViewModel;
            if (question != null)
            {
                DataContext = question;
            }
            else
            {
                throw new ArgumentException("state is not QuestionViewModel! it is: " + state.GetType());
            }
        }
    }
}

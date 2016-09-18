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

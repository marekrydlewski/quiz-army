using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using RydlewskiJablonski.Quiz.DAO.BO;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.Menu;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class AddTestViewModel : INotifyPropertyChanged
    {
        private IDAO _dao;

        public AddTestViewModel()
        {
            _dao = new DAO.DAO();
            _userViewModel = new UserViewModel();
            _testViewModel = new TestViewModel();   
            _questionViewModel = new QuestionViewModel();
            _answerViewModel = new AnswerViewModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private TestViewModel _testViewModel;

        public TestViewModel TestViewModel
        {
            get { return _testViewModel; }
            set
            {
                _testViewModel = value;
                OnPropertyChanged();
            }
        }

        private UserViewModel _userViewModel;

        public UserViewModel UserViewModel
        {
            get { return _userViewModel; }
            set
            {
                _userViewModel = value;
                OnPropertyChanged();
            }
        }

        private QuestionViewModel _questionViewModel;

        public QuestionViewModel QuestionViewModel
        {
            get { return _questionViewModel; }
            set
            {
                _questionViewModel = value;
                OnPropertyChanged();
            }
        }

        private AnswerViewModel _answerViewModel;

        public AnswerViewModel AnswerViewModel
        {
            get { return _answerViewModel; }
            set
            {
                _answerViewModel = value;
                OnPropertyChanged();
            }
        }

        public void AddCurrentQuestion()
        {
            _testViewModel.AddQuestion(_questionViewModel);
            QuestionViewModel = new QuestionViewModel();
        }

        public void AddCurrentAnswer()
        {
            _questionViewModel.AddAnswer(_answerViewModel);
            AnswerViewModel = new AnswerViewModel();
        }

        public void AddCurrentTest()
        {
            if (_dao.GetTests().Count == 0)
            {
                _testViewModel.Id = 1;
            }
            else
            {
                _testViewModel.Id = _dao.GetTests().Select(x => x.Id).Max() + 1;
            }

            _dao.AddTest(new Test
            {
                Id = _testViewModel.Id,
                GivenTime = _testViewModel.GivenTime,
                IsMultipleChoice = _testViewModel.IsMultipleChoice,
                Name = _testViewModel.Name,
                ScoringSchema = _testViewModel.ScoringSchema,
                Questions = _testViewModel.Questions
            });

            var state = _userViewModel;
            Switcher.Switch(new MainMenu(), state);
        }
    }
}
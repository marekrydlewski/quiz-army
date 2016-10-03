using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.Helpers;
using RydlewskiJablonski.Quiz.UI.Menu;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class AddTestViewModel : INotifyPropertyChanged
    {
        private readonly IDAO _dao;

        public AddTestViewModel()
        {
            _dao = (IDAO)AssemblyLoader.GetDAOConstructor().Invoke(new object[] { });
            _userViewModel = new UserViewModel();
            _testViewModel = new TestViewModel();
            _questionViewModel = new QuestionViewModel();
            _answerViewModel = new AnswerViewModel();
            _addQuestionsCommand = new RelayCommand<object>(param => AddQuestions());
            _returnToMenuCommand = new RelayCommand<object>(param => ReturnToMenu());
            _addCurrentTestCommand = new RelayCommand<object>(param => AddCurrentTest());
            _addCurrentQuestionCommand = new RelayCommand<object>(param => AddCurrentQuestion());
            _addCurrentAnswerCommand = new RelayCommand<object>(param => AddCurrentAnswer());
            _selectImageCommand = new RelayCommand<object>(param => SelectImage());
            _canAddMoreCorrectAnswers = true;
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

        private bool _canAddMoreCorrectAnswers;

        public bool CanAddMoreCorrectAnswers
        {
            get { return _canAddMoreCorrectAnswers; }
            set
            {
                _canAddMoreCorrectAnswers = value;
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
            if (!TestViewModel.IsMultipleChoice && CanAddMoreCorrectAnswers)
            {
                CanAddMoreCorrectAnswers = !_answerViewModel.IsCorrect;
            }
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

            var newTest = _dao.CreateNewTest();
            newTest.Id = TestViewModel.Id;
            newTest.GivenTime = TestViewModel.GivenTime;
            newTest.IsMultipleChoice = TestViewModel.IsMultipleChoice;
            newTest.Name = TestViewModel.Name;
            newTest.ScoringSchema = TestViewModel.ScoringSchema;
            newTest.Questions = TestViewModel.Questions;

            _dao.AddTest(newTest);

            var state = _userViewModel;
            Switcher.Switch(new MainMenu(), state);
        }

        #region Commands & navigation

        private readonly RelayCommand<object> _addQuestionsCommand;

        public RelayCommand<object> AddQuestionsCommand
        {
            get { return _addQuestionsCommand; }
        }

        private void AddQuestions()
        {
            Switcher.Switch(new AddQuestion(), this);
        }

        private readonly RelayCommand<object> _returnToMenuCommand;

        public RelayCommand<object> ReturnToMenuCommand
        {
            get { return _returnToMenuCommand; }
        }

        private void ReturnToMenu()
        {
            Switcher.Switch(new MainMenu(), UserViewModel);
        }

        private readonly RelayCommand<object> _addCurrentQuestionCommand;

        public RelayCommand<object> AddCurrentQuestionCommand
        {
            get { return _addCurrentQuestionCommand; }
        }

        private readonly RelayCommand<object> _addCurrentTestCommand;

        public RelayCommand<object> AddCurrentTestCommand
        {
            get { return _addCurrentTestCommand; }
        }

        private readonly RelayCommand<object> _addCurrentAnswerCommand;

        public RelayCommand<object> AddCurrentAsnwerCommand
        {
            get { return _addCurrentAnswerCommand; }
        }

        private readonly RelayCommand<object> _selectImageCommand;

        public RelayCommand<object> SelectImageCommand
        {
            get { return _selectImageCommand; }
        }

        private void SelectImage()
        { 
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".png",
                Filter =
                    "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"
            };
 
            bool? result = dialog.ShowDialog();

            if (result == true)
            { 
                string filename = dialog.FileName;
                QuestionViewModel.ImagePath = filename;
            }
        }

        #endregion
    }
}
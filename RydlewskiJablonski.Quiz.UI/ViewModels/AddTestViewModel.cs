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
        private readonly IDAO _dao;

        public AddTestViewModel()
        {
            _dao = new DAO.DAO();
            _userViewModel = new UserViewModel();
            _testViewModel = new TestViewModel();
            _questionViewModel = new QuestionViewModel();
            _answerViewModel = new AnswerViewModel();
            _addQuestionsCommand = new RelayCommand(param => AddQuestions());
            _returnToMenuCommand = new RelayCommand(param => ReturnToMenu());
            _addCurrentTestCommand = new RelayCommand(param => AddCurrentTest());
            _addCurrentQuestionCommand = new RelayCommand(param => AddCurrentQuestion());
            _addCurrentAnswerCommand = new RelayCommand(param => AddCurrentAnswer());
            _selectImageCommand = new RelayCommand(param => SelectImage());
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

        #region Commands & navigation

        private readonly RelayCommand _addQuestionsCommand;

        public RelayCommand AddQuestionsCommand
        {
            get { return _addQuestionsCommand; }
        }

        private void AddQuestions()
        {
            Switcher.Switch(new AddQuestion(), this);
        }

        private readonly RelayCommand _returnToMenuCommand;

        public RelayCommand ReturnToMenuCommand
        {
            get { return _returnToMenuCommand; }
        }

        private void ReturnToMenu()
        {
            Switcher.Switch(new MainMenu(), UserViewModel);
        }

        private readonly RelayCommand _addCurrentQuestionCommand;

        public RelayCommand AddCurrentQuestionCommand
        {
            get { return _addCurrentQuestionCommand; }
        }

        private readonly RelayCommand _addCurrentTestCommand;

        public RelayCommand AddCurrentTestCommand
        {
            get { return _addCurrentTestCommand; }
        }

        private readonly RelayCommand _addCurrentAnswerCommand;

        public RelayCommand AddCurrentAsnwerCommand
        {
            get { return _addCurrentAnswerCommand; }
        }

        private readonly RelayCommand _selectImageCommand;

        public RelayCommand SelectImageCommand
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
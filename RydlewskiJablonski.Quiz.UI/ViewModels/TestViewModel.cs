using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using RydlewskiJablonski.Quiz.Core;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.Helpers;
using RydlewskiJablonski.Quiz.UI.Menu;
using System;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class TestViewModel : INotifyPropertyChanged
    {
        private ITest _test;
        private IDAO _dao;

        public TestViewModel(ITest test)
        {
            _dao = (IDAO)AssemblyLoader.GetDAOConstructor().Invoke(new object[] { });
            _test = test;
            PopulateQuestions();
            _startTestCommand = new RelayCommand<object>(param => StartTest());
            _returnToTestsCommand = new RelayCommand<object>(param => ReturnToTests());
            _returnToMenuCommand = new RelayCommand<object>(param => ReturnToMenu());
            _returnToEditListCommand = new RelayCommand<object>(param => ReturnToEditList());
            _editTestCommand = new RelayCommand<object>(param => EditTest());
        }

        public TestViewModel()
        {
            _dao = (IDAO)AssemblyLoader.GetDAOConstructor().Invoke(new object[] { });
            _test = _dao.CreateNewTest();
            _test.Questions = new List<IQuestion>();
            PopulateQuestions();
        }

        private void PopulateQuestions()
        {
            _questionViewModels = new ObservableCollection<QuestionViewModel>();
            foreach (var question in _test.Questions)
            {
                _questionViewModels.Add(new QuestionViewModel(question));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Id
        {
            get { return _test.Id; }
            set
            {
                _test.Id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _test.Name; }
            set
            {
                _test.Name = value;
                OnPropertyChanged();
            }
        }

        public bool IsMultipleChoice
        {
            get { return _test.IsMultipleChoice; }
            set
            {
                _test.IsMultipleChoice = value;
                OnPropertyChanged();
            }
        }

        public int GivenTime
        {
            get { return _test.GivenTime; }
            set
            {
                _test.GivenTime = value;
                OnPropertyChanged();
            }
        }

        public ScoringSchemas ScoringSchema
        {
            get { return _test.ScoringSchema; }
            set
            {
                _test.ScoringSchema = value;
                OnPropertyChanged();
            }
        }

        public List<IQuestion> Questions
        {
            get { return _test.Questions; }
            set
            {
                _test.Questions = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<QuestionViewModel> _questionViewModels;

        public ObservableCollection<QuestionViewModel> QuestionViewModels
        {
            get { return _questionViewModels; }
            set
            {
                _questionViewModels = value;
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

        private TestResultViewModel _testResults;

        public TestResultViewModel TestResults
        {
            get { return _testResults; }
            set
            {
                _testResults = value;
                OnPropertyChanged();
            }
        }

        private double _pointsAcquired;

        public double PointsAcquired
        {
            get { return _pointsAcquired; }
            set
            {
                _pointsAcquired = value;
                OnPropertyChanged();
            }
        }

        public void AddQuestion(QuestionViewModel questionViewModel)
        {
            if (_questionViewModels.Count == 0)
            {
                questionViewModel.Id = 1;
            }     
            else
            {
                questionViewModel.Id = _questionViewModels.Select(x => x.Id).Max() + 1;
            }
            _questionViewModels.Add(questionViewModel);

            var newQuestion = _dao.CreateNewQuestion();
            newQuestion.Id = questionViewModel.Id;
            newQuestion.ImagePath = questionViewModel.ImagePath;
            newQuestion.Points = questionViewModel.Points;
            newQuestion.Text = questionViewModel.Text;
            newQuestion.Answers = questionViewModel.Answers;

            _test.Questions.Add(newQuestion);
        }

        public void CalculatePoints()
        {
            PointsAcquired = QuestionViewModels.Sum(x => x.PointsAcquired);
        }

        public void FinalizeTest()
        {
            var testResults = _dao.CreateNewTestStatistic();
            testResults.Points = PointsAcquired;
            testResults.TestId = Id;
            testResults.UserId = UserViewModel.Id;
            testResults.QuestionsStatistics = new List<IQuestionStatistic>();

            foreach (var question in QuestionViewModels)
            {
                var questionResults = _dao.CreateNewQuestionStatistic();
                questionResults.TestId = Id;
                questionResults.QuestionId = question.Id;
                questionResults.Time = question.TimeTaken;
                questionResults.Points = question.PointsAcquired;
                questionResults.AnswersStatistics = new List<IAnswerStatistic>();

                foreach (var answer in question.AnswerViewModels)
                {
                    var answerResults = _dao.CreateNewAnswerStatistic();
                    answerResults.TestId = Id;
                    answerResults.QuestionId = question.Id;
                    answerResults.AnswerId = answer.Id;
                    answerResults.IsCorrect = answer.IsCorrect;
                    answerResults.WasSelected = answer.IsSelectedAnswer;

                    questionResults.AnswersStatistics.Add(answerResults);
                }

                testResults.QuestionsStatistics.Add(questionResults);
            }

            testResults.Time = new TimeSpan(testResults.QuestionsStatistics.Sum(x => x.Time.Ticks));

            TestResults = new TestResultViewModel(testResults);
            _dao.SaveTestResults(testResults);
        }

        public void FinalizeEdition()
        {
            _dao.UpdateTest(_test);
        }

        #region Commands & navaigation

        private RelayCommand<object> _startTestCommand;

        public RelayCommand<object> StartTestCommand
        {
            get { return _startTestCommand; }
        }

        private void StartTest()
        {
            foreach (var question in QuestionViewModels)
            {
                question.AnswerViewModels.Shuffle();
            }

            var firstQuestion = QuestionViewModels.OrderBy(x => x.Id).First();
            firstQuestion.Test = this;
            firstQuestion.IsFinalQuestion = QuestionViewModels.Count == 1;
            firstQuestion.IsNotFinalQuestion = !firstQuestion.IsFinalQuestion;
            firstQuestion.RemainingTime = TimeSpan.FromMinutes(GivenTime);
            firstQuestion.StartTime = DateTime.Now;
            Switcher.Switch(new Menu.Question(), firstQuestion);
        }

        private RelayCommand<object> _returnToTestsCommand;

        public RelayCommand<object> ReturnToTestsCommand
        {
            get { return _returnToTestsCommand; }
        }

        private void ReturnToTests()
        {
            Switcher.Switch(new TestList(), _userViewModel);
        }

        private RelayCommand<object> _returnToMenuCommand;

        public RelayCommand<object> ReturnToMenuCommand
        {
            get { return _returnToMenuCommand; }
        }

        private void ReturnToMenu()
        {
            Switcher.Switch(new MainMenu(), UserViewModel);
        }

        private RelayCommand<object> _returnToEditListCommand;

        public RelayCommand<object> ReturnToEditListCommand
        {
            get { return _returnToEditListCommand; }
        }

        private void ReturnToEditList()
        {
            Switcher.Switch(new TestListEdit(), UserViewModel);
        }

        private RelayCommand<object> _editTestCommand;

        public RelayCommand<object> EditTestCommand
        {
            get { return _editTestCommand; }
        }

        private void EditTest()
        {
            FinalizeEdition();
        }

        #endregion
    }
}
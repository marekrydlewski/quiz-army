using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using RydlewskiJablonski.Quiz.Core;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.Menu;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class QuestionViewModel : INotifyPropertyChanged
    {
        private IQuestion _question;
        private IDAO _dao;

        public QuestionViewModel(IQuestion question)
        {
            _dao = new DAO.DAO();
            _question = question;
            PopulateAnswers();
            _nextQuestionCommand = new RelayCommand<object>(param => NextQuestion());
            _returnToMenuCommand = new RelayCommand<object>(param => ReturnToMenu());
            _finalizeTestCommand = new RelayCommand<object>(param => FinalizeTest());
        }

        public QuestionViewModel()
        {
            _dao = new DAO.DAO();
            _question = _dao.CreateNewQuestion();
            _question.Answers = new List<IAnswer>();

            PopulateAnswers();
            _nextQuestionCommand = new RelayCommand<object>(param => NextQuestion());
            _returnToMenuCommand = new RelayCommand<object>(param => ReturnToMenu());
            _finalizeTestCommand = new RelayCommand<object>(param => FinalizeTest());
        }

        private void PopulateAnswers()
        {
            _answerViewModels = new ObservableCollection<AnswerViewModel>();
            foreach (var answer in _question.Answers)
            {
                _answerViewModels.Add(new AnswerViewModel(answer));
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
            get { return _question.Id; }
            set
            {
                _question.Id = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get { return _question.Text; }
            set
            {
                _question.Text = value;
                OnPropertyChanged();
            }
        }

        public int Points
        {
            get { return _question.Points; }
            set
            {
                _question.Points = value;
                OnPropertyChanged();
            }
        }

        public string ImagePath
        {
            get { return _question.ImagePath; }
            set
            {
                _question.ImagePath = value;
                OnPropertyChanged();
            }
        }

        public List<IAnswer> Answers
        {
            get { return _question.Answers; }
            set
            {
                _question.Answers = value; 
                OnPropertyChanged();
            }
        }

        private ObservableCollection<AnswerViewModel> _answerViewModels;

        public ObservableCollection<AnswerViewModel> AnswerViewModels
        {
            get { return _answerViewModels; }
            set
            {
                _answerViewModels = value;
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

        public void AddAnswer(AnswerViewModel answer)
        {
            if (_answerViewModels.Count == 0)
            {
                answer.Id = 1;
            }
            else
            {
                answer.Id = _answerViewModels.Select(x => x.Id).Max() + 1;
            }
            AnswerViewModels.Add(answer);

            var newAnswer = _dao.CreateNewAnswer();
            newAnswer.Id = answer.Id;
            newAnswer.IsCorrect = answer.IsCorrect;
            newAnswer.Text = answer.Text;

            Answers.Add(newAnswer);
        }

        private TestViewModel _test;

        public TestViewModel Test
        {
            get { return _test; }
            set
            {
                _test = value;
                OnPropertyChanged();
            }
        }

        private bool _isFinalQuestion;

        public bool IsFinalQuestion
        {
            get { return _isFinalQuestion; }
            set
            {
                _isFinalQuestion = value;
                OnPropertyChanged();
            }
        }

        private bool _isNotFinalQuestion;

        public bool IsNotFinalQuestion
        {
            get { return _isNotFinalQuestion; }
            set
            {
                _isNotFinalQuestion = value;
                OnPropertyChanged();
            }
        }

        private void CalculatePoints()
        {
            double result = 0;

            switch (Test.ScoringSchema)
            {
                case ScoringSchemas.NoNegativePoints:
                    if (AnswerViewModels.Where(x => x.IsCorrect).All(x => x.IsSelectedAnswer)
                        && AnswerViewModels.Where(x => !x.IsCorrect).All(x => !x.IsSelectedAnswer))
                    {
                        result = Points;
                    }
                    break;
                case ScoringSchemas.NegativePointsForNoAnswer:
                    if (AnswerViewModels.Where(x => x.IsCorrect).All(x => x.IsSelectedAnswer)
                        && AnswerViewModels.Where(x => !x.IsCorrect).All(x => !x.IsSelectedAnswer))
                    {
                        result = Points;
                    }
                    else if (!AnswerViewModels.Any(x => x.IsSelectedAnswer))
                    {
                        result = -Points;
                    }
                    break;
                case ScoringSchemas.NegativePoints:
                    if (AnswerViewModels.Where(x => x.IsCorrect).All(x => x.IsSelectedAnswer)
                        && AnswerViewModels.Where(x => !x.IsCorrect).All(x => !x.IsSelectedAnswer))
                    {
                        result = Points;
                    }
                    else
                    {
                        result = -Points;
                    }
                    break;
                case ScoringSchemas.PartialNegativePoints:
                    if (AnswerViewModels.Where(x => x.IsCorrect).All(x => x.IsSelectedAnswer)
                        && AnswerViewModels.Where(x => !x.IsCorrect).All(x => !x.IsSelectedAnswer))
                    {
                        result = Points;
                    }
                    else
                    {
                        double pointsPerAnswer = Points / AnswerViewModels.Count;
                        foreach (var answer in AnswerViewModels)
                        {
                            if ((answer.IsSelectedAnswer && answer.IsCorrect) ||
                                (!answer.IsSelectedAnswer && !answer.IsCorrect))
                            {
                                result += pointsPerAnswer;
                            }
                            else
                            {
                                result -= pointsPerAnswer;
                            }
                        }
                    }
                    break;
            }

            PointsAcquired = result;
        }

        #region Commands & navigation

        private RelayCommand<object> _nextQuestionCommand;

        public RelayCommand<object> NextQuestionCommand
        {
            get { return _nextQuestionCommand; }
        }

        private void NextQuestion()
        {
            CalculatePoints();
            var nextQuestion = Test.QuestionViewModels.OrderBy(x => x.Id).SkipWhile(x => x.Id <= Id).First();
            nextQuestion.Test = Test;
            nextQuestion.IsFinalQuestion = Test.QuestionViewModels.OrderBy(x => x.Id).Last().Id == nextQuestion.Id;
            nextQuestion.IsNotFinalQuestion = !nextQuestion.IsFinalQuestion;
            Switcher.Switch(new Menu.Question(), nextQuestion);
        }

        private RelayCommand<object> _finalizeTestCommand;

        public RelayCommand<object> FinalizeTestCommand
        {
            get { return _finalizeTestCommand; }
        }

        void FinalizeTest()
        {
            CalculatePoints();
            Test.CalculatePoints();
            Test.FinalizeTest();
            Switcher.Switch(new TestResult(), Test);
        }

        private RelayCommand<object> _returnToMenuCommand;

        public RelayCommand<object> ReturnToMenuCommand
        {
            get { return _returnToMenuCommand; }
        }

        private void ReturnToMenu()
        {
            Switcher.Switch(new MainMenu(), Test.UserViewModel);
        }

        #endregion
    }
}
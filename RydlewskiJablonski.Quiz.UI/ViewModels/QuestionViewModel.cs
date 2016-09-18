using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using RydlewskiJablonski.Quiz.DAO.BO;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.Menu;
using Question = RydlewskiJablonski.Quiz.DAO.BO.Question;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class QuestionViewModel : INotifyPropertyChanged
    {
        private IQuestion _question;

        public QuestionViewModel(IQuestion question)
        {
            _question = question;
            PopulateAnswers();
            _nextQuestionCommand = new RelayCommand<object>(param => NextQuestion());
            _returnToMenuCommand = new RelayCommand<object>(param => ReturnToMenu());
        }

        public QuestionViewModel()
        {
            _question = new Question();
            _question.Answers = new List<IAnswer>();

            PopulateAnswers();
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
            Answers.Add(new Answer
            {
                Id = answer.Id,
                IsCorrect = answer.IsCorrect,
                Text = answer.Text
            });
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

        #region Commands & navigation

        private RelayCommand<object> _nextQuestionCommand;

        public RelayCommand<object> NextQuestionCommand
        {
            get { return _nextQuestionCommand; }
        }

        private void NextQuestion()
        {
            var nextQuestion = Test.QuestionViewModels.OrderBy(x => x.Id).SkipWhile(x => x.Id <= Id).First();
            nextQuestion.Test = Test;
            nextQuestion.IsFinalQuestion = Test.QuestionViewModels.OrderBy(x => x.Id).Last().Id == nextQuestion.Id;
            nextQuestion.IsNotFinalQuestion = !nextQuestion.IsFinalQuestion;
            Switcher.Switch(new Menu.Question(), nextQuestion);
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
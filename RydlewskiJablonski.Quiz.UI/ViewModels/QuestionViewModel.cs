using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class QuestionViewModel : INotifyPropertyChanged
    {
        private IQuestion _question;

        public QuestionViewModel(IQuestion question)
        {
            _question = question;
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
    }
}
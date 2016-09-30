using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class QuestionResultViewModel : INotifyPropertyChanged
    {
        private IDAO _dao;
        private IQuestionStatistic _questionResult;

        public QuestionResultViewModel(IQuestionStatistic questionResult)
        {
            _dao = new DAO.DAO();
            _questionResult = questionResult;
            _text =
                _dao.GetTests()
                    .Find(x => x.Id == questionResult.TestId)
                    .Questions.Find(x => x.Id == questionResult.QuestionId)
                    .Text;
            _answerResults = new List<AnswerResultViewModel>();
            foreach (var answerResult in QuestionResult.AnswersStatistics)
            {
                _answerResults.Add(new AnswerResultViewModel(answerResult));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public IQuestionStatistic QuestionResult
        {
            get { return _questionResult; }
            set
            {
                _questionResult = value;
                OnPropertyChanged();
            }
        }

        private string _text;

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        private List<AnswerResultViewModel> _answerResults;

        public List<AnswerResultViewModel> AnswerResults
        {
            get { return _answerResults; }
            set
            {
                _answerResults = value;
                OnPropertyChanged();
            }
        }
    }
}
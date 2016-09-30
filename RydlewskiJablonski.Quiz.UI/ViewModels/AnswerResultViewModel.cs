using System.ComponentModel;
using System.Runtime.CompilerServices;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class AnswerResultViewModel : INotifyPropertyChanged
    {
        private IAnswerStatistic _answerResult;
        private IDAO _dao;

        public AnswerResultViewModel(IAnswerStatistic answerResult)
        {
            _dao = new DAO.DAO();
            _answerResult = answerResult;
            _text =
                _dao.GetTests()
                    .Find(x => x.Id == answerResult.TestId)
                    .Questions.Find(x => x.Id == answerResult.QuestionId)
                    .Answers.Find(x => x.Id == answerResult.AnswerId)
                    .Text;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public IAnswerStatistic AnswerResult
        {
            get { return _answerResult; }
            set
            {
                _answerResult = value;
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
    }
}
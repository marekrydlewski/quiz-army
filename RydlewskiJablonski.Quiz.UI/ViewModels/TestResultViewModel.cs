using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class TestResultViewModel : INotifyPropertyChanged
    {
        private ITestStatistic _testResult;

        public TestResultViewModel(ITestStatistic testResult)
        {
            _testResult = testResult;
            _questionResults = new List<QuestionResultViewModel>();
            foreach (var questionResult in TestResult.QuestionsStatistics)
            {
                _questionResults.Add(new QuestionResultViewModel(questionResult));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public ITestStatistic TestResult
        {
            get { return _testResult; }
            set
            {
                _testResult = value;
                OnPropertyChanged();
            }
        }

        private List<QuestionResultViewModel> _questionResults;

        public List<QuestionResultViewModel> QuestionResults
        {
            get { return _questionResults; }
            set
            {
                _questionResults = value;
                OnPropertyChanged();
            }
        }
    }
}
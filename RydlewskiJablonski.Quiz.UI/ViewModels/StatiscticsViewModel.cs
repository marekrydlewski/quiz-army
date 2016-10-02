using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using LiveCharts;
using LiveCharts.Wpf;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.Helpers;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class StatiscticsViewModel : INotifyPropertyChanged
    {
        private IDAO _dao;
        private UserViewModel _userViewModel;
        private List<TestResultViewModel> _testStatistics;

        public StatiscticsViewModel()
        {
            _dao = new DAO.DAO();
            _userViewModel = new UserViewModel();
            _testStatistics = new List<TestResultViewModel>();
            _questionTimes = new List<TakeTimes>();
        }

        public StatiscticsViewModel(int testId, UserViewModel userViewModel)
        {
            _dao = new DAO.DAO();
            _userViewModel = userViewModel;
            _testStatistics = _dao.GetTestStatistics(testId).Select(x => new TestResultViewModel(x)).ToList();
            _testName = _dao.GetTests().Find(x => x.Id == testId).Name;
            _questions = _testStatistics.First()
                .QuestionResults.Select(x => new QuestionComboBoxItem
                {
                    Id = x.QuestionResult.QuestionId,
                    Text = x.Text
                }).ToList();
            _histogramData = new SeriesCollection();
            _questionTimes = new List<TakeTimes>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<TestResultViewModel> TestStatistics
        {
            get { return _testStatistics; }
            set
            {
                _testStatistics = value;
                OnPropertyChanged();
            }
        }

        public int TimesTaken
        {
            get { return TestStatistics.Count; }
        }

        private readonly string _testName;

        public string TestName
        {
            get { return _testName; }
        }

        private List<QuestionComboBoxItem> _questions;

        public List<QuestionComboBoxItem> Questions
        {
            get { return _questions; }
            set
            {
                _questions = value;
                OnPropertyChanged();
            }
        }

        private int _selectedQuestionId;

        public int SelectedQuestionId
        {
            get { return _selectedQuestionId; }
            set
            {
                _selectedQuestionId = value;
                CalculateQuestionTimes();
                CalculateChartData();
                OnPropertyChanged();
            }
        }

        private List<TakeTimes> _questionTimes;

        public List<TakeTimes> QuestionTimes
        {
            get { return _questionTimes; }
            set
            {
                _questionTimes = value;
                OnPropertyChanged();
            }
        }

        private string[] _histogramLabels;

        public string[] HistogramLabels
        {
            get { return _histogramLabels; }
            set
            {
                _histogramLabels = value;
                OnPropertyChanged();
            }
        }

        private SeriesCollection _histogramData;

        public SeriesCollection HistogramData
        {
            get { return _histogramData; }
            set
            {
                _histogramData = value;
                OnPropertyChanged();
            }
        }

        private double _questionAverageTime;

        public double QuestionAverageTime
        {
            get { return _questionAverageTime; }
            set
            {
                _questionAverageTime = value;
                OnPropertyChanged();
            }
        }

        private void CalculateQuestionTimes()
        {
            var newTimes = new List<TakeTimes>();
            var times = TestStatistics.Select(
                x =>
                    x.QuestionResults.Find(y => y.QuestionResult.QuestionId == SelectedQuestionId)
                        .QuestionResult.Time.TotalMinutes).ToList();

            for (int i = 1; i <= times.Count; i++)
            {
                newTimes.Add(new TakeTimes {TakeId = i, Time = times[i - 1]});
            }

            QuestionTimes = newTimes;

            QuestionAverageTime = QuestionTimes.Select(x => x.Time).Average();
        }

        private void CalculateChartData()
        {
            var answersTaken = new Dictionary<string, int>();
            foreach (var testResults in TestStatistics)
            {
                var questionResults = testResults.QuestionResults.Find(x => x.QuestionResult.QuestionId == _selectedQuestionId);
                foreach (var answerResults in questionResults.AnswerResults)
                {
                    if (!answersTaken.ContainsKey(answerResults.Text))
                        answersTaken.Add(answerResults.Text, 0);
                    if (answerResults.AnswerResult.WasSelected)
                        answersTaken[answerResults.Text] += 1;
                }
            }

            HistogramLabels = answersTaken.Keys.ToArray();

            if (HistogramData.Count != 0)
            {
                HistogramData.RemoveAt(0);
            }
            HistogramData.Add(new ColumnSeries
            {
                Title = "Answers",
                Values = new ChartValues<int>(answersTaken.Values)
            });
        }
    }
}
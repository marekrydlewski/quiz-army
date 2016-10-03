using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using LiveCharts;
using LiveCharts.Wpf;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.Helpers;
using RydlewskiJablonski.Quiz.UI.Menu;

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
            _testTimes = new List<TakeTimes>();
            _returnCommand = new RelayCommand<object>(param => Return());
            _users = _dao.GetUsers();
            IUser allUsers = _dao.CreateNewUser();
            allUsers.Id = 0;
            allUsers.Login = "All";
            _users.Add(allUsers);
        }

        public StatiscticsViewModel(int testId, UserViewModel userViewModel)
        {
            _testId = testId;
            _dao = new DAO.DAO();
            _userViewModel = userViewModel;
            _testStatistics = _dao.GetTestStatistics(testId).Select(x => new TestResultViewModel(x)).ToList();
            _timesTaken = _testStatistics.Count;
            _testName = _dao.GetTests().Find(x => x.Id == testId).Name;
            AverageTestTime = _testStatistics.Select(x => x.TestResult.Time.TotalMinutes).Average();
            CalculateTestTimes();
            _questions = _testStatistics.First()
                .QuestionResults.Select(x => new QuestionComboBoxItem
                {
                    Id = x.QuestionResult.QuestionId,
                    Text = x.Text
                }).ToList();
            _histogramData = new SeriesCollection();
            _questionTimes = new List<TakeTimes>();
            _returnCommand = new RelayCommand<object>(param => Return());
            _users = _dao.GetUsers();
            IUser allUsers = _dao.CreateNewUser();
            allUsers.Id = 0;
            allUsers.Login = "All";
            _users.Add(allUsers);
        }

        private int _testId;

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

        private int _timesTaken;

        public int TimesTaken
        {
            get { return _timesTaken; }
            set
            {
                _timesTaken = value;
                OnPropertyChanged();
            }
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

        private List<TakeTimes> _testTimes;

        public List<TakeTimes> TestTimes
        {
            get { return _testTimes; }
            set
            {
                _testTimes = value;
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

        private double _averageTestTime;

        public double AverageTestTime
        {
            get { return _averageTestTime; }
            set
            {
                _averageTestTime = value;
                OnPropertyChanged();
            }
        }

        private List<IUser> _users;

        public List<IUser> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        private int _selectedUserId;

        public int SelectedUserId
        {
            get { return _selectedUserId; }
            set
            {
                _selectedUserId = value;

                if (_selectedUserId != 0)
                {
                    TestStatistics =
                    _dao.GetTestStatistics(_testId)
                        .Where(x => x.UserId == _selectedUserId)
                        .Select(x => new TestResultViewModel(x))
                        .ToList();
                    RecalculateStatistics();
                }
                else
                {
                    TestStatistics =
                    _dao.GetTestStatistics(_testId)
                        .Select(x => new TestResultViewModel(x))
                        .ToList();
                    RecalculateStatistics();
                }

                OnPropertyChanged();
            }
        }

        private void RecalculateStatistics()
        {
            TimesTaken = TestStatistics.Count;
            AverageTestTime = _testStatistics.Count == 0
                ? 0
                : _testStatistics.Select(x => x.TestResult.Time.TotalMinutes).Average();
            CalculateTestTimes();
            if (_selectedQuestionId != 0)
            {
                CalculateQuestionTimes();
                CalculateChartData();
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

            QuestionAverageTime = QuestionTimes.Count == 0 ? 0 : QuestionTimes.Select(x => x.Time).Average();
        }

        private void CalculateTestTimes()
        {
            var times =
                _testStatistics.Select((t, i) => new TakeTimes {TakeId = i + 1, Time = t.TestResult.Time.TotalMinutes})
                    .ToList();
            TestTimes = times;
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

    #region commands & nav

        private RelayCommand<object> _returnCommand;

        public RelayCommand<object> ReturnCommand
        {
            get { return _returnCommand; }
        }

        private void Return()
        {
            Switcher.Switch(new Statistics(), _userViewModel);
        }

        #endregion
    }
}
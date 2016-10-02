using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class StatiscticsViewModel : INotifyPropertyChanged
    {
        private IDAO _dao;
        private UserViewModel _userViewModel;
        private List<ITestStatistic> _testStatistics;

        public StatiscticsViewModel()
        {
            _dao = new DAO.DAO();
            _userViewModel = new UserViewModel();
            _testStatistics = new List<ITestStatistic>();
        }

        public StatiscticsViewModel(int testId, UserViewModel userViewModel)
        {
            _dao = new DAO.DAO();
            _userViewModel = userViewModel;
            _testStatistics = _dao.GetTestStatistics(testId);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<ITestStatistic> TestStatistics
        {
            get { return _testStatistics; }
            set
            {
                _testStatistics = value;
                OnPropertyChanged();
            }
        }
    }
}
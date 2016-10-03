using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.Menu;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class StatisticsTestListViewModel : INotifyPropertyChanged
    {
        private IDAO _dao;

        public StatisticsTestListViewModel()
        {
            _dao = new DAO.DAO();
            PopulateTestList(_dao.GetTests());
            _returnToMenuCommand = new RelayCommand<object>(param => ReturnToMenu());
            _viewStatisticsCommand = new RelayCommand<object>(param => ViewStatistics());
            _selectedTest = new TestViewModel();
        }

        private void PopulateTestList(List<ITest> tests)
        {
            _tests = new ObservableCollection<TestViewModel>();
            foreach (var test in tests)
            {
                if(_dao.GetAlreadyTakenTestsIds().Contains(test.Id))
                    _tests.Add(new TestViewModel(test));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<TestViewModel> _tests;

        public ObservableCollection<TestViewModel> Tests
        {
            get { return _tests; }
            set
            {
                _tests = value;
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

        private TestViewModel _selectedTest;

        public TestViewModel SelectedTest
        {
            get { return _selectedTest; }
            set
            {
                _selectedTest = value;
                OnPropertyChanged();
            }
        }

        #region Commands & navigation

        private RelayCommand<object> _returnToMenuCommand;

        public RelayCommand<object> ReturnToMenuCommand
        {
            get { return _returnToMenuCommand; }
        }

        private void ReturnToMenu()
        {
            Switcher.Switch(new MainMenu(), _userViewModel);
        }

        private RelayCommand<object> _viewStatisticsCommand;

        public RelayCommand<object> ViewStatisticsCommand
        {
            get { return _viewStatisticsCommand; }
        }

        private void ViewStatistics()
        {
            Switcher.Switch(new TestStatistics(), new StatiscticsViewModel(SelectedTest.Id, UserViewModel));
        }

        #endregion
    }
}
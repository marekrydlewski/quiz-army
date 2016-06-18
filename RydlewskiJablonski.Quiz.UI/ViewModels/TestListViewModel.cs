using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class TestListViewModel : INotifyPropertyChanged
    {
        private IDAO _dao;

        public TestListViewModel()
        {
            PopulateTestList(_dao.GetTests());
        }

        private void PopulateTestList(List<ITest> tests)
        {
            _tests = new ObservableCollection<TestViewModel>();
            foreach (var test in tests)
            {
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
    }
}
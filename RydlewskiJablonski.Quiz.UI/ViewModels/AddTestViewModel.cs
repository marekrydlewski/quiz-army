using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class AddTestViewModel : INotifyPropertyChanged
    {
        public AddTestViewModel()
        {
            _userViewModel = new UserViewModel();
            _testViewModel = new TestViewModel();   
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private TestViewModel _testViewModel;

        public TestViewModel TestViewModel
        {
            get { return _testViewModel; }
            set
            {
                _testViewModel = value;
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
    }
}
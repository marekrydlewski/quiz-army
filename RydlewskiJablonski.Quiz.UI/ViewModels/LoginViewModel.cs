using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class LoginViewModel: INotifyPropertyChanged
    {
        private UserListViewModel _userListViewModel;
        private bool _isIncorrect;

        public bool IsIncorrect
        {
            get { return _isIncorrect; }
            set
            {
                _isIncorrect = value;
                OnPropertyChanged();
            }
        }

        public LoginViewModel()
        {
            IsIncorrect = false;
            _userListViewModel = new UserListViewModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

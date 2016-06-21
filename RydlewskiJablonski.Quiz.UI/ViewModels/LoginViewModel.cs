using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using RydlewskiJablonski.Quiz.UI.Menu;

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
            _loginCommand = new RelayCommand(param => Login(param as string));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        public void Login(string password)
        {
            if (_userListViewModel.UserViewModels.Any(x => _userName.Equals(x.Login) && password.Equals(x.Password)))
            {
                Switcher.Switch(new MainMenu(), _userListViewModel.UserViewModels.FirstOrDefault(x => _userName.Equals(x.Login) && password.Equals(x.Password)));
            }
            else
            {
                _isIncorrect = true;
            }
        }

        #region Commands & navigation

        private RelayCommand _loginCommand;

        public RelayCommand LoginCommand
        {
            get { return _loginCommand; }
        }

        #endregion
    }
}

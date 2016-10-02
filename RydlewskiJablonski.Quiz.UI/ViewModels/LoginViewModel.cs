using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
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
            _loginCommand = new RelayCommand<object>(param => Login(param));
            _signupCommand = new RelayCommand<object>(param => Signup(param));
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

        public void Login(object passwordBox)
        {
            string password = (passwordBox as PasswordBox).Password;
            if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(_userName) && _userListViewModel.UserViewModels.Any(x => _userName.Equals(x.Login) && password.Equals(x.Password)))
            {
                Switcher.Switch(new MainMenu(), _userListViewModel.UserViewModels.FirstOrDefault(x => _userName.Equals(x.Login) && password.Equals(x.Password)));
            }
            else
            {
                IsIncorrect = true;
            }
        }

        public void Signup(object state)
        {
            Switcher.Switch(new SignUp());
        }

        #region Commands & navigation

        private RelayCommand<object> _loginCommand;

        public RelayCommand<object> LoginCommand
        {
            get { return _loginCommand; }
        }

        private RelayCommand<object> _signupCommand;

        public RelayCommand<object> SignupCommand
        {
            get { return _signupCommand; }
        }

        #endregion
    }
}

using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using RydlewskiJablonski.Quiz.UI.Menu;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        public SignUpViewModel()
        {
            _userListViewModel = new UserListViewModel();
            _userViewModel = new UserViewModel();
            _isLoginTaken = false;
            _signUpCommand = new RelayCommand<object>(param => AddUser(param));
            _returnToLoginCommand = new RelayCommand<object>(param => ReturnToLogin());
        }

        private bool _isLoginTaken;

        public bool IsLoginTaken 
        { 
            get { return _isLoginTaken; }
            set
            {
                _isLoginTaken = value;
                OnPropertyChanged();
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
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

        private UserListViewModel _userListViewModel;

        public UserListViewModel UserListViewModel
        {
            get { return _userListViewModel; }
            set
            {
                _userListViewModel = value;
                OnPropertyChanged();
            }
        }

        public void AddUser(object passwords)
        {
            var passswordBoxes = passwords as object[];
            var passwordBox = passswordBoxes[0] as PasswordBox;
            var repeatPasswordBox = passswordBoxes[1] as PasswordBox;
            var password = passwordBox.Password;
            var repeatPassword = repeatPasswordBox.Password;

            if (password.Equals(repeatPassword))
            {
                _userViewModel.Password = password;
                try
                {
                    _userListViewModel.AddUser(_userViewModel);
                    Switcher.Switch(new MainMenu(), _userViewModel);
                }
                catch (DuplicateNameException)
                {
                    _isLoginTaken = true;
                }
            }
        }

        #region Commands & navigation

        private RelayCommand<object> _signUpCommand;

        public RelayCommand<object> SignUpCommand
        {
            get { return _signUpCommand; }
        }

        private RelayCommand<object> _returnToLoginCommand;

        public RelayCommand<object> ReturnToLoginCommand
        {
            get { return _returnToLoginCommand; }
        }

        private void ReturnToLogin()
        {
            Switcher.Switch(new MainMenu());
        }

        #endregion
    }
}
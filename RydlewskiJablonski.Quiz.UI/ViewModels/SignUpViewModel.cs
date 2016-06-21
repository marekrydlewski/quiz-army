using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
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

        public void AddUser(string password, string repeatPassword)
        {
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
    }
}
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RydlewskiJablonski.Quiz.Core;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.Menu;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private IUser _user;
        private IDAO _dao;

        public UserViewModel(IUser user)
        {
            _user = user;
            IsEditor = user.UserType == UserTypes.Editor;
            _dao = new DAO.DAO();
            _returnToLoginCommand = new RelayCommand<object>(param => ReturnToLogin());
        }

        public UserViewModel()
        {
            _dao = new DAO.DAO();
            _user = _dao.CreateNewUser();
            _returnToLoginCommand = new RelayCommand<object>(param => ReturnToLogin());
        }

        public int Id
        {
            get { return _user.Id; }
            set
            {
                _user.Id = value;
                OnPropertyChanged();
            }
        }

        public UserTypes UserType
        {
            get { return _user.UserType; }
            set
            {
                _user.UserType = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get { return _user.FirstName; }
            set
            {
                _user.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _user.LastName; }
            set
            {
                _user.LastName = value;
                OnPropertyChanged();
            }
        }

        public string Login
        {
            get { return _user.Login; }
            set
            {
                _user.Login = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _user.Password; }
            set
            {
                _user.Password = value;
                OnPropertyChanged();
            }
        }

        private bool _isEditor;

        public bool IsEditor
        {
            get { return _isEditor; }
            set
            {
                _isEditor = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #region navigation
        private RelayCommand<object> _returnToLoginCommand;

        public RelayCommand<object> ReturnToLoginCommand
        {
            get { return _returnToLoginCommand; }
        }

        private void ReturnToLogin()
        {
            Switcher.Switch(new MainMenu(), this);
        }
        #endregion
    }
}
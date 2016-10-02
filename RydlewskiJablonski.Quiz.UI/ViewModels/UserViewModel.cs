using System.ComponentModel;
using System.Runtime.CompilerServices;
using RydlewskiJablonski.Quiz.Core;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.Menu;
using System;
using RydlewskiJablonski.Quiz.DAO.BO;
using System.Windows.Controls;

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
            _editAccountCommand = new RelayCommand<object>(param => EditAccount(param));
        }

        public UserViewModel()
        {
            _dao = new DAO.DAO();
            _user = _dao.CreateNewUser();
            _editAccountCommand = new RelayCommand<object>(param => EditAccount(param));
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

        private RelayCommand<object> _editAccountCommand;

        public RelayCommand<object> EditAccountCommand
        {
            get { return _editAccountCommand; }
        }

        private void EditAccount(object passwords)
        {
            var passswordBoxes = passwords as object[];
            var passwordBox = passswordBoxes[0] as PasswordBox;
            var repeatPasswordBox = passswordBoxes[1] as PasswordBox;
            var password = passwordBox.Password;
            var repeatPassword = repeatPasswordBox.Password;

            if (password.Equals(repeatPassword))
            {
                IUser user = new User();
                user.FirstName = FirstName;
                user.LastName = LastName;
                user.Login = Login;
                user.UserType = UserType;
                user.Id = Id;

                user.Password = password;
                _dao.UpdateUser(user);
                Switcher.Switch(new MainMenu());
            }

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
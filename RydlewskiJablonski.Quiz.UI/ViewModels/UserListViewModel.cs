using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using RydlewskiJablonski.Quiz.DAO.BO;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class UserListViewModel : INotifyPropertyChanged
    {
        private IDAO _dao;
        private ObservableCollection<UserViewModel> _userViewModels;

        public UserListViewModel()
        {
            _dao = new DAO.DAO();
            PopulateUsers(_dao.GetUsers());
        }

        private void PopulateUsers(List<IUser> users )
        {
            _userViewModels = new ObservableCollection<UserViewModel>();
            foreach (var user in users)
            {
                _userViewModels.Add(new UserViewModel(user));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<UserViewModel> UserViewModels
        {
            get { return _userViewModels; }
            set
            {
                _userViewModels = value;
                OnPropertyChanged();
            }
        }

        public void AddUser(UserViewModel user)
        {
            if (_userViewModels.Count == 0)
            {
                user.Id = 1;
            }
            else
            {
                user.Id = _userViewModels.Select(x => x.Id).Max() + 1;
            }

            if (!_userViewModels.Select(x => x.Login).Contains(user.Login))
            {
                _userViewModels.Add(user);
                _dao.AddUser(new User
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Login = user.Login,
                    Password = user.Password,
                    UserType = user.UserType,
                    TestsStatistics = new List<ITestStatistics>()
                });
            }
            else
            {
                throw new DuplicateNameException("User with login " + user.Login + " already exists.");
            }
        }
    }
}

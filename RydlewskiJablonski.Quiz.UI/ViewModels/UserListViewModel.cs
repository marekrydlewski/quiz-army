using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    class UserListViewModel : INotifyPropertyChanged
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

        public ObservableCollection<UserViewModel> GetUsers()
        {
            return _userViewModels;
        }
    }
}

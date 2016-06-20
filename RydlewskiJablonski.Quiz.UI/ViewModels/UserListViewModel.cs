using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.Annotations;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    class UserListViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<UserViewModel> _userViewModels;

        public UserListViewModel()
        {
            var dao = new DAO.DAO();
            PopulateUsers(dao.GetUsers());
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

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<UserViewModel> GetUsers()
        {
            return _userViewModels;
        }
    }
}

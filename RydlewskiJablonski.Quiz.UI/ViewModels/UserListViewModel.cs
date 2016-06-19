using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.Annotations;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    class UsersViewModel: INotifyPropertyChanged
    {
        private IDAO _dao;

        public UsersViewModel()
        {
            _dao = new DAO.DAO();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<IUser> GetUsers()
        {
            return _dao.GetUsers();
        }
    }
}

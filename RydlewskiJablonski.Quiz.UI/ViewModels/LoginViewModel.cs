using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RydlewskiJablonski.Quiz.UI.Annotations;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class LoginViewModel: INotifyPropertyChanged
    {

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

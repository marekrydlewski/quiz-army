using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.UI.Menu
{
    /// <summary>
    /// Interaction logic for TestList.xaml
    /// </summary>
    public partial class TestList : UserControl, ISwitchable
    {
        private IUser _user;

        public TestList()
        {
            InitializeComponent();
        }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            IUser user = state as IUser;
            if (user != null) _user = user;
            else
            {
                throw new ArgumentException("state is not IUser! it is: " + state.GetType().ToString());
            }
        }
        #endregion

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }
    }
}

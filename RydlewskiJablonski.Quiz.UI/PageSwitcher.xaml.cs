using System;
using System.Windows;
using System.Windows.Controls;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.UI
{
    /// <summary>
    /// Interaction logic for PageSwitcher.xaml
    /// </summary>
    public partial class PageSwitcher : Window
    {
        public PageSwitcher()
        {
            InitializeComponent();
            Switcher.PageSwitcher = this;
            Switcher.Switch(new Menu.Login());
        }

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, object state)
        {
            this.Content = nextPage;
            ISwitchable s = nextPage as ISwitchable;

            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("NextPage is not ISwitchable! "
                  + nextPage.Name.ToString());
        }
    }
}


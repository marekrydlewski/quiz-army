using System.Windows.Controls;

namespace RydlewskiJablonski.Quiz.UI
{
    public static class Switcher
    {
        public static PageSwitcher PageSwitcher;

        public static void Switch(UserControl newPage)
        {
            PageSwitcher.Navigate(newPage);
        }

        public static void Switch(UserControl newPage, object state)
        {
            PageSwitcher.Navigate(newPage, state);
        }
    }
}

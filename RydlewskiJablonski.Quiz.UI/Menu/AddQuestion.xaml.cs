using System;
using System.Reflection;
using System.Windows.Controls;
using RydlewskiJablonski.Quiz.Interfaces;
using RydlewskiJablonski.Quiz.UI.ViewModels;

namespace RydlewskiJablonski.Quiz.UI.Menu
{
    /// <summary>
    /// Interaction logic for AddQuestion.xaml
    /// </summary>
    public partial class AddQuestion : UserControl, ISwitchable
    {
        private IUser _user;
        private TestViewModel _testViewModel;

        public AddQuestion()
        {
            InitializeComponent();
        }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            Type stateType = state.GetType();
            PropertyInfo testProperty = stateType.GetProperty("Test");
            _testViewModel = testProperty.GetValue(state, null) as TestViewModel;
            //IUser user = state as IUser;
            //if (user != null) _user = user;
            //else
            //{
            //    throw new ArgumentException("state is not IUser! it is: " + state.GetType().ToString());
            //}
        }
        #endregion
    }
}

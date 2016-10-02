using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.UI.ViewModels
{
    public class StatiscticsViewModel
    {
        private IDAO _dao;
        private UserViewModel _userViewModel;
        private List<ITestStatistic> _testStatistics;

        public StatiscticsViewModel()
        {
            _dao = new DAO.DAO();
            _userViewModel = new UserViewModel();
            _testStatistics = new List<ITestStatistic>();
        }

        public StatiscticsViewModel(int testId, UserViewModel userViewModel)
        {
            _dao = new DAO.DAO();
            _userViewModel = userViewModel;
            _testStatistics = _dao.GetTestStatistics(testId);
        }
    }
}
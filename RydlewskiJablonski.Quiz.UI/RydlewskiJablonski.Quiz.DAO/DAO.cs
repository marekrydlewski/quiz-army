using System;
using System.Collections.Generic;
using System.Linq;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.DAO
{
    public class DAO : IDAO
    {
        private List<IUser> _users;
        private List<ITest> _tests;

        public List<IUser> GetUsers()
        {
            return _users;
        }

        public void AddUser(IUser user)
        {
            int newUserId = _users.Select(x => x.Id).Max() + 1;
            user.Id = newUserId;
            _users.Add(user);
        }

        public List<ITest> GetTests()
        {
            return _tests;
        }

        public void AddTest(ITest test)
        {
            int newTestId = _tests.Select(x => x.Id).Max() + 1;
            test.Id = newTestId;
            _tests.Add(test);
        }

        public List<ITestStatistics> GetUserStatistics(IUser user)
        {
            IUser usr = _users.FirstOrDefault(x => x.Id == user.Id);
            if (usr == null)
            {
                throw new ArgumentException("No such user");
            }
            return usr.TestsStatistics;
        }
    }
}
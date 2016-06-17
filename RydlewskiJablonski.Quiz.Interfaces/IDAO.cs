using System.Collections.Generic;

namespace RydlewskiJablonski.Quiz.Interfaces
{
    public interface IDAO
    {
        List<IUser> GetUsers();
        bool AddUser(IUser user);
        List<ITest> GetTests();
        bool AddTest(ITest test);
        List<ITestStatistics> GetUserStatistics(IUser user);
    }
}
using System.Collections.Generic;

namespace RydlewskiJablonski.Quiz.Interfaces
{
    public interface IDAO
    {
        List<IUser> GetUsers();
        void AddUser(IUser user);
        List<ITest> GetTests();
        void AddTest(ITest test);
    }
}
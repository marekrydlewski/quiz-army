using System.Collections.Generic;

namespace RydlewskiJablonski.Quiz.Interfaces
{
    public interface IDAO
    {
        List<IUser> GetUsers();
        void AddUser(IUser user);
        List<ITest> GetTests();
        void AddTest(ITest test);
        void UpdateTest(ITest test);
        void UpdateUser(IUser user);
        void SaveTestResults(ITestStatistic results);
        IUser CreateNewUser();
        ITest CreateNewTest();
        IQuestion CreateNewQuestion();
        IAnswer CreateNewAnswer();
        ITestStatistic CreateNewTestStatistic();
        IQuestionStatistic CreateNewQuestionStatistic();
        IAnswerStatistic CreateNewAnswerStatistic();
        List<ITestStatistic> GetTestStatistics(int testId);
        List<int> GetAlreadyTakenTestsIds();
    }
}
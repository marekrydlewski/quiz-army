using System;
using System.Collections.Generic;

namespace RydlewskiJablonski.Quiz.Interfaces
{
    public interface ITestStatistic
    {
        Guid Id { get; set; }
        int UserId { get; set; }
        int TestId { get; set; }
        double Points { get; set; }
        TimeSpan Time { get; set; }
        List<IQuestionStatistic> QuestionsStatistics { get; set; }
    }
}
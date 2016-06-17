using System;
using System.Collections.Generic;

namespace RydlewskiJablonski.Quiz.Interfaces
{
    public interface ITestStatistics
    {
        int TestId { get; set; }
        DateTime Date { get; set; }
        int TotalTime { get; set; }
        List<IQuestionStatistics> QuestionsStatistics { get; set; }
    }
}
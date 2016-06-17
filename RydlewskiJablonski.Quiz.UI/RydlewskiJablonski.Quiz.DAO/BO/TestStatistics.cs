using System;
using System.Collections.Generic;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.DAO.BO
{
    public class TestStatistics : ITestStatistics
    {
        public int TestId { get; set; }
        public DateTime Date { get; set; }
        public int TotalTime { get; set; }
        public List<IQuestionStatistics> QuestionsStatistics { get; set; }
    }
}
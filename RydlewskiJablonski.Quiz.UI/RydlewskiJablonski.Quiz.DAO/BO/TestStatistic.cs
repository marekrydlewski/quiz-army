using System;
using System.Collections.Generic;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.DAO.BO
{
    public class TestStatistic : ITestStatistic
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public int TestId { get; set; }
        public double Points { get; set; }
        public double Time { get; set; }
        public List<IQuestionStatistic> QuestionsStatistics { get; set; }
    }
}
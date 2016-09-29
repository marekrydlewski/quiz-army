using System;
using System.Collections.Generic;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.DAO.BO
{
    public class QuestionStatistic : IQuestionStatistic
    {
        public Guid TestTakeId { get; set; }
        public int TestId { get; set; }
        public int QuestionId { get; set; }
        public double Time { get; set; }
        public double Points { get; set; }
        public List<IAnswerStatistic> AnswersStatistics { get; set; }
    }
}
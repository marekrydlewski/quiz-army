using System;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.DAO.BO
{
    public class QuestionStatistic : IQuestionStatistic
    {
        public Guid TestTakeId { get; set; }
        public int QuestionId { get; set; }
        public double Time { get; set; }
        public double Points { get; set; }
    }
}
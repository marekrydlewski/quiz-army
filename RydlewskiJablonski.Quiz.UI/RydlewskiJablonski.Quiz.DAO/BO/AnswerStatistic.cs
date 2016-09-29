using System;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.DAO.BO
{
    public class AnswerStatistic : IAnswerStatistic
    {
        public Guid TestTakeId { get; set; }
        public int TestId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public bool IsCorrect { get; set; }
        public bool WasSelected { get; set; }
    }
}
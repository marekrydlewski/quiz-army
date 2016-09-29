using System;

namespace RydlewskiJablonski.Quiz.Interfaces
{
    public interface IQuestionStatistic
    {
        Guid TestTakeId { get; set; }
        int QuestionId { get; set; }
        double Time { get; set; }
        double Points { get; set; }
    }
}
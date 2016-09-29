using System;

namespace RydlewskiJablonski.Quiz.Interfaces
{
    public interface IAnswerStatistic
    {
        Guid TestTakeId { get; set; }
        int QuestionId { get; set; }
        int AnswerId { get; set; }
        bool IsCorrect { get; set; }
        bool WasSelected { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace RydlewskiJablonski.Quiz.Interfaces
{
    public interface IQuestionStatistic
    {
        Guid TestTakeId { get; set; }
        int TestId { get; set; }
        int QuestionId { get; set; }
        TimeSpan Time { get; set; }
        double Points { get; set; }
        List<IAnswerStatistic> AnswersStatistics { get; set; }
    }
}
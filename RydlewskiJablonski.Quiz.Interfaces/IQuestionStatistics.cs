using System.Collections.Generic;

namespace RydlewskiJablonski.Quiz.Interfaces
{
    public interface IQuestionStatistics
    {
        int QuestionId { get; set; }
        List<int> AnswersIds { get; set; }
    }
}
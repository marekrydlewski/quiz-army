using System.Collections.Generic;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.DAO.BO
{
    public class QuestionStatistics : IQuestionStatistics
    {
        public int QuestionId { get; set; }
        public List<int> AnswersIds { get; set; }
        public int TimeTaken { get; set; }
    }
}
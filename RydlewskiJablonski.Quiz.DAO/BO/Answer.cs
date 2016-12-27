using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.DAO.BO
{
    public class Answer : IAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int TestId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
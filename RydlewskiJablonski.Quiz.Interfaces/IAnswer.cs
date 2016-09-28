namespace RydlewskiJablonski.Quiz.Interfaces
{
    public interface IAnswer
    {
        int Id { get; set; }
        int QuestionId { get; set; }
        int TestId { get; set; }
        string Text { get; set; }
        bool IsCorrect { get; set; }
    }
}
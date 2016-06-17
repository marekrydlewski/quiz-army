namespace RydlewskiJablonski.Quiz.Interfaces
{
    public interface IAnswer
    {
        string Text { get; set; }
        bool IsCorrect { get; set; }
    }
}
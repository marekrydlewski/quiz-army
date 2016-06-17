using System.Collections.Generic;

namespace RydlewskiJablonski.Quiz.Interfaces
{
    public interface IQuestion
    {
        int Id { get; set; }
        string Text { get; set; }
        int Points { get; set; }
        string ImagePath { get; set; }
        List<IAnswer> Answers { get; set; }
    }
}
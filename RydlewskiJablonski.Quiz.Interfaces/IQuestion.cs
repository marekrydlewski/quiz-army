using System.Collections.Generic;

namespace RydlewskiJablonski.Quiz.Interfaces
{
    public interface IQuestion
    {
        string Text { get; set; }
        int Points { get; set; }
        string ImagePath { get; set; }
        List<IAnswer> Answers { get; set; }
    }
}
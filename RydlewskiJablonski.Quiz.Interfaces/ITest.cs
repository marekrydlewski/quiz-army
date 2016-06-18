using System.Collections.Generic;
using RydlewskiJablonski.Quiz.Core;

namespace RydlewskiJablonski.Quiz.Interfaces
{
    public interface ITest
    {
        int Id { get; set; }
        string Name { get; set; }
        bool IsMultipleChoice { get; set; }
        int GivenTime { get; set; }
        ScoringSchemas ScoringSchema { get; set; }
        List<IQuestion> Questions { get; set; }
    }
}
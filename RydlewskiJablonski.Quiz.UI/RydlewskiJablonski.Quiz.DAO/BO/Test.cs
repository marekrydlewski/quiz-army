using System.Collections.Generic;
using RydlewskiJablonski.Quiz.Core;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.DAO.BO
{
    public class Test : ITest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMultipleChoice { get; set; }
        public int GivenTime { get; set; }
        public ScoringSchemaEnum ScoringSchema { get; set; }
        public List<IQuestion> Questions { get; set; }
    }
}
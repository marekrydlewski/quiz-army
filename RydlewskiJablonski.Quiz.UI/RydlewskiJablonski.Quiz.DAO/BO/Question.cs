using System.Collections.Generic;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.DAO.BO
{
    public class Question : IQuestion
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Points { get; set; }
        public string ImagePath { get; set; }
        public List<IAnswer> Answers { get; set; }
    }
}
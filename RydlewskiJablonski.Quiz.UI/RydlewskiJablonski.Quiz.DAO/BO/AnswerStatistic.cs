using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.DAO.BO
{
    public class AnswerStatistic : IAnswerStatistic
    {
        [Key]
        [Column(Order = 1)]
        public Guid TestTakeId { get; set; }
        public int TestId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int QuestionId { get; set; }
        [Key]
        [Column(Order = 3)]
        public int AnswerId { get; set; }
        public bool IsCorrect { get; set; }
        public bool WasSelected { get; set; }
    }
}
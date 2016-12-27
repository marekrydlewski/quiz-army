using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.DAO.BO
{
    public class QuestionStatistic : IQuestionStatistic
    {
        [Key]
        [Column(Order = 1)]
        public Guid TestTakeId { get; set; }
        public int TestId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int QuestionId { get; set; }
        public TimeSpan Time { get; set; }
        public double Points { get; set; }
        public List<IAnswerStatistic> AnswersStatistics { get; set; }
    }
}
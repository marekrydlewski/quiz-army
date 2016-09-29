using System;

namespace RydlewskiJablonski.Quiz.Interfaces
{
    public interface ITestStatistic
    {
        Guid Id { get; set; }
        int UserId { get; set; }
        int TestId { get; set; }
        double Points { get; set; }
        double Time { get; set; }
    }
}
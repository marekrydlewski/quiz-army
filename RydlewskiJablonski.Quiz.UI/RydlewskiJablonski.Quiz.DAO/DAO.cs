using System;
using System.Collections.Generic;
using System.Linq;
using RydlewskiJablonski.Quiz.Core;
using RydlewskiJablonski.Quiz.DAO.BO;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.DAO
{
    public class DAO : IDAO
    {
        public DAO()
        {
        }

        public List<IUser> GetUsers()
        {
            using (var context = new TestsContext())
            {
                return Enumerable.Cast<IUser>(context.Users).ToList();
            }
        }

        public void AddUser(IUser user)
        {
            using (var context = new TestsContext())
            {
                var usersID = context.Users.Select(x => x.Id);
                int newUserId = usersID == null ? usersID.Max() + 1 : 1;
                user.Id = newUserId;
                context.Users.Add(user as User);
                context.SaveChanges();
            }
        }

        public List<ITest> GetTests()
        {
            using (var context = new TestsContext())
            {
                var tests = Enumerable.Cast<ITest>(context.Tests).ToList();
                foreach (var test in tests)
                {
                    test.Questions =
                        Enumerable.Cast<IQuestion>(context.Questions.Where(x => x.TestId == test.Id)).ToList();
                    foreach (var question in test.Questions)
                    {
                        question.Answers =
                            Enumerable.Cast<IAnswer>(
                                context.Answers.Where(x => x.TestId == test.Id && x.QuestionId == question.Id)).ToList();
                    }
                }
                return tests;
            }
        }

        public void AddTest(ITest test)
        {
            using (var context = new TestsContext())
            {
                int newTestId;
                if (!context.Tests.Any())
                {
                    newTestId = 1;
                }
                else
                {
                    newTestId = context.Tests.Select(x => x.Id).Max() + 1;
                }

                test.Id = newTestId;
                context.Tests.Add(test as Test);

                var questionId = context.Questions.Any() ? context.Questions.Select(x => x.Id).Max() + 1 : 1;
                foreach (var question in test.Questions)
                {
                    question.TestId = test.Id;
                    question.Id = questionId;
                    context.Questions.Add(question as Question);
                    foreach (var answer in question.Answers)
                    {
                        answer.TestId = test.Id;
                        answer.QuestionId = question.Id;
                        context.Answers.Add(answer as Answer);
                    }
                    questionId++;
                }
                context.SaveChanges();
            }
        }

        public void SaveTestResults(ITestStatistic results)
        {
            using (var context = new TestsContext())
            {
                results.Id = Guid.NewGuid();
                context.TestStatistics.Add(results as TestStatistic);
                foreach (var questionResult in results.QuestionsStatistics)
                {
                    questionResult.TestTakeId = results.Id;
                    context.QuestionStatistics.Add(questionResult as QuestionStatistic);
                    foreach (var answerResult in questionResult.AnswersStatistics)
                    {
                        answerResult.TestTakeId = results.Id;
                        context.AnswerStatistics.Add(answerResult as AnswerStatistic);
                    }
                }
                context.SaveChanges();
            }
        }

        public void UpdateTest(ITest updatedTest)
        {
            using (var context = new TestsContext())
            {
                var original = context.Tests.Find(updatedTest.Id);
                if (original != null)
                {
                    original.Name = updatedTest.Name;
                    original.GivenTime = updatedTest.GivenTime;
                    original.IsMultipleChoice = updatedTest.IsMultipleChoice;
                    original.ScoringSchema = updatedTest.ScoringSchema;
                    original.Questions = updatedTest.Questions;
                    context.SaveChanges();
                }

                foreach (var question in updatedTest.Questions)
                {
                    var originalQuestion = context.Questions.Find(question.Id);
                    if (originalQuestion != null)
                    {
                        originalQuestion.Points = question.Points;
                        originalQuestion.Text = question.Text;
                        context.SaveChanges();

                        foreach (var answer in question.Answers)
                        {
                            var originalAnswer = context.Answers.Find(answer.Id);
                            if (originalAnswer != null)
                            {
                                originalAnswer.IsCorrect = answer.IsCorrect;
                                originalAnswer.Text = answer.Text;
                                context.SaveChanges();
                            }
                        }
                    }
                }
            }
        }

        public void UpdateUser(IUser updatedUser)
        {
            using (var context = new TestsContext())
            {
                var original = context.Users.Find(updatedUser.Id);
                if (original != null)
                {
                    original.FirstName = updatedUser.FirstName;
                    original.LastName = updatedUser.LastName;
                    original.Login = updatedUser.Login;
                    original.Password = updatedUser.Password;
                    original.UserType = updatedUser.UserType;
                    context.SaveChanges();
                }
            }
        }

        public IUser CreateNewUser()
        {
            return new User();
        }

        public ITest CreateNewTest()
        {
            return new Test();
        }

        public IQuestion CreateNewQuestion()
        {
            return new Question();
        }

        public IAnswer CreateNewAnswer()
        {
            return new Answer();
        }

        public ITestStatistic CreateNewTestStatistic()
        {
            return new TestStatistic();
        }

        public IQuestionStatistic CreateNewQuestionStatistic()
        {
            return new QuestionStatistic();
        }

        public IAnswerStatistic CreateNewAnswerStatistic()
        {
            return new AnswerStatistic();
        }

        public List<ITestStatistic> GetTestStatistics(int testId)
        {
            List<ITestStatistic> stats;

            using (var context = new TestsContext())
            {
                stats = new List<ITestStatistic>();
                context.TestStatistics.Where(x => x.TestId == testId).ToList().ForEach(x => stats.Add(x));
                foreach (var testStat in stats)
                {
                    testStat.QuestionsStatistics = new List<IQuestionStatistic>();
                    context.QuestionStatistics.Where(x => x.TestTakeId == testStat.Id)
                        .ToList().ForEach(x => testStat.QuestionsStatistics.Add(x));

                    foreach (var questionStat in testStat.QuestionsStatistics)
                    {
                        questionStat.AnswersStatistics = new List<IAnswerStatistic>();
                        context.AnswerStatistics.Where(
                            x => x.TestTakeId == testStat.Id && x.QuestionId == questionStat.QuestionId)
                            .ToList().ForEach(x => questionStat.AnswersStatistics.Add(x));
                    }
                }
            }

            return stats;
        }

        public List<int> GetAlreadyTakenTestsIds()
        {
            using (var context = new TestsContext())
            {
                return context.TestStatistics.Select(x => x.TestId).Distinct().ToList();
            }
        }
    }
}
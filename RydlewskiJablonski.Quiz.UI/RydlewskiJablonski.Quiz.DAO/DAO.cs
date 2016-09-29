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
                int newUserId = context.Users.Select(x => x.Id).Max() + 1;
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
    }
}
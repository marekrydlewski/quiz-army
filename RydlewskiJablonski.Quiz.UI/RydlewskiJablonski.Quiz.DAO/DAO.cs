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
        private List<IUser> _users;
        private List<ITest> _tests;

        public DAO()
        {
            _users = new List<IUser>();
            _tests = new List<ITest>();

            _tests.Add(new Test
            {
                Id = 1,
                GivenTime = 60,
                IsMultipleChoice = false,
                Name = "Sample test",
                ScoringSchema = ScoringSchemas.NegativePoints,
                Questions = new List<IQuestion>
                {
                    new Question
                    {
                        Id = 1,
                        ImagePath = null,
                        Text = "Is that a question?",
                        Answers = new List<IAnswer>
                        {
                            new Answer
                            {
                                Id = 1,
                                IsCorrect = true,
                                Text = "Yes"
                            },
                            new Answer
                            {
                                Id = 2,
                                IsCorrect = false,
                                Text = "No"
                            }
                        }
                    }
                }
            });

            _users.Add(new User
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                Login = "user",
                Password = "password",
                UserType = UserTypes.User
            });
            _users.Add(new User
            {
                Id = 2,
                FirstName = "Test",
                LastName = "Editor",
                Login = "editor",
                Password = "password",
                UserType = UserTypes.Editor
            });
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
                foreach (var question in test.Questions)
                {
                    question.TestId = test.Id;
                    context.Questions.Add(question as Question);
                    foreach (var answer in question.Answers)
                    {
                        answer.TestId = test.Id;
                        answer.QuestionId = question.Id;
                        context.Answers.Add(answer as Answer);
                    }
                }
                context.SaveChanges();
            }
        }

        public List<ITestStatistics> GetUserStatistics(IUser user)
        {
            IUser usr = _users.FirstOrDefault(x => x.Id == user.Id);
            if (usr == null)
            {
                throw new ArgumentException("No such user");
            }
            return usr.TestsStatistics;
        }
    }
}
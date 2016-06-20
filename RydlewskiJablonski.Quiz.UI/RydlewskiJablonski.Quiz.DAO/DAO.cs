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
            return _users;
        }

        public void AddUser(IUser user)
        {
            int newUserId = _users.Select(x => x.Id).Max() + 1;
            user.Id = newUserId;
            _users.Add(user);
        }

        public List<ITest> GetTests()
        {
            return _tests;
        }

        public void AddTest(ITest test)
        {
            int newTestId;
            if (_tests.Count == 0)
            {
                newTestId = 1;
            }
            else
            {
                newTestId = _tests.Select(x => x.Id).Max() + 1;
            }
                
            test.Id = newTestId;
            _tests.Add(test);
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
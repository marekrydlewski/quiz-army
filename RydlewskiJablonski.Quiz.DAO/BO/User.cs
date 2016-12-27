using System.Collections.Generic;
using RydlewskiJablonski.Quiz.Core;
using RydlewskiJablonski.Quiz.Interfaces;

namespace RydlewskiJablonski.Quiz.DAO.BO
{
    public class User : IUser
    {
        public int Id { get; set; }
        public UserTypes UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
using RydlewskiJablonski.Quiz.Core;

namespace RydlewskiJablonski.Quiz.Interfaces
{
    public interface IUser
    {
        int Id { get; set; }
        UserTypeEnum UserType { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Login { get; set; }
        string Password { get; set; }
    }
}
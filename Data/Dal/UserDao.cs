using ASP1.Data.Context;
using ASP1.Data.Entities;

namespace ASP1.Data.Dal
{
    public class UserDao
    {
        private readonly DataContext _context;
        public UserDao(DataContext context) 
        {
            _context = context;
        }
        public bool IsEmailFree(String email)
        {
            return !_context
                .Users
                .Where(u => u.Email == email)
                .Any(); //значит что что-то есть

        }

        public void SignUpUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}

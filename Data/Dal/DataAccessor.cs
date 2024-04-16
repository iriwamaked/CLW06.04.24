using ASP1.Data.Context;

namespace ASP1.Data.Dal
{
    //сервіс - реєструємо у Program.cs
    public class DataAccessor
    {
        public DataContext _context;
        public UserDao UserDao { get; private set; }
        public DataAccessor(DataContext context)
        {
            _context = context;
            UserDao = new(_context);
        }
    }
}

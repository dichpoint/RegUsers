using System.Data.Entity; // технология, позволяющая работать с СУБД

namespace RegUsers
{
    // связь с БД
    internal class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppContext() : base("DefaultConnection") { }

    }
}
using ASP1.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASP1.Data.Context
{
    public class DataContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        //нужно перегрузить конструктор, который принимает options и передаем его в базовый конструктор
        //при этом по сути он ничего не делает, дополнительных действий не делает, но передает эти options
        //Подключение к реальной БД хотя и является свойством контекста данных, но его параметры традиционно
        //сохраняются в файлах конфигурации всего проекта (appsettings.json).
        //Соответственно, передача этих параметров будет осуществляться
        //из Program.cs, где подключаются эти файлы
        public DataContext(DbContextOptions options):base(options) {}

        //Связи между данными (Relations) и ограничения (Constraints) указываются в специальном методе 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Укажем UNIQUE ограничения для E-mail, которое используется вместо логина
            modelBuilder.Entity<User>()
                 .HasIndex(u => u.Email) //указываем, что будет индексируемое поле user.Emai;
                 .IsUnique();              // и это поле будет уникальным

            //тут указываются как связи, так и ограничения
        }
    }
}

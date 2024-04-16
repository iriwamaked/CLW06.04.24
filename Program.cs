using ASP1.Data.Context;
using ASP1.Data.Dal;
using ASP1.Services.Hash;
using ASP1.Services.Kdf;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Реєстрація сервісу - його додавання до контейнеру build.Services
//builder.Services.AddSingleton<IHashService, Md5HashService>();
/*При слідуванні DIP встановлюється "звязка" між інтерфейсом та імплементацією. Дану інструкцію
 можна читати у такий спосіб: "якщо буде запит на інжекцію на IHashService, то видати обєкт Md5HashService"*/
builder.Services.AddSingleton<IHashService, ShaHashService>();
//
builder.Services.AddSingleton<IKdfService, Pdkdf1Service>();

//Реєструємо контекст даних, а також передаємо йому конфігурацію
builder.Services.AddDbContext<DataContext>(options=>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MsSql") ),
        ServiceLifetime.Singleton
   );

builder.Services.AddSingleton<DataAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using FourthTask.DataBase;
using FourthTask.DataBase.Entities; 
using FourthTask.Data.Repositories.Abstractions;
using FourthTask.Data.Repositories;
using FourthTask.Data.Abstractions.Repositories;
using FourthTask.Data.Abstractions;
using FourthTask.Buisness.Services;
using FourthTask.Core.Abstractions;

namespace FourthTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromHours(0.5);
                    options.AccessDeniedPath = new PathString("/User/Authentication");
                    options.LoginPath = new PathString("/User/Authentication");
                });

            var connectionString = builder.Configuration.GetConnectionString("Default"); 
            //dependency Injection DataBase
            builder.Services.AddDbContext<FourthTaskContext>(optionsBuilder => optionsBuilder.UseSqlServer(connectionString));

            //dependency Injection AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            ////Dependency Injection Services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IStatusService, StatusService>(); 

            ////Dependency Injection GenericRepository
            builder.Services.AddScoped<IRepository<User>, Repository<User>>();
            builder.Services.AddScoped<IRepository<Status>, Repository<Status>>();
             
            //Dependency Injection UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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


            app.UseAuthentication(); // Set HttpContex.User
            app.UseAuthorization();  // Check Users Succes 


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
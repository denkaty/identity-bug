
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AuthTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<UniversityDbContext>(options => options.UseSqlServer("Server=.\\SQLEXPRESS;Database=auth-test;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"));
            builder.Services.AddDataProtection();

            //builder.Services.AddScoped<IUserStore<StaffUser>, UserStore<StaffUser, StaffRole, UniversityDbContext, string, StaffUserClaim, StaffUserRole, StaffUserLogin, StaffUserToken, StaffRoleClaim>>();
            //builder.Services.AddScoped<IRoleStore<StaffRole>, RoleStore<StaffRole, UniversityDbContext, string, StaffUserRole, StaffRoleClaim>>();

            builder.Services
                .AddIdentityCore<StaffUser>()
                .AddRoles<StaffRole>()
                .AddEntityFrameworkStores<UniversityDbContext>()
                .AddDefaultTokenProviders();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

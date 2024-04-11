using Demo_BLL.Interfaces;
using Demo_BLL.Repositories;
using Demo_DAL.Data;
using Demo_DAL.Models;
using Demo_PL.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_PL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(m=> m.AddProfile(new MappingPorfiles()));

            //services.AddScoped<UserManager<ApplicationUser>>();
            //services.AddScoped<SignInManager<ApplicationUser>>();
            //services.AddScoped<RoleManager<IdentityRole>>();

            services.AddIdentity<ApplicationUser, IdentityRole>(IdentityOptions =>
            {
                IdentityOptions.Password.RequiredUniqueChars = 2;
                IdentityOptions.Password.RequireDigit = true;
                IdentityOptions.Password.RequireNonAlphanumeric = true;
                IdentityOptions.Password.RequireUppercase = true;
                IdentityOptions.Password.RequireLowercase = true;
                IdentityOptions.Password.RequiredLength = 5;

                IdentityOptions.Lockout.AllowedForNewUsers = true;
                IdentityOptions.Lockout.MaxFailedAccessAttempts = 5;
                IdentityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);

                //IdentityOptions.User.AllowedUserNameCharacters = "1-9 a-z";
                IdentityOptions.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<ApplicationDbContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_Group7_demo_BLL.Services.Abstraction;
using MVC_Group7_demo_BLL.Services.Implementation;
using MVC_Group7_demo_DAL.DataBase;
using MVC_Group7_demo_DAL.Entities;
using MVC_Group7_demo_DAL.Repository.Abstraction;
using MVC_Group7_demo_DAL.Repository.Implementation;
using System.Threading.Tasks;

namespace MVC_Group7_demo_PLL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var connectionString = builder.Configuration.GetConnectionString("defaultConnection");

            builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));

            builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddCookie(IdentityConstants.ApplicationScheme, options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<Context>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

            builder.Services.AddScoped<IAdminRepo, AdminRepo>();
            builder.Services.AddScoped<ICartRepo, CartRepo>();
            builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
            builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
            builder.Services.AddScoped<IDeliveryRepo, DeliveryRepo>();
            builder.Services.AddScoped<IOrdersRepo, OrdersRepo>();
            builder.Services.AddScoped<IProductCartRepo, ProductCartRepo>();
            builder.Services.AddScoped<IProductOrderRepo, ProductOrderRepo>();
            builder.Services.AddScoped<IProductRepo, ProductRepo>();

            //builder.Services.AddScoped<IAdminServices, AdminServices>();
            builder.Services.AddScoped<ICartServices, CartServices>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICustomerServices, CustomerServices>();
            //builder.Services.AddScoped<IDeliveryServices, DeliveryServices>();
            builder.Services.AddScoped<IOrdersServices, OrdersServices>();
            //builder.Services.AddScoped<IProductCartServices, ProductCartServices>();
            builder.Services.AddScoped<IProductOrderServices, ProductOrderServices>();
            builder.Services.AddScoped<IProductService, ProductService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            using(var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "Admin", "Customer"};


                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

            }

            app.Run();

            
        }
    }
}

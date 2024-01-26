using Microsoft.EntityFrameworkCore;
using SignalR_Demo.Hubs;
using SignalR_Demo.MiddlewareExtensions;
using SignalR_Demo.SubscribeTableDependencies;

namespace SignalR_Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("ProductDbContext") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddControllersWithViews();
            builder.Services.AddSignalR();
            builder.Services.AddSingleton<Dashboard_Push_Notification_Hub>();
            builder.Services.AddSingleton<Product_Push_Notification_Hub>();
            builder.Services.AddSingleton<SubscribeProductTableDependency>();

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

            app.MapHub<ChatHub>("/chatHub");
            app.MapHub<Dashboard_Push_Notification_Hub>("/dashboard_push_notification_hub");
            app.MapHub<Product_Push_Notification_Hub>("/product_push_notification_hub");
            app.UseSqlTableDependency<SubscribeProductTableDependency>(connectionString);

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

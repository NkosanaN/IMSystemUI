using IMSystemUI.Service.Interfaces;
using IMSystemUI.Service.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IMSystemUI.UI.Extension;
public static class ServicesApplication
{
    public static IServiceCollection ConfigureService(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                //options.LoginPath = "/Account/Login";
                options.LoginPath = "/Home/DashboardData";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.Cookie.Name = "IMSystemUI";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
            });

      

        services.AddDistributedMemoryCache();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(1000);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        services.AddHttpContextAccessor();

       // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddHttpClient<IUserService, UserService>();
        services.AddHttpClient<ISupplierService, SupplierService>();
        services.AddHttpClient<IItemService, ItemService>();
        services.AddHttpClient<IItemEmployeeAssignmentService, ItemEmployeeAssignmentService>();
        services.AddHttpClient<IDepartmentService, DepartmentService>();
        services.AddHttpClient<IShelveTypeService, ShelveTypeService>();

        services.AddControllersWithViews();
        return services;
    }
}


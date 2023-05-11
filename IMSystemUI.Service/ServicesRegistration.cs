using IMSystemUI.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using IMSystemUI.Service.Helpers;
using IMSystemUI.Service.Repository;

namespace IMSystemUI.Service
{
    public static class ServicesRegistration
    {
        public static IServiceCollection ConfigureService(this IServiceCollection srv)
        {
            srv.AddSingleton<IHttpClientExtensions, HttpClientExtensions>();
            srv.AddSingleton<ISupplierService, SupplierService>();
            return srv;
        }
    }
}

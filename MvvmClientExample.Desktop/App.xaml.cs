using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvvmClientExample.Application.Services;
using MvvmClientExample.DataAccess.Data;
using MvvmClientExample.Domain.Services;
using MvvmClientExample.ViewModels;

namespace MvvmClientExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(BuildConfiguration());
        }

        private static ServiceProvider BuildConfiguration()
        {
            var services = new ServiceCollection();

            services.AddDbContext<AuthDbContext>(opt =>
            {
                opt.UseSqlite("Data Source = MvvmClientExample.db");
            });

            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IUserService, UserService>();

            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegistrationViewModel>();
            services.AddTransient<OptionsViewModel>();
            services.AddTransient<PasswordRestoreViewModel>();
            services.AddTransient<MailVerificationViewModel>();

            return services.BuildServiceProvider();
        }
    }
}

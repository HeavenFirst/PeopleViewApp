using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PeopleViewApp.Services;
using PeopleViewApp.Services.Interfaces;
using PeopleViewApp.Settings;
using PeopleViewApp.Stores;
using PeopleViewApp.ViewModels;
using System.IO;
using System.Windows;

namespace PeopleViewApp
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
            NavigationStore navigationStore = new();

            navigationStore.CurrentViewModel = new HomeViewModel(navigationStore);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>
                (Configuration.GetSection(nameof(AppSettings)));

            services.AddScoped<IUsersApi, UsersApi>();
            services.AddTransient(typeof(MainWindow));
        }
    }

}

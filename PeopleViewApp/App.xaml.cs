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
        public IServiceProvider _serviceProvider;

        public IConfiguration Configuration { get; private set; }

        public App()
        {           
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            NavigationStore navigationStore = new();

            navigationStore.CurrentViewModel = new HomeViewModel(navigationStore, _serviceProvider.GetRequiredService<IUsersApi>());

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private void ConfigureServices(IServiceCollection serviceCollection)
        {
            var service = new ServiceCollection();

            service.Configure<AppSettings>
                (Configuration.GetSection(nameof(AppSettings)));

            service.AddScoped<IUsersApi, UsersApi>();

            service.AddHttpClient();

            service.AddTransient(typeof(HomeViewModel));

            service.AddSingleton<NavigationStore>();

            _serviceProvider = service.BuildServiceProvider();
        }
    }

}

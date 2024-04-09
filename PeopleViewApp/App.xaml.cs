using PeopleViewApp.Stores;
using PeopleViewApp.ViewModels;
using System.Windows;

namespace PeopleViewApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore navigationStore = new();

            navigationStore.CurrentViewModel = new HomeViewModel(navigationStore);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}

using System.Configuration;
using System.Data;
using System.Windows;
using WPF_Final_Project.Accounts;

namespace WPF_Final_Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Prevent the application from shutting down when the MainWindow is closed
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }
    }
}

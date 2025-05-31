using System.Windows;

namespace Zasobowo.Client
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown; // <== KLUCZOWA ZMIANA

            var loginWindow = new LoginWindow();
            bool? result = loginWindow.ShowDialog();

            if (result == true)
            {
                var mainWindow = new MainWindow();
                this.MainWindow = mainWindow;
                mainWindow.Show();
            }
            else
            {
                Shutdown();
            }
        }
    }
}

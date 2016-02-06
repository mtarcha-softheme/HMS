using System.Windows;
using System.Windows.Threading;

namespace Client
{
    public class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "HMS", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}

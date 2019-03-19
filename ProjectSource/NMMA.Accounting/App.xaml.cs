using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NMMA.Accounting
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            // Application is running 
            // Process command line args 
            string startMode = "";            
            for (int i = 0; i != e.Args.Length; ++i)
            {
                    startMode = e.Args[i];              
            }

            // Create main application window, starting minimized if specified
            MainWindow mainWindow = new MainWindow();
            if (startMode != "")
            {
                mainWindow.WindowState = WindowState.Minimized;
                mainWindow.ConsoleExec(startMode);
            }
            else
            {
                mainWindow.Show();
            }
            
        }
    }
}

using System.Windows;
using TimerSK.Tools;

namespace TimerSK
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
       
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if(new NetTest().IsInternetAvailable())
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
            }
            else
            {
                NoInternet noInternet = new NoInternet();
                noInternet.Show();
            }

            
        }

    }
}

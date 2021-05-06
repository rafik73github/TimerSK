using System.Windows;
using TimerSK.Tools;
using TimerSK.Models;
using System.Windows.Input;

namespace TimerSK
{
    /// <summary>
    /// Logika interakcji dla klasy LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string password;
        public LoginWindow()
        {
            InitializeComponent();
            PassBox.Focus();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            password = PassBox.Password;

            if(new HTTPRequest().CheckToken(password))
            {
                HASH.HASH_VALUE = new Security().GetHashString(password);
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();

            }
            else
            {
                MessageBox.Show("NIEPRAWIDŁOWE HASŁO !");
            }
        }

        private void PassOnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                password = PassBox.Password;

                if (new HTTPRequest().CheckToken(password))
                {
                    HASH.HASH_VALUE = new Security().GetHashString(password);
                    MainWindow mainWindow = new MainWindow();
                    this.Close();
                    mainWindow.Show();

                }
                else
                {
                    MessageBox.Show("NIEPRAWIDŁOWE HASŁO !");
                }
            }
        }
    }
}

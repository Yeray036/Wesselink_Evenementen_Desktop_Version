using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wesselink_Evenementen_Desktop_Version.Classes;

namespace Wesselink_Evenementen_Desktop_Version.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {

        UsersConfig UsersConfig = new UsersConfig();
        public static bool LoginAccepted { get; set; }
        
        public Login()
        {
            InitializeComponent();
        }

        private void LoginHandlerBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                ErrorText.Text = "";
                LoginAccepted = false;
                if (UsernameInput.Text != String.Empty && PasswordInput.Password != String.Empty)
                {
                    if (UsersConfig.ValidatePassword(UsernameInput.Text, PasswordInput.Password) == true)
                    {
                        UsersConfig.GetAccountDetails(UsersConfig.Id);
                        UsernameInput.Visibility = Visibility.Hidden;
                        PasswordInput.Visibility = Visibility.Hidden;
                        LoginBtnLayout.Visibility = Visibility.Hidden;
                        LoginText.Text = $"Welkom {AccountDetails.Name}";
                        MainWindow.mainWindow.MainLoginBtn.Visibility = Visibility.Hidden;
                        MainWindow.mainWindow.MainRegisterBtn.Visibility = Visibility.Hidden;
                        MainWindow.mainWindow.MainLogoutBtn.Visibility = Visibility.Visible;
                        MainWindow.mainWindow.MainWelkomUserText.Text = "Welkom " + AccountDetails.Name;
                        LoginAccepted = true;
                    }
                    else
                    {
                        ErrorText.Text = "Foutmelding: controleer naam/wachtwoord!";
                        LoginAccepted = false;
                    }
                }
                else
                {
                    ErrorText.Text = "Foutmelding: verplichten velden zijn leeg";
                    LoginAccepted = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

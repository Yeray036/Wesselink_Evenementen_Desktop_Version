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
using Wesselink_Evenementen_Desktop_Version.Pages;

namespace Wesselink_Evenementen_Desktop_Version
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Login loginPage = new Login();
        public static MainWindow mainWindow { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
            mainWindow.MainLogoutBtn.Visibility = Visibility.Hidden;
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            this.ResizeMode = ResizeMode.NoResize;
            this.DragMove();
            this.ResizeMode = ResizeMode.CanResizeWithGrip;
        }

        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void MinimizeWindow(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void OpenAccountsPage(object sender, RoutedEventArgs e)
        {
            BackgroundImage.Source = null;
            Accounts accountsPage = new Accounts();
            MainFrame.Navigate(accountsPage);
        }

        private void LoginPageBtn(object sender, RoutedEventArgs e)
        {
            BackgroundImage.Source = null;
            MainFrame.Navigate(loginPage);
        }

        private void HomePageBtn(object sender, RoutedEventArgs e)
        {
            BackgroundImage.Source = null;
            BackgroundImage.Source = new BitmapImage(new Uri("https://billetto.co.uk/blog/wp-content/uploads/2019/04/hanny-naibaho-388579-unsplash-e1554461063517.jpg", UriKind.Absolute));
            MainFrame.Content = null;
        }

        private void LogoutBtn(object sender, RoutedEventArgs e)
        {
            mainWindow.MainLoginBtn.Visibility = Visibility.Visible;
            mainWindow.MainRegisterBtn.Visibility = Visibility.Visible;
            mainWindow.MainLogoutBtn.Visibility = Visibility.Hidden;
            mainWindow.MainWelkomUserText.Text = "";
            loginPage.LoginText.Text = "Login";
            loginPage.UsernameInput.Visibility = Visibility.Visible;
            loginPage.PasswordInput.Visibility = Visibility.Visible;
            loginPage.LoginBtnLayout.Visibility = Visibility.Visible;
            mainWindow.MainFrame.Navigate(loginPage);
            Login.LoginAccepted = false;
            UsersConfig.Id = 0;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LWin || e.Key == Key.RWin)
            {
                this.ResizeMode = ResizeMode.NoResize;
            }
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LWin || e.Key == Key.RWin)
            {
                this.ResizeMode = ResizeMode.CanResizeWithGrip;
            }
        }
    }
}

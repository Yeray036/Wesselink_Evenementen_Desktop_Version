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

namespace Wesselink_Evenementen_Desktop_Version.UserControls
{
    /// <summary>
    /// Interaction logic for ManageAccount.xaml
    /// </summary>
    public partial class ManageAccount : UserControl
    {
        UsersConfig UsersConfig = new UsersConfig();
        public bool AccountChanged { get; set; }

        public ManageAccount()
        {
            InitializeComponent();
            if (Login.LoginAccepted == true)
            {
                FillAccountDetails();
            }
            else
            {
                MessageBox.Show("Niet ingelogd");
            }
        }

        public void FillAccountDetails()
        {
            try
            {
                if (UsersConfig.GetAccountDetails(UsersConfig.Id).Any() && UsersConfig.Id != 0)
                {
                    AccountName.Text = AccountDetails.Name;
                    AccountSurname.Text = AccountDetails.Surname;
                    AccountEmail.Text = AccountDetails.Email;
                    AccountPhoneNumber.Text = AccountDetails.PhoneNumber;
                    if (AccountDetails.Barkeeper == "1")
                    {
                        BarKeeperCheckbox.IsChecked = true;
                    }
                    else
                    {
                        BarKeeperCheckbox.IsChecked = false;
                    }
                    if (AccountDetails.Receptionist == "1")
                    {
                        ReceptionistCheckbox.IsChecked = true;
                    }
                    else
                    {
                        ReceptionistCheckbox.IsChecked = false;
                    }
                    if (AccountDetails.Waiter == "1")
                    {
                        WaiterCheckbox.IsChecked = true;
                    }
                    else
                    {
                        WaiterCheckbox.IsChecked = false;
                    }
                    if (AccountDetails.Host == "1")
                    {
                        HostCheckbox.IsChecked = true;
                    }
                    else
                    {
                        HostCheckbox.IsChecked = false;
                    }
                    if (AccountDetails.ProfilePicture != String.Empty)
                    {
                        PictureInput.Source = new BitmapImage(new Uri(AccountDetails.ProfilePicture, UriKind.Absolute));
                    }
                    else
                    {
                        PictureInput.Source = new BitmapImage(new Uri("https://www.renaissancecapital.com/Content/Images/No-Profile-Found.png", UriKind.Absolute));
                    }
                    AccountChanged = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AccountResetBtn(object sender, RoutedEventArgs e)
        {
            FillAccountDetails();
        }

        private void AccountSendBtn(object sender, RoutedEventArgs e)
        {
            if (AccountChanged == true)
            {
                AccountDetails.Name = AccountName.Text;
                AccountDetails.Surname = AccountSurname.Text;
                AccountDetails.Email = AccountEmail.Text;
                AccountDetails.PhoneNumber = AccountPhoneNumber.Text;
                if (BarKeeperCheckbox.IsChecked == true)
                {
                    AccountDetails.Barkeeper = "1";
                }
                else
                {
                    AccountDetails.Barkeeper = "0";
                }
                if (ReceptionistCheckbox.IsChecked == true)
                {
                    AccountDetails.Receptionist = "1";
                }
                else
                {
                    AccountDetails.Receptionist = "0";
                }
                if (WaiterCheckbox.IsChecked == true)
                {
                    AccountDetails.Waiter = "1";
                }
                else
                {
                    AccountDetails.Waiter = "0";
                }
                if (HostCheckbox.IsChecked == true)
                {
                    AccountDetails.Host = "1";
                }
                else
                {
                    AccountDetails.Host = "0";
                }
                UsersConfig.SendNewAccountDetails();
                MainWindow.mainWindow.MainWelkomUserText.Text = "Welkom " + AccountDetails.Name;
            }
            else
            {
                MessageBox.Show("Geen aanpassingen gedaan");
            }
        }

        private void NameChanged(object sender, TextChangedEventArgs e)
        {
            AccountChanged = true;
        }

        private void SurnameChanged(object sender, TextChangedEventArgs e)
        {
            AccountChanged = true;
        }

        private void EmailChanged(object sender, TextChangedEventArgs e)
        {
            AccountChanged = true;
        }

        private void PhoneNumberChanged(object sender, TextChangedEventArgs e)
        {
            AccountChanged = true;
        }

        private void BarkeeperChecked(object sender, RoutedEventArgs e)
        {
            AccountChanged = true;
        }

        private void BarkeeperUnchecked(object sender, RoutedEventArgs e)
        {
            AccountChanged = true;
        }

        private void ReceptionistUnchecked(object sender, RoutedEventArgs e)
        {
            AccountChanged = true;
        }

        private void ReceptionistChecked(object sender, RoutedEventArgs e)
        {
            AccountChanged = true;
        }

        private void WaiterChecked(object sender, RoutedEventArgs e)
        {
            AccountChanged = true;
        }

        private void WaiterUnchecked(object sender, RoutedEventArgs e)
        {
            AccountChanged = true;
        }

        private void HostUnchecked(object sender, RoutedEventArgs e)
        {
            AccountChanged = true;
        }

        private void HostChecked(object sender, RoutedEventArgs e)
        {
            AccountChanged = true;
        }
    }
}

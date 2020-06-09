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

namespace Wesselink_Evenementen_Desktop_Version.UserControls
{
    /// <summary>
    /// Interaction logic for ManageAccount.xaml
    /// </summary>
    public partial class ManageAccount : UserControl
    {
        UsersConfig UsersConfig = new UsersConfig();

        public ManageAccount()
        {
            InitializeComponent();
            UsersConfig.ValidatePassword("Yeray", "Welkom2020!");
            FillAccountDetails();
        }

        public void FillAccountDetails()
        {
            if (UsersConfig.GetAccountDetails(UsersConfig.Id).Any())
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
            }
        }
    }
}

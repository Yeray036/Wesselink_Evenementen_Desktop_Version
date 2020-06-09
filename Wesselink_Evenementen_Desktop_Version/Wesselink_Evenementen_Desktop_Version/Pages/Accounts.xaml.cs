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
using Wesselink_Evenementen_Desktop_Version.UserControls;

namespace Wesselink_Evenementen_Desktop_Version.Pages
{
    /// <summary>
    /// Interaction logic for Accounts.xaml
    /// </summary>
    public partial class Accounts : Page
    {
        public Accounts()
        {
            InitializeComponent();
        }

        private void SearchEmployeesBtn(object sender, RoutedEventArgs e)
        {
            SearchEmployees searchEmployees = new SearchEmployees();
            AccountWrapPanel.Children.Add(searchEmployees);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for SearchEmployees.xaml
    /// </summary>
    public partial class SearchEmployees : UserControl
    {
        UsersConfig UsersConfig = new UsersConfig();

        public SearchEmployees()
        {
            InitializeComponent();
            UsersConfig.GetAllEmployeesBasedOnZipCode();
            SearchEmployeesGrid.ItemsSource = UsersConfig.ZipCodeEmployees.DefaultView;
            DataGridTextColumn functie = new DataGridTextColumn();
            functie.Header = "Functie";
            functie.Binding = new Binding("Functie");
            SearchEmployeesGrid.Columns.Add(functie);
            functie.Binding = new Binding("Functie");
            //SearchEmployeesGrid.Items.Add(new EmployeeFunction { Functie = $"Barkeeper, Ober, {Environment.NewLine} Receptionist, Gastheer/vrouw"});

        }
        public struct EmployeeFunction
        {
            public string Functie { set; get; }
        }

    }
}

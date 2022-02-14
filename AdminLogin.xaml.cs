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
using System.Windows.Shapes;

namespace VendingMachine
{
    public partial class AdminLogin : Window
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text == "admin" && password.Password == "admin")
            {
                AdminPanel adminPanel = new AdminPanel();
                adminPanel.ShowDialog();
                Close();
            }
        }
    }
}

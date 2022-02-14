using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using VendingMachine.Models;
using VendingMachine.Repository;
using VendingMachine.Service;

namespace VendingMachine
{
    public partial class MainWindow : Window
    {
        private byte[] ByteImg;
        decimal Money = 0;
        List<VendingMachineCoins> Coins = new List<VendingMachineCoins>();
        List<VendingMachineDrinks> Drinks = new List<VendingMachineDrinks>();
        int VendingMachineId = 0;
        int oneCount = 0;
        int twoCount = 0;
        int fiveCount = 0;
        int tenCount = 0;

        public MainWindow()
        {
            InitializeComponent();

            BitmapImage img = new BitmapImage();
            img = new BitmapImage(new Uri("C:\\images\\cola.png"));

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(img));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                ByteImg = ms.ToArray();
            }

            SqlConnection cn = new SqlConnection("Data Source=DESKTOP-9SV6HT1\\SQLEXPRESS;Initial Catalog=VendingMachine;Integrated Security=true");
            cn.Open();
            SqlCommand cmd = new SqlCommand("update Drinks set Image = convert(varbinary(max), @img) where Id = 0", cn);
            cmd.Parameters.Add("@img", SqlDbType.Image, 0).Value = ByteImg;
            cmd.ExecuteNonQuery();
            cn.Close();

            GetVendingMachineAllDrinks(VendingMachineId);
            GetVendingMachineAllCoins(VendingMachineId);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        void GetVendingMachineAllDrinks(int id)
        {
            DrinksList.ItemsSource = Drinks = VendingMachineDrinksRepository.GetVendingMachineDrinksByVendingMachineId(VendingMachineRepository.GetVendingMachineById(id).Id).ToList();
            DrinksList.SelectedValuePath = "Id";
        }

        void GetVendingMachineAllCoins(int id)
        {
            Coins = VendingMachineCoinsRepository.GetVendingMachineCoinsByVendingMachineId(VendingMachineRepository.GetVendingMachineById(id).Id).OrderBy(c => c.Coins.Denominal).ToList();
            one.IsEnabled = Convert.ToBoolean(Coins[0].IsActive);
            two.IsEnabled = Convert.ToBoolean(Coins[1].IsActive);
            five.IsEnabled = Convert.ToBoolean(Coins[2].IsActive);
            ten.IsEnabled = Convert.ToBoolean(Coins[3].IsActive);
            oneCount = Coins[0].Count;
            twoCount = Coins[1].Count;
            fiveCount = Coins[2].Count;
            tenCount = Coins[3].Count;
        }

        private void Coin_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int keyword = Convert.ToInt32(btn.Content.ToString());
            Money += keyword;
            money.Content = Money + " руб.";
            VendingMachineCoins updateCoin = Coins.FirstOrDefault(c => c.Coins.Denominal == keyword);
            VendingMachineCoinsService.UpdateVendingMachineCoins(updateCoin.Id, updateCoin.Count + 1, updateCoin.IsActive);
        }

        private void Item_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int id = Convert.ToInt32(DrinksList.SelectedValue);
            VendingMachineDrinks Drink =  Drinks.FirstOrDefault(d => d.Id == id);
            if (Money >= Drink.Drinks.Cost && Drink.Count > 0)
            {
                Money -= Drink.Drinks.Cost;
                money.Content = Money + " руб.";
                VendingMachineDrinksService.UpdateVendingMachineDrinks(id, Drink.Count - 1);
                Drink.Count -= 1;
            }
            if (Drink.Count == 0)
            {
                Drink.IsActive = false;
                DrinksList.Items.Refresh();
            }
            return;
        }

        private void cash_Click(object sender, RoutedEventArgs e)
        {
            for (; tenCount > 0 && Money >= 10;)
            {
                Money -= 10;
                tenCount -= 1;
            }
            Coins[3].Count = tenCount;
            for (; fiveCount > 0 && Money >= 5;)
            {
                Money -= 5;
                fiveCount -= 1;
            }
            Coins[2].Count = fiveCount;
            for (; twoCount > 0 && Money >= 2;)
            {
                Money -= 2;
                twoCount -= 1;
            }
            Coins[1].Count = twoCount;
            for (; oneCount > 0 && Money >= 1;)
            {
                Money -= 1;
                oneCount -= 1;
            }
            Coins[0].Count = oneCount;
            money.Content = Money + " руб.";
            VendingMachineCoinsService.SaveVendingMachineCoins(Coins, VendingMachineId);
        }

        private void admin_Click(object sender, RoutedEventArgs e)
        {
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.ShowDialog();
            GetVendingMachineAllDrinks(VendingMachineId);
            GetVendingMachineAllCoins(VendingMachineId);
        }
    }
}

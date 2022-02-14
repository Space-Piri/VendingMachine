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
using VendingMachine.Models;
using VendingMachine.Repository;
using VendingMachine.Service;

namespace VendingMachine
{
    public partial class AdminPanel : Window
    {
        List<VendingMachineCoins> Coins = new List<VendingMachineCoins>();
        List<VendingMachineDrinks> Drinks = new List<VendingMachineDrinks>();
        int VendingMachineId = 0;

        public AdminPanel()
        {
            InitializeComponent();
            GetVendingMachineAllDrinks(VendingMachineId);
            GetVendingMachineAllCoins(VendingMachineId);
        }

        void GetVendingMachineAllDrinks(int id)
        {
            Drinks = VendingMachineDrinksRepository.GetVendingMachineDrinksByVendingMachineId(VendingMachineRepository.GetVendingMachineById(id).Id).ToList();
            Drinks.Add(new VendingMachineDrinks(-66, VendingMachineId, new Drinks(-1,"Добавить", null, 0), 1, true));
            DrinksList.ItemsSource = Drinks;
            DrinksList.SelectedValuePath = "Id";
            DrinksList.Items.Refresh();
        }

        void GetVendingMachineAllCoins(int id)
        {
            Coins = VendingMachineCoinsRepository.GetVendingMachineCoinsByVendingMachineId(VendingMachineRepository.GetVendingMachineById(id).Id).OrderBy(c => c.Coins.Denominal).ToList();
            oneText.Text = Coins[0].Count.ToString();
            oneCheck.IsChecked = Convert.ToBoolean(Coins[0].IsActive);
            twoText.Text = Coins[1].Count.ToString();
            twoCheck.IsChecked = Convert.ToBoolean(Coins[1].IsActive);
            fiveText.Text = Coins[2].Count.ToString();
            fiveCheck.IsChecked = Convert.ToBoolean(Coins[2].IsActive);
            tenText.Text = Coins[3].Count.ToString();
            tenCheck.IsChecked = Convert.ToBoolean(Coins[3].IsActive);
        }

        private void coinsSave_Click(object sender, RoutedEventArgs e)
        {
            Coins[0].Count = Convert.ToInt32(oneText.Text);
            Coins[0].IsActive = Convert.ToInt32(oneCheck.IsChecked);
            Coins[1].Count = Convert.ToInt32(twoText.Text);
            Coins[1].IsActive = Convert.ToInt32(twoCheck.IsChecked);
            Coins[2].Count = Convert.ToInt32(fiveText.Text);
            Coins[2].IsActive = Convert.ToInt32(fiveCheck.IsChecked);
            Coins[3].Count = Convert.ToInt32(tenText.Text);
            Coins[3].IsActive = Convert.ToInt32(tenCheck.IsChecked);
            VendingMachineCoinsService.SaveVendingMachineCoins(Coins, VendingMachineId);
        }

        private void DrinksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = Convert.ToInt32(DrinksList.SelectedValue);
            if (id != -66)
            {
                return;
            }
            DrinkAdd drinkAdd = new DrinkAdd();
            drinkAdd.SetID(VendingMachineId);
            if (id != -66)
            {
                drinkAdd.SetDrink(VendingMachineDrinksRepository.GetVendingMachineDrinksById(id));
            }
            drinkAdd.ShowDialog();
            GetVendingMachineAllDrinks(VendingMachineId);
            DrinksList.Items.Refresh();
        }

        private void StackPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int id = Convert.ToInt32(DrinksList.SelectedValue);
            DrinkAdd drinkAdd = new DrinkAdd();
            drinkAdd.SetID(VendingMachineId);
            if (id != -66)
            {
                drinkAdd.SetDrink(VendingMachineDrinksRepository.GetVendingMachineDrinksById(id));
            }
            drinkAdd.ShowDialog();
            GetVendingMachineAllDrinks(VendingMachineId);
            DrinksList.Items.Refresh();
        }
    }
}

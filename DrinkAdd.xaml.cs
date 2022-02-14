using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using VendingMachine.Models;
using VendingMachine.Repository;
using VendingMachine.Service;

namespace VendingMachine
{
    public partial class DrinkAdd : Window
    {
        bool newDrink = true;
        BitmapImage img = new BitmapImage();
        int VendingMachineId = 0;
        int DrinkId = 0;

        public DrinkAdd()
        {
            InitializeComponent();
            Title = "Добавление напитка";
        }

        public void SetDrink(VendingMachineDrinks drink)
        {
            newDrink = false;
            image.Source = drink.Drinks.Image;
            costText.Text = drink.Drinks.Cost.ToString();
            countText.Text = drink.Count.ToString();
            name.Text = drink.Drinks.Name.ToString();
            DrinkId = drink.Id;
            Title = "Изменение напитка";
        }

        public void SetID(int id)
        {
            VendingMachineId = id;
        }

        private void fileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            string path = "";
            if (openFile.ShowDialog() == true)
            {
                path = openFile.FileName;
            }
            img = new BitmapImage(new Uri(path));
            image.Source = img;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            byte[] ByteImg;
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(img));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                ByteImg = ms.ToArray();
            }
            if (newDrink)
            {
                Drinks newDrinks = new Drinks(0, name.Text, ByteImg, Convert.ToDecimal(costText.Text));
                DrinksService.AddDrinks(newDrinks);
                VendingMachineDrinksService.AddVendingMachineDrinks(new VendingMachineDrinks(0, VendingMachineId, DrinksRepository.GetDrinksByName(name.Text), Convert.ToInt32(countText.Text), true));
            }
            else
            {
                int id = VendingMachineDrinksRepository.GetVendingMachineDrinksById(DrinkId).Drinks.Id;
                DrinksService.UpdateDrinks(id, name.Text, ByteImg, Convert.ToInt32(costText.Text));
                VendingMachineDrinksService.UpdateVendingMachineDrinks(DrinkId, Convert.ToInt32(countText.Text));
            }
            Close();
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            if (newDrink)
            {
                Close();
            }
            else
            {
                int id = VendingMachineDrinksRepository.GetVendingMachineDrinksById(DrinkId).Drinks.Id;
                VendingMachineDrinksService.DeleteVendingMachineDrinks(DrinkId);
                DrinksService.DeleteDrinks(id);
            }
            Close();
        }
    }
}

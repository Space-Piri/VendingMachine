using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Models;

namespace VendingMachine.Repository
{
    public class VendingMachineDrinksRepository : Repository
    {
        public static VendingMachineDrinks GetVendingMachineDrinksById(int Id)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            SqlCommand cmd = new SqlCommand("select * from VendingMachineDrinks where Id = " + Id, cn);
            var reader = cmd.ExecuteReader();
            VendingMachineDrinks vendingMachineDrinks = null;
            while (reader.Read())
            {
                vendingMachineDrinks = new VendingMachineDrinks(Convert.ToInt32(reader["Id"]), Convert.ToInt32(reader["VendingMachineId"]), DrinksRepository.GetDrinksById(Convert.ToInt32(reader["DrinksId"])), Convert.ToInt32(reader["Count"]), (Convert.ToInt32(reader["Count"]) > 0));
            }
            cn.Close();
            return vendingMachineDrinks;
        }
        public static List<VendingMachineDrinks> GetVendingMachineDrinksByVendingMachineId(int VendingMachineId)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            SqlCommand cmd = new SqlCommand("select * from VendingMachineDrinks where VendingMachineId = " + VendingMachineId, cn);
            var reader = cmd.ExecuteReader();
            List<VendingMachineDrinks> vendingMachineDrinks = new List<VendingMachineDrinks>();
            while (reader.Read())
            {
                vendingMachineDrinks.Add(new VendingMachineDrinks(Convert.ToInt32(reader["Id"]), Convert.ToInt32(reader["VendingMachineId"]), DrinksRepository.GetDrinksById(Convert.ToInt32(reader["DrinksId"])), Convert.ToInt32(reader["Count"]), (Convert.ToInt32(reader["Count"]) > 0)));
            }
            cn.Close();
            return vendingMachineDrinks;
        }
    }
}

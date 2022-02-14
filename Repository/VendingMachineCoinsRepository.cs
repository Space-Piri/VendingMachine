using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Models;

namespace VendingMachine.Repository
{
    public class VendingMachineCoinsRepository : Repository
    {
        public static VendingMachineCoins GetVendingMachineCoinsById(int Id)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            SqlCommand cmd = new SqlCommand("select * from VendingMachineCoins where Id = " + Id, cn);
            var reader = cmd.ExecuteReader();
            VendingMachineCoins vendingMachineDrinks = null;
            while (reader.Read())
            {
                vendingMachineDrinks = new VendingMachineCoins(Convert.ToInt32(reader["Id"]), Convert.ToInt32(reader["VendingMachineId"]), CoinsRepository.GetCoinsById(Convert.ToInt32(reader["CoinsId"])), Convert.ToInt32(reader["Count"]), Convert.ToInt32(reader["IsActive"]));
            }
            cn.Close();
            return vendingMachineDrinks;
        }
        public static List<VendingMachineCoins> GetVendingMachineCoinsByVendingMachineId(int VendingMachineId)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            SqlCommand cmd = new SqlCommand("select * from VendingMachineCoins where VendingMachineId = " + VendingMachineId, cn);
            var reader = cmd.ExecuteReader();
            List<VendingMachineCoins> vendingMachineCoins = new List<VendingMachineCoins>();
            while (reader.Read())
            {
                vendingMachineCoins.Add(new VendingMachineCoins(Convert.ToInt32(reader["Id"]), Convert.ToInt32(reader["VendingMachineId"]), CoinsRepository.GetCoinsById(Convert.ToInt32(reader["CoinsId"])), Convert.ToInt32(reader["Count"]), Convert.ToInt32(reader["IsActive"])));
            }
            cn.Close();
            return vendingMachineCoins;
        }
    }
}

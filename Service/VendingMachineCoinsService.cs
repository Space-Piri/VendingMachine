using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Models;

namespace VendingMachine.Service
{
    public class VendingMachineCoinsService : Service
    {
        public static void UpdateVendingMachineCoins(int Id, int Count, int IsActive)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            SqlCommand cmd = new SqlCommand("update VendingMachineCoins set Count = " + Count + ", IsActive = " + IsActive + " where Id = " + Id, cn);
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static void SaveVendingMachineCoins(List<VendingMachineCoins> Coins, int VendingMachineId)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            for (int i = 0; i < 4; i++)
            {
                SqlCommand cmd = new SqlCommand("update VendingMachineCoins set Count = " + Coins[i].Count + ", IsActive = " + Coins[i].IsActive + " where Id = " + Coins[i].Id, cn);
                cmd.ExecuteNonQuery();
            }
            cn.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Models;

namespace VendingMachine.Service
{
    public class VendingMachineDrinksService : Service
    {
        public static void UpdateVendingMachineDrinks(int Id, int Count)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            SqlCommand cmd = new SqlCommand("update VendingMachineDrinks set Count = " + Count + " where Id = " + Id, cn);
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static void AddVendingMachineDrinks(VendingMachineDrinks newDrink)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            SqlCommand cmd = new SqlCommand("insert into VendingMachineDrinks values("+newDrink.VendingMachineId+", "+newDrink.Drinks.Id+" ,"+newDrink.Count+")", cn);
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static void DeleteVendingMachineDrinks(int id)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            SqlCommand cmd = new SqlCommand("delete from VendingMachineDrinks where Id = " + id, cn);
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}

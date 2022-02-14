using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Repository
{
    public class VendingMachineRepository : Repository
    {
        public static Models.VendingMachine GetVendingMachineById(int Id)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            SqlCommand cmd = new SqlCommand("select * from VendingMachines where Id = " + Id, cn);
            var reader = cmd.ExecuteReader();
            Models.VendingMachine vendingMachine = null;
            while (reader.Read())
            {
                vendingMachine = new Models.VendingMachine(Convert.ToInt32(reader["Id"]), Convert.ToString(reader["SecretCode"]));
            }
            cn.Close();
            return vendingMachine;
        }
    }
}

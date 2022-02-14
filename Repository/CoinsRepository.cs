using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Models;

namespace VendingMachine.Repository
{
    public class CoinsRepository : Repository
    {
        public static Coins GetCoinsById(int Id)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            SqlCommand cmd = new SqlCommand("select * from Coins where Id = " + Id, cn);
            var reader = cmd.ExecuteReader();
            Coins coins = null;
            while (reader.Read()){
                 coins = new Coins(Convert.ToInt32(reader["Id"]), Convert.ToInt32(reader["Denominal"]));
            }
            cn.Close();
            return coins;
        }
    }
}

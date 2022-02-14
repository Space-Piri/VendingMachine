using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Models;

namespace VendingMachine.Repository
{
    public class DrinksRepository : Repository
    {
        public static Drinks GetDrinksById(int Id)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            SqlCommand cmd = new SqlCommand("select * from Drinks where Id = " + Id, cn);
            var reader = cmd.ExecuteReader();
            Drinks drinks = null;
            while (reader.Read())
            {
                drinks = new Drinks(Convert.ToInt32(reader["Id"]), Convert.ToString(reader["Name"]), (byte[])reader.GetValue(2), Convert.ToInt32(reader["Cost"]));
            }
            cn.Close();
            return drinks;
        }

        public static Drinks GetDrinksByName(string Name)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            SqlCommand cmd = new SqlCommand("select * from Drinks where Name = '" + Name + "'", cn);
            var reader = cmd.ExecuteReader();
            Drinks drinks = null;
            while (reader.Read())
            {
                drinks = new Drinks(Convert.ToInt32(reader["Id"]), Convert.ToString(reader["Name"]), (byte[])reader.GetValue(2), Convert.ToInt32(reader["Cost"]));
            }
            cn.Close();
            return drinks;
        }
    }
}

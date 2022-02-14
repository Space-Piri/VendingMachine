using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using VendingMachine.Models;

namespace VendingMachine.Service
{
    public class DrinksService : Service
    {
        public static void AddDrinks(Drinks newDrink)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(newDrink.Image));
            byte[] ByteImg;
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                ByteImg = ms.ToArray();
            }
            SqlCommand cmd = new SqlCommand("insert into Drinks values('"+ newDrink.Name + "', convert(varbinary(max), @img) ," + newDrink.Cost+")", cn);
            cmd.Parameters.Add("@img", SqlDbType.Image, 0).Value = ByteImg;
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static void UpdateDrinks(int Id, string Name, byte[] image, int Cost)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            SqlCommand cmd = new SqlCommand("update Drinks set Name = '" + Name + "', Image = convert(varbinary(max), @img), Cost = " + Cost + " where Id = " + Id, cn);
            cmd.Parameters.Add("@img", SqlDbType.Image, 0).Value = image;
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static void DeleteDrinks(int id)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            SqlCommand cmd = new SqlCommand("delete from Drinks where Id = " + id, cn);
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}

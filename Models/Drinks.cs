using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace VendingMachine.Models
{
    public class Drinks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BitmapImage Image { get; set; }
        public decimal Cost { get; set; }

        public Drinks(int Id, string Name, byte[] image, decimal Cost)
        {
            this.Id = Id;
            this.Name = Name;

            if (image != null)
            {
                using (var ms = new MemoryStream(image))
                {
                    var i = new BitmapImage();
                    i.BeginInit();
                    i.CacheOption = BitmapCacheOption.OnLoad;
                    i.StreamSource = ms;
                    i.EndInit();
                    this.Image = i;
                }
            }
            else
            {
                this.Image = null;
            }

            this.Cost = Cost;
        }
    }
}

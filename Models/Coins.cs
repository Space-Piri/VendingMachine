namespace VendingMachine.Models
{
    public class Coins
    {
        public int Id { get; set; }
        public int Denominal { get; set; }

        public Coins(int Id, int Denominal)
        {
            this.Id = Id;
            this.Denominal = Denominal;
        }
    }
}

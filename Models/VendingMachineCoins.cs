namespace VendingMachine.Models
{
    public class VendingMachineCoins
    {
        public int Id { get; set; }
        public int VendingMachineId { get; set; }
        public Coins Coins { get; set; }
        public int Count { get; set; }
        public int IsActive { get; set; }

        public VendingMachineCoins(int Id, int VendingMachineId, Coins Coins, int Count, int IsActive)
        {
            this.Id = Id;
            this.VendingMachineId = VendingMachineId;
            this.Coins = Coins;
            this.Count = Count;
            this.IsActive = IsActive;
        }
    }
}

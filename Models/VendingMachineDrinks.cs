namespace VendingMachine.Models
{
    public class VendingMachineDrinks
    {
        public int Id { get; set; }
        public int VendingMachineId { get; set; }
        public Drinks Drinks { get; set; }
        public int Count { get; set; }
        public bool IsActive { get; set; }

        public VendingMachineDrinks(int Id, int VendingMachineId, Drinks Drinks, int Count, bool IsActive)
        {
            this.Id = Id;
            this.VendingMachineId = VendingMachineId;
            this.Drinks = Drinks;
            this.Count = Count;
            this.IsActive = IsActive;
        }
    }
}

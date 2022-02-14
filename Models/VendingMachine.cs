namespace VendingMachine.Models
{
    public class VendingMachine
    {
        public int Id { get; set; }
        public string SecretCode { get; set; }

        public VendingMachine(int Id, string SecretCode)
        {
            this.Id = Id;
            this.SecretCode = SecretCode;
        }
    }
}

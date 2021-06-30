namespace TestEFE.Models
{
    public class HomePaket : Paket
    {
        public HomePaket()
        {
            PaketType = PaketType.Home;
        }
        public Address AddressInsuredObject { get; set; } = new Address();
    }
}

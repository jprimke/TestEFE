using System;

namespace TestEFE.Models
{
    public class TravelPaket : Paket
    {
        public TravelPaket()
        {
            PaketType = PaketType.Travel;
        }
        public Person InsuredPerson { get; set; } = new Person();

        public DateTime Birthday { get; set; }
    }
}

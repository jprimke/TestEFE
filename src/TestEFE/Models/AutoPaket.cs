using System;
using System.ComponentModel.DataAnnotations;

namespace TestEFE.Models
{
    public class AutoPaket : Paket
    {
        public AutoPaket()
        {
            PaketType = PaketType.Automotive;
        }

        [Required]
        [StringLength(20)]
        public string Vin { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string LicensePlate { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string Brand { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Model { get; set; } = string.Empty;

        public DateTime FirstRegistrationDate { get; set; }
    }
}


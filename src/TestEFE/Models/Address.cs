using System.ComponentModel.DataAnnotations;

namespace TestEFE.Models
{
    public class Address
    {
        [Required]
        [StringLength(150)]
        public string Street { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string Zip { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string CountryCode { get; set; } = string.Empty;
    }
}

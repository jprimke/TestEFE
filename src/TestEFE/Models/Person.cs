using System.ComponentModel.DataAnnotations;

namespace TestEFE.Models
{
    public class Person
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Salutation { get; set; }
    }
}

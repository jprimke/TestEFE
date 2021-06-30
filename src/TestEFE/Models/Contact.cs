using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestEFE.Models
{
    public class Contact
    {
        [StringLength(255)]
        public string? Email { get; set; }

        [StringLength(30)]
        public string? Phone { get; set; }
    }
}

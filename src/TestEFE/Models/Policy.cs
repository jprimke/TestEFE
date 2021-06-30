//-----------------------------------------------------------------------
// <copyright file="D:\Projekte\Bestand\Importer\Generic Data Import\Data\GenericDataImport.Model\Internal\Policy.cs" company="AXA Partners">
//     Author: Jörg H Primke
//     Copyright (c) 2019 - AXA Partners. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestEFE.Models
{
    public class Policy : IPolicyData, ITraceableData  
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string PolicyNumber { get; set; } = string.Empty;

        [StringLength(100)]
        public string? AdditionalPartForPolicyNumber { get; set; }

        public Person Person { get; set; } = new Person();

        public Address Address { get; set; } = new Address();

        public Contact Contact { get; set; } = new Contact();

        public string? AdditionalDataAsJSON { get; set; }

        public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset LastChanged { get; set; } = DateTimeOffset.Now;

        public bool Active { get; set; } = true;

        public int ContractId { get; set; }

        public virtual ICollection<Paket> Pakets { get; set; } = new HashSet<Paket>();

        [StringLength(30)]
        public string? CreatedBy { get; set; }

        [StringLength(30)]
        public string? ChangedBy { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; } = string.Empty;

        public int? ProductId { get; set; }

        [Required]
        public DateTime ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }
    }
}

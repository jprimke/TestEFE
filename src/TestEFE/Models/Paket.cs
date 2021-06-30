//-----------------------------------------------------------------------
// <copyright file="D:\Projekte\Bestand\Importer\Generic Data Import\Data\GenericDataImport.Model\Internal\Policy.cs" company="AXA Partners">
//     Author: Jörg H Primke
//     Copyright (c) 2019 - AXA Partners. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace TestEFE.Models
{
    public abstract class Paket : ITraceableData
    {
        protected Paket()
        {
        }

        public int Id { get; set; }

        public int PolicyId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        public DateTime ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        [StringLength(5000)]
        public string? AdditionalDataAsJSON { get; set; }

        protected PaketType paketType;

        [Required]
        public PaketType PaketType { get; protected set; }

        [StringLength(30)]
        public string? CreatedBy { get; set; }

        [StringLength(30)]
        public string? ChangedBy { get; set; }

        public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset LastChanged { get; set; } = DateTimeOffset.Now;

        public bool Active { get; set; } = true;

        public Policy? Policy { get; set; }

        public int? SchemeId { get; set; }

        [StringLength(255)]
        [Required(AllowEmptyStrings = true)]
        public string Special { get; set; } = string.Empty;
    }
}

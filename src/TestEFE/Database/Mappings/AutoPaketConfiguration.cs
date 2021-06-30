//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\Bestand\Importer\Generic Data Import\Data\GenericDataImport.Database\Mapping\AutoPaketConfiguration.cs" company="AXA Partners">
// Author: Jörg H Primke
// Copyright (c) 2020 - AXA Partners. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestEFE.Models;

namespace TestEFE.Database.Mappings
{
    public class AutoPaketConfiguration : IEntityTypeConfiguration<AutoPaket>
    {
        public void Configure(EntityTypeBuilder<AutoPaket> builder)
        {
            builder.ToTable("AutoPaket");

            builder.Property(c => c.LicensePlate).HasMaxLength(20).IsUnicode().IsRequired();
            builder.Property(c => c.Vin).HasMaxLength(20).IsRequired().IsUnicode();
            builder.Property(c => c.Brand).HasMaxLength(30).IsRequired().IsUnicode().HasDefaultValueSql("'Unknown'");
            builder.Property(c => c.Model).HasMaxLength(50).IsRequired().IsUnicode().HasDefaultValueSql("'Unknown'");
            builder.Property(e => e.FirstRegistrationDate).HasColumnType("datetime").HasAnnotation("Relational:ColumnType", "datetime");
        }
    }
}

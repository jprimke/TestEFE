//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\Bestand\Importer\Generic Data Import\Data\GenericDataImport.Database\Mapping\TravelPaketConfiguration.cs" company="AXA Partners">
// Author: Jörg H Primke
// Copyright (c) 2020 - AXA Partners. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestEFE.Models;

namespace TestEFE.Database.Mappings
{
    public class TravelPaketConfiguration : IEntityTypeConfiguration<TravelPaket>
    {
        public void Configure(EntityTypeBuilder<TravelPaket> builder)
        {
            builder.ToTable("TravelPaket");

            builder.Property(e => e.Birthday).HasColumnType("datetime").HasAnnotation("Relational:ColumnType", "datetime");

            builder.OwnsOne(e => e.InsuredPerson,
                            nb =>
                            {
                                nb.Property(c => c.Salutation).HasMaxLength(20).IsUnicode();
                                nb.Property(c => c.FirstName).HasMaxLength(50).IsRequired().IsUnicode();
                                nb.Property(c => c.Name).HasMaxLength(100).IsRequired().IsUnicode();
                            });
            builder.Navigation(e => e.InsuredPerson).IsRequired();
        }
    }
}

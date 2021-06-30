//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\Bestand\Importer\Generic Data Import\Data\GenericDataImport.Database\Mapping\HomePaketConfiguration.cs" company="AXA Partners">
// Author: Jörg H Primke
// Copyright (c) 2020 - AXA Partners. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestEFE.Models;

namespace TestEFE.Database.Mappings
{
    public class HomePaketConfiguration : IEntityTypeConfiguration<HomePaket>
    {
        public void Configure(EntityTypeBuilder<HomePaket> builder)
        {
            builder.ToTable("HomePaket");

            builder.OwnsOne(e => e.AddressInsuredObject,
                            nb =>
                            {
                                nb.Property(s => s.City).HasMaxLength(100).IsRequired().IsUnicode();
                                nb.Property(s => s.CountryCode).HasMaxLength(2).IsFixedLength().IsRequired().IsUnicode();
                                nb.Property(s => s.Street).HasMaxLength(150).IsRequired().IsUnicode();
                                nb.Property(c => c.Zip).HasMaxLength(10).IsRequired().IsUnicode();
                            });
            builder.Navigation(e => e.AddressInsuredObject).IsRequired();
        }
    }
}

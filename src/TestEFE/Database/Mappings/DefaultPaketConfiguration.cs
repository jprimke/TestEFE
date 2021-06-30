//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\Bestand\Importer\Generic Data Import\Data\GenericDataImport.Database\Mapping\DefaultPaketConfiguration.cs" company="AXA Partners">
// Author: Jörg H Primke
// Copyright (c) 2020 - AXA Partners. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestEFE.Models;

namespace TestEFE.Database.Mappings
{
    public class DefaultPaketConfiguration : IEntityTypeConfiguration<DefaultPaket>
    {
        public void Configure(EntityTypeBuilder<DefaultPaket> builder)
        {
            builder.ToTable("DefaultPaket");
        }
    }
}

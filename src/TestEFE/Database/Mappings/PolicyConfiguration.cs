//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\Bestand\Importer\Generic Data Import\Data\GenericDataImport.Database\Mapping\PolicyConfiguration.cs" company="AXA Partners">
// Author: Jörg H Primke
// Copyright (c) 2020 - AXA Partners. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json.Linq;
using TestEFE.Models;

namespace TestEFE.Database.Mappings
{
    public class PolicyConfiguration : IEntityTypeConfiguration<Policy>
    {
        public void Configure(EntityTypeBuilder<Policy> builder)
        {
            builder.ToTable("Policy");

            builder.HasIndex(e =>
                                 new
                                 {
                                     e.PolicyNumber,
                                     e.AdditionalPartForPolicyNumber,
                                     e.ContractId
                                 },
                             "IX_Policy_PolicyNumberAddPartAndOwnerId")
                   .IsUnique();

            builder.Property(e => e.AdditionalDataAsJSON)
                   .HasColumnType("ntext")
                   .HasColumnName("AdditionalDataAsJSON")
                   .HasAnnotation("Relational:ColumnType", "ntext");
            builder.Property(e => e.ChangedBy).IsRequired().HasMaxLength(30).HasDefaultValueSql("''");
            builder.Property(e => e.Created).HasDefaultValueSql("sysdatetimeoffset()");
            builder.Property(e => e.CreatedBy).IsRequired().HasMaxLength(30).HasDefaultValueSql("''");
            builder.Property(e => e.PolicyNumber).IsRequired().HasMaxLength(50);
            builder.Property(e => e.ProductName).IsRequired().HasMaxLength(100).HasDefaultValueSql("'Unknown'");
            builder.Property(e => e.ValidFrom).IsRequired().HasDefaultValueSql("'1900-01-01'");

            builder.OwnsOne(e => e.Address,
                            navBuilder =>
                            {
                                navBuilder.Property(s => s.City).HasMaxLength(100).IsRequired().IsUnicode();
                                navBuilder.Property(s => s.CountryCode).HasMaxLength(2).IsFixedLength().IsRequired().IsUnicode();
                                navBuilder.Property(s => s.Street).HasMaxLength(150).IsRequired().IsUnicode();
                                navBuilder.Property(c => c.Zip).HasMaxLength(10).IsRequired().IsUnicode();
                            });
            builder.Navigation(e => e.Address).IsRequired();

            builder.OwnsOne(e => e.Contact);

            builder.OwnsOne(e => e.Person,
                            nb =>
                            {
                                nb.Property(c => c.Salutation).HasMaxLength(20).IsUnicode();
                                nb.Property(c => c.FirstName).HasMaxLength(50).IsRequired().IsUnicode();
                                nb.Property(c => c.Name).HasMaxLength(100).IsRequired().IsUnicode();
                            });
            builder.Navigation(e => e.Person).IsRequired();
        }
    }
}

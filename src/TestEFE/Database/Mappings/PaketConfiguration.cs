//-----------------------------------------------------------------------
// <copyright file="D:\PROJEKTE\Bestand\Importer\Generic Data Import\Data\GenericDataImport.Database\Mapping\PaketConfiguration.cs" company="AXA Partners">
// Author: Jörg H Primke
// Copyright (c) 2020 - AXA Partners. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestEFE.Models;

namespace TestEFE.Database.Mappings
{
    public class PaketConfiguration : IEntityTypeConfiguration<Paket>
    {
        public void Configure(EntityTypeBuilder<Paket> builder)
        {
            builder.ToTable("Paket");

            builder.HasIndex(e =>
                                 new
                                 {
                                     e.PolicyId,
                                     e.Name,
                                     e.Special
                                 },
                             "IX_Paket_PolicyIdAndNameAndSpecial")
                   .IsUnique();
            builder.HasIndex(e => e.PolicyId, "IX_PolicyId");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            builder.Property(e => e.AdditionalDataAsJSON)
                   .HasColumnType("ntext")
                   .HasColumnName("AdditionalDataAsJSON")
                   .HasAnnotation("Relational:ColumnType", "ntext");
            builder.Property(e => e.ChangedBy).IsRequired().HasMaxLength(30).HasDefaultValueSql("''");
            builder.Property(e => e.Created).HasDefaultValueSql("(sysdatetimeoffset())");
            builder.Property(e => e.CreatedBy).IsRequired().HasMaxLength(30).HasDefaultValueSql("''");
            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
            builder.Property(e => e.ValidFrom).HasColumnType("datetime").HasAnnotation("Relational:ColumnType", "datetime");
            builder.Property(e => e.ValidTo).HasColumnType("datetime").HasAnnotation("Relational:ColumnType", "datetime");
            builder.Property(e => e.Special).HasMaxLength(255).IsRequired().HasDefaultValueSql("''");

            builder.HasOne(d => d.Policy)
                   .WithMany(p => p.Pakets)
                   .HasForeignKey(d => d.PolicyId)
                   .HasConstraintName("FK_dbo.Paket_dbo.Policy_PolicyId");
        }
    }
}

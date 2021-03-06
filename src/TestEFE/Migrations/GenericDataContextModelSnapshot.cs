// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestEFE.Database;

namespace TestEFE.Migrations
{
    [DbContext(typeof(GenericDataContext))]
    partial class GenericDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TestEFE.Models.Paket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("AdditionalDataAsJSON")
                        .HasMaxLength(5000)
                        .HasColumnType("ntext")
                        .HasColumnName("AdditionalDataAsJSON");

                    b.Property<string>("ChangedBy")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasDefaultValueSql("''");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("(sysdatetimeoffset())");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasDefaultValueSql("''");

                    b.Property<DateTimeOffset>("LastChanged")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PaketType")
                        .HasColumnType("int");

                    b.Property<int>("PolicyId")
                        .HasColumnType("int");

                    b.Property<int?>("SchemeId")
                        .HasColumnType("int");

                    b.Property<string>("Special")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasDefaultValueSql("''");

                    b.Property<DateTime>("ValidFrom")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("ValidTo")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "PolicyId", "Name", "Special" }, "IX_Paket_PolicyIdAndNameAndSpecial")
                        .IsUnique();

                    b.HasIndex(new[] { "PolicyId" }, "IX_PolicyId");

                    b.ToTable("Paket");
                });

            modelBuilder.Entity("TestEFE.Models.Policy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("AdditionalDataAsJSON")
                        .HasColumnType("ntext")
                        .HasColumnName("AdditionalDataAsJSON");

                    b.Property<string>("AdditionalPartForPolicyNumber")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ChangedBy")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasDefaultValueSql("''");

                    b.Property<int>("ContractId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("sysdatetimeoffset()");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasDefaultValueSql("''");

                    b.Property<DateTimeOffset>("LastChanged")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("PolicyNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValueSql("'Unknown'");

                    b.Property<DateTime>("ValidFrom")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("'1900-01-01'");

                    b.Property<DateTime?>("ValidTo")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "PolicyNumber", "AdditionalPartForPolicyNumber", "ContractId" }, "IX_Policy_PolicyNumberAddPartAndOwnerId")
                        .IsUnique()
                        .HasFilter("[AdditionalPartForPolicyNumber] IS NOT NULL");

                    b.ToTable("Policy");
                });

            modelBuilder.Entity("TestEFE.Models.AutoPaket", b =>
                {
                    b.HasBaseType("TestEFE.Models.Paket");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(30)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(30)")
                        .HasDefaultValueSql("'Unknown'");

                    b.Property<DateTime>("FirstRegistrationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("'Unknown'");

                    b.Property<string>("Vin")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(20)");

                    b.ToTable("AutoPaket");
                });

            modelBuilder.Entity("TestEFE.Models.DefaultPaket", b =>
                {
                    b.HasBaseType("TestEFE.Models.Paket");

                    b.ToTable("DefaultPaket");
                });

            modelBuilder.Entity("TestEFE.Models.HomePaket", b =>
                {
                    b.HasBaseType("TestEFE.Models.Paket");

                    b.ToTable("HomePaket");
                });

            modelBuilder.Entity("TestEFE.Models.TravelPaket", b =>
                {
                    b.HasBaseType("TestEFE.Models.Paket");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime");

                    b.ToTable("TravelPaket");
                });

            modelBuilder.Entity("TestEFE.Models.Paket", b =>
                {
                    b.HasOne("TestEFE.Models.Policy", "Policy")
                        .WithMany("Pakets")
                        .HasForeignKey("PolicyId")
                        .HasConstraintName("FK_dbo.Paket_dbo.Policy_PolicyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Policy");
                });

            modelBuilder.Entity("TestEFE.Models.Policy", b =>
                {
                    b.OwnsOne("TestEFE.Models.Address", "Address", b1 =>
                        {
                            b1.Property<int>("PolicyId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("CountryCode")
                                .IsRequired()
                                .HasMaxLength(2)
                                .IsUnicode(true)
                                .HasColumnType("nchar(2)")
                                .IsFixedLength(true);

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(150)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(150)");

                            b1.Property<string>("Zip")
                                .IsRequired()
                                .HasMaxLength(10)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(10)");

                            b1.HasKey("PolicyId");

                            b1.ToTable("Policy");

                            b1.WithOwner()
                                .HasForeignKey("PolicyId");
                        });

                    b.OwnsOne("TestEFE.Models.Contact", "Contact", b1 =>
                        {
                            b1.Property<int>("PolicyId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Email")
                                .HasMaxLength(255)
                                .HasColumnType("nvarchar(255)");

                            b1.Property<string>("Phone")
                                .HasMaxLength(30)
                                .HasColumnType("nvarchar(30)");

                            b1.HasKey("PolicyId");

                            b1.ToTable("Policy");

                            b1.WithOwner()
                                .HasForeignKey("PolicyId");
                        });

                    b.OwnsOne("TestEFE.Models.Person", "Person", b1 =>
                        {
                            b1.Property<int>("PolicyId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(100)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("Salutation")
                                .HasMaxLength(20)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(20)");

                            b1.HasKey("PolicyId");

                            b1.ToTable("Policy");

                            b1.WithOwner()
                                .HasForeignKey("PolicyId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Contact")
                        .IsRequired();

                    b.Navigation("Person")
                        .IsRequired();
                });

            modelBuilder.Entity("TestEFE.Models.AutoPaket", b =>
                {
                    b.HasOne("TestEFE.Models.Paket", null)
                        .WithOne()
                        .HasForeignKey("TestEFE.Models.AutoPaket", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestEFE.Models.DefaultPaket", b =>
                {
                    b.HasOne("TestEFE.Models.Paket", null)
                        .WithOne()
                        .HasForeignKey("TestEFE.Models.DefaultPaket", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestEFE.Models.HomePaket", b =>
                {
                    b.HasOne("TestEFE.Models.Paket", null)
                        .WithOne()
                        .HasForeignKey("TestEFE.Models.HomePaket", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.OwnsOne("TestEFE.Models.Address", "AddressInsuredObject", b1 =>
                        {
                            b1.Property<int>("HomePaketId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("CountryCode")
                                .IsRequired()
                                .HasMaxLength(2)
                                .IsUnicode(true)
                                .HasColumnType("nchar(2)")
                                .IsFixedLength(true);

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(150)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(150)");

                            b1.Property<string>("Zip")
                                .IsRequired()
                                .HasMaxLength(10)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(10)");

                            b1.HasKey("HomePaketId");

                            b1.ToTable("HomePaket");

                            b1.WithOwner()
                                .HasForeignKey("HomePaketId");
                        });

                    b.Navigation("AddressInsuredObject")
                        .IsRequired();
                });

            modelBuilder.Entity("TestEFE.Models.TravelPaket", b =>
                {
                    b.HasOne("TestEFE.Models.Paket", null)
                        .WithOne()
                        .HasForeignKey("TestEFE.Models.TravelPaket", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.OwnsOne("TestEFE.Models.Person", "InsuredPerson", b1 =>
                        {
                            b1.Property<int>("TravelPaketId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(100)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("Salutation")
                                .HasMaxLength(20)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(20)");

                            b1.HasKey("TravelPaketId");

                            b1.ToTable("TravelPaket");

                            b1.WithOwner()
                                .HasForeignKey("TravelPaketId");
                        });

                    b.Navigation("InsuredPerson")
                        .IsRequired();
                });

            modelBuilder.Entity("TestEFE.Models.Policy", b =>
                {
                    b.Navigation("Pakets");
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestEFE.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Policy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AdditionalPartForPolicyNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Person_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Person_FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Person_Salutation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address_Street = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Address_Zip = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_CountryCode = table.Column<string>(type: "nchar(2)", fixedLength: true, maxLength: 2, nullable: false),
                    Contact_Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Contact_Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AdditionalDataAsJSON = table.Column<string>(type: "ntext", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "sysdatetimeoffset()"),
                    LastChanged = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValueSql: "''"),
                    ChangedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValueSql: "''"),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "'Unknown'"),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "'1900-01-01'"),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime", nullable: true),
                    AdditionalDataAsJSON = table.Column<string>(type: "ntext", maxLength: 5000, nullable: true),
                    PaketType = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValueSql: "''"),
                    ChangedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValueSql: "''"),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(sysdatetimeoffset())"),
                    LastChanged = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    SchemeId = table.Column<int>(type: "int", nullable: true),
                    Special = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, defaultValueSql: "''")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.Paket_dbo.Policy_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AutoPaket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Vin = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValueSql: "'Unknown'"),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "'Unknown'"),
                    FirstRegistrationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoPaket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutoPaket_Paket_Id",
                        column: x => x.Id,
                        principalTable: "Paket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DefaultPaket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultPaket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DefaultPaket_Paket_Id",
                        column: x => x.Id,
                        principalTable: "Paket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomePaket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AddressInsuredObject_Street = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AddressInsuredObject_Zip = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AddressInsuredObject_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AddressInsuredObject_CountryCode = table.Column<string>(type: "nchar(2)", fixedLength: true, maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePaket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomePaket_Paket_Id",
                        column: x => x.Id,
                        principalTable: "Paket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TravelPaket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    InsuredPerson_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InsuredPerson_FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsuredPerson_Salutation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelPaket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelPaket_Paket_Id",
                        column: x => x.Id,
                        principalTable: "Paket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Paket_PolicyIdAndNameAndSpecial",
                table: "Paket",
                columns: new[] { "PolicyId", "Name", "Special" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PolicyId",
                table: "Paket",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_PolicyNumberAddPartAndOwnerId",
                table: "Policy",
                columns: new[] { "PolicyNumber", "AdditionalPartForPolicyNumber", "ContractId" },
                unique: true,
                filter: "[AdditionalPartForPolicyNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoPaket");

            migrationBuilder.DropTable(
                name: "DefaultPaket");

            migrationBuilder.DropTable(
                name: "HomePaket");

            migrationBuilder.DropTable(
                name: "TravelPaket");

            migrationBuilder.DropTable(
                name: "Paket");

            migrationBuilder.DropTable(
                name: "Policy");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Contacts.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class migrationone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ParentContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Contacts_ParentContactId",
                        column: x => x.ParentContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Name", "ParentContactId" },
                values: new object[,]
                {
                    { new Guid("1b5638a1-7ef2-4fb9-a8a2-a751dd81349e"), "prywatny", null },
                    { new Guid("4b115b8d-598c-4433-9883-4f5f55f03f17"), "sluzbowy", null },
                    { new Guid("5215fe15-0dc9-4ad9-9742-4711823232e9"), "inny", null },
                    { new Guid("551a3bb1-f066-4b0a-bb42-a7f76db5a258"), "sluzbowy", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleType" },
                values: new object[] { new Guid("176281dc-738d-4cc2-a9c1-ee3e0bea1dce"), "User" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Name", "ParentContactId" },
                values: new object[,]
                {
                    { new Guid("83e618be-4c4c-4162-a6e8-296586b82cb8"), "szef", new Guid("551a3bb1-f066-4b0a-bb42-a7f76db5a258") },
                    { new Guid("c5c0f6a1-4f25-47d0-9dd1-f713c92d754a"), "klient", new Guid("551a3bb1-f066-4b0a-bb42-a7f76db5a258") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "Password", "RoleId" },
                values: new object[] { new Guid("ca801fb2-176c-4fbf-97eb-3f65c13dc202"), "TestUser", "0db32429d0398d23872b01db915daf6d6e75015eab57f2a43b2952a70efe79da", new Guid("176281dc-738d-4cc2-a9c1-ee3e0bea1dce") });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "ContactId", "DateOfBirth", "Email", "FirstName", "LastName", "Password", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("60594a88-5b2e-4c4c-9de1-f84ddc673b9c"), new Guid("83e618be-4c4c-4162-a6e8-296586b82cb8"), new DateTime(1995, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test2@gmail.com", "Test2", "LastName2", "haslo2", 111111111 },
                    { new Guid("d050e44d-8a08-4e4c-931b-bdb120354dfa"), new Guid("83e618be-4c4c-4162-a6e8-296586b82cb8"), new DateTime(1990, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test1@gmail.com", "Test1", "LastName1", "haslo1", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ParentContactId",
                table: "Contacts",
                column: "ParentContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ContactId",
                table: "Persons",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Contacts.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class sjsjs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("1b5638a1-7ef2-4fb9-a8a2-a751dd81349e"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("4b115b8d-598c-4433-9883-4f5f55f03f17"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("5215fe15-0dc9-4ad9-9742-4711823232e9"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("c5c0f6a1-4f25-47d0-9dd1-f713c92d754a"));

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("60594a88-5b2e-4c4c-9de1-f84ddc673b9c"));

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("d050e44d-8a08-4e4c-931b-bdb120354dfa"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ca801fb2-176c-4fbf-97eb-3f65c13dc202"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("83e618be-4c4c-4162-a6e8-296586b82cb8"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("176281dc-738d-4cc2-a9c1-ee3e0bea1dce"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("551a3bb1-f066-4b0a-bb42-a7f76db5a258"));

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Name", "ParentContactId" },
                values: new object[,]
                {
                    { new Guid("212e7d98-3c46-41ce-bab3-681fd85defd4"), "prywatny", null },
                    { new Guid("6d9e0e98-4d18-4a85-84f0-091f7cb84c86"), "inny", null },
                    { new Guid("df115677-64a9-4603-815c-851db272a33a"), "sluzbowy", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleType" },
                values: new object[] { new Guid("ebf54045-415a-48ee-af1a-09e5a70c7499"), "User" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Name", "ParentContactId" },
                values: new object[,]
                {
                    { new Guid("2c9d97e8-c054-4947-95b4-c2bdc7f0851d"), "klient", new Guid("df115677-64a9-4603-815c-851db272a33a") },
                    { new Guid("733916b6-da40-47a0-8326-5f15b585a035"), "szef", new Guid("df115677-64a9-4603-815c-851db272a33a") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "Password", "RoleId" },
                values: new object[] { new Guid("353fe9e5-2d8b-4d43-a1fb-f8f0e461b42a"), "TestUser", "0db32429d0398d23872b01db915daf6d6e75015eab57f2a43b2952a70efe79da", new Guid("ebf54045-415a-48ee-af1a-09e5a70c7499") });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "ContactId", "DateOfBirth", "Email", "FirstName", "LastName", "Password", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("194f7583-c230-46c7-be5c-8077ece78d9a"), new Guid("733916b6-da40-47a0-8326-5f15b585a035"), new DateTime(1990, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test1@gmail.com", "Test1", "LastName1", "haslo1", 0 },
                    { new Guid("5238cf46-b7bc-4d1a-abb5-b3efaeb6c3e8"), new Guid("733916b6-da40-47a0-8326-5f15b585a035"), new DateTime(1995, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test2@gmail.com", "Test2", "LastName2", "haslo2", 111111111 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("212e7d98-3c46-41ce-bab3-681fd85defd4"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("2c9d97e8-c054-4947-95b4-c2bdc7f0851d"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("6d9e0e98-4d18-4a85-84f0-091f7cb84c86"));

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("194f7583-c230-46c7-be5c-8077ece78d9a"));

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: new Guid("5238cf46-b7bc-4d1a-abb5-b3efaeb6c3e8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("353fe9e5-2d8b-4d43-a1fb-f8f0e461b42a"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("733916b6-da40-47a0-8326-5f15b585a035"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ebf54045-415a-48ee-af1a-09e5a70c7499"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("df115677-64a9-4603-815c-851db272a33a"));

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
        }
    }
}

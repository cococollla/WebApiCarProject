using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarWebService.DAL.Migrations
{
    public partial class ChangeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShorDescription",
                table: "Cars",
                newName: "ShortDescription");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "08096d83-6505-423f-843e-9ee51e79b457", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "7ad85874-5c48-465e-86d3-a2b7d5c0aa9e", "MANAGER" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "2c54d9b2-d72a-4acc-b316-69d2ddf8d96d", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "Cars",
                newName: "ShorDescription");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "21940fb5-9b78-4ddc-8899-2f9aa69a0116", null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "f039422b-b35a-42fc-9124-08d3b0ac2982", null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "317aa514-2601-4a71-9384-af1ecbe342d8", null });
        }
    }
}

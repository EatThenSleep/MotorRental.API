using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorRental.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14047e48-70af-45b0-a548-50e214bcdaba",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Admin", "admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2d5df123-4595-4307-9a1e-6d1f67fb7182",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af7b1d9b-681c-49d5-90ba-f08632c6f5f3", "Cong Vien", "CÔNG VIÊN", "AQAAAAIAAYagAAAAEObm2r6LL5lCIP/j9HuCvHWjOExfVU3MHOsIa6OR9nWg25YEyHYy/Mg6zWbDHC5oqw==", "7cf952c8-50fe-4030-9467-192ffc1cd68a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14047e48-70af-45b0-a548-50e214bcdaba",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Visitor", "visitor" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2d5df123-4595-4307-9a1e-6d1f67fb7182",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff425c7a-9de2-4332-bf84-656215a81fb7", "Huỳnh Văn Giảng", "HUYNH VAN GIANG", "AQAAAAIAAYagAAAAEH3RZjVQao4jYQX6fCX3VGH6ld3yV+nJRBbyuhv5Pkct0C5rzu/a/2INFEKqMIiXSA==", "65b249c3-4ee5-47c9-9408-4d165e613aed" });
        }
    }
}

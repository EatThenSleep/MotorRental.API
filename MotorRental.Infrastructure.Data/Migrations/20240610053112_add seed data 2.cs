using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorRental.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addseeddata2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2d5df123-4595-4307-9a1e-6d1f67fb7182",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "4d3ff379-f510-4cb5-96a4-82a726193c65", "huynhvangiang@gmail.com", "HUYNHVANGIANG@GMAIL.COM", "AQAAAAIAAYagAAAAELK9QwwvBkbq9560Nrs2L9XsYUpuRP0rmfo2XexhvYVJy02XxFu3mIPrjjGTW6Zolg==", "894ea844-e247-43ee-a19d-f589f6e83c5e", "huynhvangiang@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2d5df123-4595-4307-9a1e-6d1f67fb7182",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "af7b1d9b-681c-49d5-90ba-f08632c6f5f3", "nickdehoc0504@gmail.com", "NICKDEHOC0504@GMAIL.COM", "AQAAAAIAAYagAAAAEObm2r6LL5lCIP/j9HuCvHWjOExfVU3MHOsIa6OR9nWg25YEyHYy/Mg6zWbDHC5oqw==", "7cf952c8-50fe-4030-9467-192ffc1cd68a", "nickdehoc0504@gmail.com" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorRental.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addseeduserrolevistor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ff4e682a-94dd-43e8-bafd-cceaec160d90", "8842457c-2323-4fd6-969e-c320b461b3d2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff4e682a-94dd-43e8-bafd-cceaec160d90");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8842457c-2323-4fd6-969e-c320b461b3d2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "14047e48-70af-45b0-a548-50e214bcdaba", "14047e48-70af-45b0-a548-50e214bcdaba", "Visitor", "visitor" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "IdentityImageBack", "IdentityImagePre", "IdentityNumber", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UrlAvatar", "UserName" },
                values: new object[] { "2d5df123-4595-4307-9a1e-6d1f67fb7182", 0, "ff425c7a-9de2-4332-bf84-656215a81fb7", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nickdehoc0504@gmail.com", false, "", "", "", false, null, "Huỳnh Văn Giảng", "NICKDEHOC0504@GMAIL.COM", "HUYNH VAN GIANG", "AQAAAAIAAYagAAAAEH3RZjVQao4jYQX6fCX3VGH6ld3yV+nJRBbyuhv5Pkct0C5rzu/a/2INFEKqMIiXSA==", null, false, "65b249c3-4ee5-47c9-9408-4d165e613aed", false, "", "nickdehoc0504@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "14047e48-70af-45b0-a548-50e214bcdaba", "2d5df123-4595-4307-9a1e-6d1f67fb7182" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "14047e48-70af-45b0-a548-50e214bcdaba", "2d5df123-4595-4307-9a1e-6d1f67fb7182" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14047e48-70af-45b0-a548-50e214bcdaba");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2d5df123-4595-4307-9a1e-6d1f67fb7182");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ff4e682a-94dd-43e8-bafd-cceaec160d90", "ff4e682a-94dd-43e8-bafd-cceaec160d90", "Owner", "owner" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "IdentityImageBack", "IdentityImagePre", "IdentityNumber", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UrlAvatar", "UserName" },
                values: new object[] { "8842457c-2323-4fd6-969e-c320b461b3d2", 0, "3c31b645-45a4-441f-a20f-a5adb9935cfe", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "huynhvangiang0504@gmail.com", false, "", "", "", false, null, "Huỳnh Văn Giảng", "HUYNHVANGIANG0504@GMAIL.COM", "HUYNHVANGIANG0504@GMAIL.COM", "AQAAAAIAAYagAAAAEKhlKZozxAzUubWcb3SvlCwQzWSwjYNHx6CH3SKtK5QDXp9iY4qg1BQZWwuL71YX8Q==", null, false, "4e1efc87-7fb6-4756-a330-59a5259ecfc8", false, "", "huynhvangiang0504@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ff4e682a-94dd-43e8-bafd-cceaec160d90", "8842457c-2323-4fd6-969e-c320b461b3d2" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorRental.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addseedowner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motorbikes_AspNetUsers_UserId",
                table: "Motorbikes");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Motorbikes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Motorbikes_AspNetUsers_UserId",
                table: "Motorbikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motorbikes_AspNetUsers_UserId",
                table: "Motorbikes");

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

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Motorbikes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Motorbikes_AspNetUsers_UserId",
                table: "Motorbikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MotorRental.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addseeddata3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[,]
                {
                    { "301dc331-909a-4766-a360-c62f4f3780a8", "301dc331-909a-4766-a360-c62f4f3780a8", "Owner", "owner" },
                    { "3d3d69df-3ed5-4067-a2aa-6e2a65b13a10", "3d3d69df-3ed5-4067-a2aa-6e2a65b13a10", "Admin", "admin" },
                    { "89a9aa03-6e32-4e2c-b68e-f900b74504d3", "89a9aa03-6e32-4e2c-b68e-f900b74504d3", "Visitor", "visitor" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "IdentityImageBack", "IdentityImagePre", "IdentityNumber", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UrlAvatar", "UserName" },
                values: new object[,]
                {
                    { "020544df-1e91-49ff-8815-eedd9139c16e", 0, "70c46436-9f70-4c27-9173-1b511fa49f28", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "visitor@gmail.com", false, "", "", "", false, null, "Visitor 1", "VISITOR@GMAIL.COM", "VISITOR 1", "AQAAAAIAAYagAAAAEP53s/PI5RkGzcBRlqDtXI5vC4LivQO7XfnpOfiDVVXoHKo+YZ/WNicU/ndAAOvDJg==", null, false, "c9f38ea4-8c23-46b8-b567-fe89985b0787", false, "", "visitor@gmail.com" },
                    { "3cf092c8-08f8-406a-bd95-fedf4336036e", 0, "d272589e-5807-4ca4-9e9b-7f8fc9bdac2e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "owner@gmail.com", false, "", "", "", false, null, "Owner 1", "OWNER@GMAIL.COM", "OWNER 1", "AQAAAAIAAYagAAAAEEylzCz2AHd+MjXVsSmO0CWbUBeMIeZHGsnoWSKOLUzetXWFSyYbci02IWpkL8hn/A==", null, false, "031cbf8e-801a-4425-8b74-316ea3f61151", false, "", "owner@gmail.com" },
                    { "a829d08d-8810-46f8-a03d-f3d89b4dfdd8", 0, "5c5c1a6b-51a0-4d3a-b15c-06377bb84123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", false, "", "", "", false, null, "Admin 1", "ADMIN@GMAIL.COM", "ADMIN 1", "AQAAAAIAAYagAAAAEB8RXeG4DrZIHKBWY8aBjUtah76mY83EJo98i2wMr6IsMTFfaqbOMc86sYoHwgEvJw==", null, false, "2114db81-db1f-4a89-81e1-c113653801a2", false, "", "admin@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "89a9aa03-6e32-4e2c-b68e-f900b74504d3", "020544df-1e91-49ff-8815-eedd9139c16e" },
                    { "301dc331-909a-4766-a360-c62f4f3780a8", "3cf092c8-08f8-406a-bd95-fedf4336036e" },
                    { "3d3d69df-3ed5-4067-a2aa-6e2a65b13a10", "a829d08d-8810-46f8-a03d-f3d89b4dfdd8" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "89a9aa03-6e32-4e2c-b68e-f900b74504d3", "020544df-1e91-49ff-8815-eedd9139c16e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "301dc331-909a-4766-a360-c62f4f3780a8", "3cf092c8-08f8-406a-bd95-fedf4336036e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3d3d69df-3ed5-4067-a2aa-6e2a65b13a10", "a829d08d-8810-46f8-a03d-f3d89b4dfdd8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "301dc331-909a-4766-a360-c62f4f3780a8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d3d69df-3ed5-4067-a2aa-6e2a65b13a10");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89a9aa03-6e32-4e2c-b68e-f900b74504d3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "020544df-1e91-49ff-8815-eedd9139c16e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3cf092c8-08f8-406a-bd95-fedf4336036e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a829d08d-8810-46f8-a03d-f3d89b4dfdd8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "14047e48-70af-45b0-a548-50e214bcdaba", "14047e48-70af-45b0-a548-50e214bcdaba", "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "IdentityImageBack", "IdentityImagePre", "IdentityNumber", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UrlAvatar", "UserName" },
                values: new object[] { "2d5df123-4595-4307-9a1e-6d1f67fb7182", 0, "4d3ff379-f510-4cb5-96a4-82a726193c65", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "huynhvangiang@gmail.com", false, "", "", "", false, null, "Cong Vien", "HUYNHVANGIANG@GMAIL.COM", "CÔNG VIÊN", "AQAAAAIAAYagAAAAELK9QwwvBkbq9560Nrs2L9XsYUpuRP0rmfo2XexhvYVJy02XxFu3mIPrjjGTW6Zolg==", null, false, "894ea844-e247-43ee-a19d-f589f6e83c5e", false, "", "huynhvangiang@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "14047e48-70af-45b0-a548-50e214bcdaba", "2d5df123-4595-4307-9a1e-6d1f67fb7182" });
        }
    }
}

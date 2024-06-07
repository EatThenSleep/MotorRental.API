using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorRental.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMotorbikeAvatarLicensePlatecolumninMotorbikeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LicensePlate",
                table: "Motorbikes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MotorbikeAvatar",
                table: "Motorbikes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicensePlate",
                table: "Motorbikes");

            migrationBuilder.DropColumn(
                name: "MotorbikeAvatar",
                table: "Motorbikes");
        }
    }
}

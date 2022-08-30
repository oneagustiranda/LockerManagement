using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locker.API.Migrations
{
    public partial class updateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "Lockers");

            migrationBuilder.AddColumn<string>(
                name: "LockerNo",
                table: "Lockers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Lockers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LockerNo",
                table: "Lockers");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Lockers");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "Lockers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class DateIdentityServerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Graduates");

            migrationBuilder.AddColumn<DateTime>(
                name: "GraduateAt",
                table: "Graduates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GraduateAt",
                table: "Graduates");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Graduates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

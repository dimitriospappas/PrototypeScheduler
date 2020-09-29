using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchedulerAPI.Migrations
{
    public partial class FixedProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModiefed",
                table: "Skills",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "LastModiefed",
                table: "Employees",
                newName: "LastModified");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
            name: "LastModified",
            table: "Skills",
            newName: "LastModiefed");
            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Employees",
                newName: "LastModiefed");
        }
    }
}

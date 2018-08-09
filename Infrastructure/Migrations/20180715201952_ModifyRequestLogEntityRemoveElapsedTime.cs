using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ModifyRequestLogEntityRemoveElapsedTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElapsedTicks",
                table: "RequestLog");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "ElapsedTicks",
                table: "RequestLog",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}

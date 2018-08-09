using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddRequestLogEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RequestMethod = table.Column<string>(maxLength: 20, nullable: false),
                    ResponseStatusCode = table.Column<string>(maxLength: 20, nullable: false),
                    UrlPath = table.Column<string>(maxLength: 200, nullable: false),
                    ElapsedTicks = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLog", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestLog");
        }
    }
}

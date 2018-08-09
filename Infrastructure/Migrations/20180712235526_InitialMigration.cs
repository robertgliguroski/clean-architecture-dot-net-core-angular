using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Code = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Airport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Airport_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlightRoute",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    ValidFrom = table.Column<DateTimeOffset>(nullable: false),
                    ValidTo = table.Column<DateTimeOffset>(nullable: false),
                    OriginId = table.Column<int>(nullable: true),
                    DestinationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightRoute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightRoute_Airport_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Airport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FlightRoute_Airport_OriginId",
                        column: x => x.OriginId,
                        principalTable: "Airport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FlightInstance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    DepartureTime = table.Column<DateTimeOffset>(nullable: false),
                    ArrivalTime = table.Column<DateTimeOffset>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    FlightRouteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightInstance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightInstance_FlightRoute_FlightRouteId",
                        column: x => x.FlightRouteId,
                        principalTable: "FlightRoute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlightTicket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    DatePurchased = table.Column<DateTimeOffset>(nullable: false),
                    FlightInstanceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightTicket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightTicket_FlightInstance_FlightInstanceId",
                        column: x => x.FlightInstanceId,
                        principalTable: "FlightInstance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Airport_CityId",
                table: "Airport",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryId",
                table: "City",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightInstance_FlightRouteId",
                table: "FlightInstance",
                column: "FlightRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightRoute_DestinationId",
                table: "FlightRoute",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightRoute_OriginId",
                table: "FlightRoute",
                column: "OriginId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightTicket_FlightInstanceId",
                table: "FlightTicket",
                column: "FlightInstanceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightTicket");

            migrationBuilder.DropTable(
                name: "FlightInstance");

            migrationBuilder.DropTable(
                name: "FlightRoute");

            migrationBuilder.DropTable(
                name: "Airport");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AirportFLightRouteNavigationProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightRoute_Airport_DestinationId",
                table: "FlightRoute");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightRoute_Airport_OriginId",
                table: "FlightRoute");

            migrationBuilder.AlterColumn<int>(
                name: "OriginId",
                table: "FlightRoute",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DestinationId",
                table: "FlightRoute",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_FlightRoute_Airport_DestinationId",
                table: "FlightRoute",
                column: "DestinationId",
                principalTable: "Airport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightRoute_Airport_OriginId",
                table: "FlightRoute",
                column: "OriginId",
                principalTable: "Airport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightRoute_Airport_DestinationId",
                table: "FlightRoute");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightRoute_Airport_OriginId",
                table: "FlightRoute");

            migrationBuilder.AlterColumn<int>(
                name: "OriginId",
                table: "FlightRoute",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DestinationId",
                table: "FlightRoute",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightRoute_Airport_DestinationId",
                table: "FlightRoute",
                column: "DestinationId",
                principalTable: "Airport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightRoute_Airport_OriginId",
                table: "FlightRoute",
                column: "OriginId",
                principalTable: "Airport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

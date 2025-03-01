using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NimbusApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFlightsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "flights",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    airline = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    departure_airport_code = table.Column<string>(type: "text", nullable: false),
                    destination_airport_code = table.Column<string>(type: "text", nullable: false),
                    departure_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    arrival_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    layovers = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("flight_pkey", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_flights_airline",
                table: "flights",
                column: "airline");

            migrationBuilder.CreateIndex(
                name: "IX_flights_departure_airport_code",
                table: "flights",
                column: "departure_airport_code");

            migrationBuilder.CreateIndex(
                name: "IX_flights_destination_airport_code",
                table: "flights",
                column: "destination_airport_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "flights");
        }
    }
}

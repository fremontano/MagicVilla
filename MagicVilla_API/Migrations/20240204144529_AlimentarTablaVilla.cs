using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenida", "Detalle", "FechaDeActualizacion", "FechaDeCreacion", "ImageUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detalle de la villa..", new DateTime(2024, 2, 4, 11, 45, 29, 823, DateTimeKind.Local).AddTicks(3255), new DateTime(2024, 2, 4, 11, 45, 29, 823, DateTimeKind.Local).AddTicks(3207), "", 150, "Villa nancy", 5, 200.0 },
                    { 2, "", "Detalle de la villa..", new DateTime(2024, 2, 4, 11, 45, 29, 823, DateTimeKind.Local).AddTicks(3289), new DateTime(2024, 2, 4, 11, 45, 29, 823, DateTimeKind.Local).AddTicks(3287), "", 40, "Premiun a la Piscina", 2, 270.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscuelaPrimariaAPI.Migrations
{
    /// <inheritdoc />
    public partial class CambiotablaestudiantecampoidturnoinnecesarioCorrecion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Turnos_TurnoidTurno",
                table: "Estudiantes");

        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscuelaPrimariaAPI.Migrations
{
    /// <inheritdoc />
    public partial class Correciontablaestudiante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Turnos_TurnoidTurno",
                table: "Estudiantes");

            migrationBuilder.RenameColumn(
                name: "TurnoidTurno",
                table: "Estudiantes",
                newName: "idTurno");

            migrationBuilder.RenameIndex(
                name: "IX_Estudiantes_TurnoidTurno",
                table: "Estudiantes",
                newName: "IX_Estudiantes_idTurno");

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Turnos_idTurno",
                table: "Estudiantes",
                column: "idTurno",
                principalTable: "Turnos",
                principalColumn: "idTurno");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Turnos_idTurno",
                table: "Estudiantes");

            migrationBuilder.RenameColumn(
                name: "idTurno",
                table: "Estudiantes",
                newName: "TurnoidTurno");

            migrationBuilder.RenameIndex(
                name: "IX_Estudiantes_idTurno",
                table: "Estudiantes",
                newName: "IX_Estudiantes_TurnoidTurno");

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Turnos_TurnoidTurno",
                table: "Estudiantes",
                column: "TurnoidTurno",
                principalTable: "Turnos",
                principalColumn: "idTurno");
        }
    }
}

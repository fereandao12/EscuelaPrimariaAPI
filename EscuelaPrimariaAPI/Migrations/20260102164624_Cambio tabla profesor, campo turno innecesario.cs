using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscuelaPrimariaAPI.Migrations
{
    /// <inheritdoc />
    public partial class Cambiotablaprofesorcampoturnoinnecesario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profesores_Turnos_idTurno",
                table: "Profesores");

            migrationBuilder.RenameColumn(
                name: "idTurno",
                table: "Profesores",
                newName: "TurnoidTurno");

            migrationBuilder.RenameIndex(
                name: "IX_Profesores_idTurno",
                table: "Profesores",
                newName: "IX_Profesores_TurnoidTurno");

            migrationBuilder.AddForeignKey(
                name: "FK_Profesores_Turnos_TurnoidTurno",
                table: "Profesores",
                column: "TurnoidTurno",
                principalTable: "Turnos",
                principalColumn: "idTurno");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profesores_Turnos_TurnoidTurno",
                table: "Profesores");

            migrationBuilder.RenameColumn(
                name: "TurnoidTurno",
                table: "Profesores",
                newName: "idTurno");

            migrationBuilder.RenameIndex(
                name: "IX_Profesores_TurnoidTurno",
                table: "Profesores",
                newName: "IX_Profesores_idTurno");

            migrationBuilder.AddForeignKey(
                name: "FK_Profesores_Turnos_idTurno",
                table: "Profesores",
                column: "idTurno",
                principalTable: "Turnos",
                principalColumn: "idTurno");
        }
    }
}

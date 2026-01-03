using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscuelaPrimariaAPI.Migrations
{
    /// <inheritdoc />
    public partial class CambiorelacionesprofesorturnoCorrecion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profesores_Turnos_idTurno",
                table: "Profesores");

            migrationBuilder.DropIndex(
                name: "IX_Profesores_idTurno",
                table: "Profesores");

            migrationBuilder.DropColumn(
                name: "idTurno",
                table: "Profesores");

            migrationBuilder.CreateTable(
                name: "ProfesorTurno",
                columns: table => new
                {
                    ProfesoresidProfesor = table.Column<int>(type: "int", nullable: false),
                    TurnosidTurno = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfesorTurno", x => new { x.ProfesoresidProfesor, x.TurnosidTurno });
                    table.ForeignKey(
                        name: "FK_ProfesorTurno_Profesores_ProfesoresidProfesor",
                        column: x => x.ProfesoresidProfesor,
                        principalTable: "Profesores",
                        principalColumn: "idProfesor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfesorTurno_Turnos_TurnosidTurno",
                        column: x => x.TurnosidTurno,
                        principalTable: "Turnos",
                        principalColumn: "idTurno",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfesorTurno_TurnosidTurno",
                table: "ProfesorTurno",
                column: "TurnosidTurno");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfesorTurno");

            migrationBuilder.AddColumn<int>(
                name: "idTurno",
                table: "Profesores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profesores_idTurno",
                table: "Profesores",
                column: "idTurno");

            migrationBuilder.AddForeignKey(
                name: "FK_Profesores_Turnos_idTurno",
                table: "Profesores",
                column: "idTurno",
                principalTable: "Turnos",
                principalColumn: "idTurno");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscuelaPrimariaAPI.Migrations
{
    /// <inheritdoc />
    public partial class Correcionderelacionesinnecesarias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Turnos_TurnoidTurno",
                table: "Estudiantes");*/

            migrationBuilder.DropTable(
                name: "ProfesorTurno");

            migrationBuilder.DropIndex(
                name: "IX_Estudiantes_TurnoidTurno",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "TurnoidTurno",
                table: "Estudiantes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TurnoidTurno",
                table: "Estudiantes",
                type: "int",
                nullable: true);

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
                name: "IX_Estudiantes_TurnoidTurno",
                table: "Estudiantes",
                column: "TurnoidTurno");

            migrationBuilder.CreateIndex(
                name: "IX_ProfesorTurno_TurnosidTurno",
                table: "ProfesorTurno",
                column: "TurnosidTurno");

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Turnos_TurnoidTurno",
                table: "Estudiantes",
                column: "TurnoidTurno",
                principalTable: "Turnos",
                principalColumn: "idTurno");
        }
    }
}

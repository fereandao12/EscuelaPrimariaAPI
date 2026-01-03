using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscuelaPrimariaAPI.Migrations
{
    /// <inheritdoc />
    public partial class Correcionderelacionesinnecesariasfinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Profesores_idProfesor",
                table: "Estudiantes");

            migrationBuilder.DropIndex(
                name: "IX_Estudiantes_idProfesor",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "idProfesor",
                table: "Estudiantes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idProfesor",
                table: "Estudiantes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_idProfesor",
                table: "Estudiantes",
                column: "idProfesor");

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Profesores_idProfesor",
                table: "Estudiantes",
                column: "idProfesor",
                principalTable: "Profesores",
                principalColumn: "idProfesor");
        }
    }
}

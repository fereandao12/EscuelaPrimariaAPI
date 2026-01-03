using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscuelaPrimariaAPI.Migrations
{
    /// <inheritdoc />
    public partial class CambiorelacionestablaProfesorconSeccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profesores_Secciones_idSeccion",
                table: "Profesores");

            migrationBuilder.DropIndex(
                name: "IX_Profesores_idSeccion",
                table: "Profesores");

            migrationBuilder.DropColumn(
                name: "idSeccion",
                table: "Profesores");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idSeccion",
                table: "Profesores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profesores_idSeccion",
                table: "Profesores",
                column: "idSeccion");

            migrationBuilder.AddForeignKey(
                name: "FK_Profesores_Secciones_idSeccion",
                table: "Profesores",
                column: "idSeccion",
                principalTable: "Secciones",
                principalColumn: "idSeccion");
        }
    }
}

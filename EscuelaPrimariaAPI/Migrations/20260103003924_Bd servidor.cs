using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscuelaPrimariaAPI.Migrations
{
    /// <inheritdoc />
    public partial class Bdservidor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apoderados",
                columns: table => new
                {
                    idApoderado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroTelf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apoderados", x => x.idApoderado);
                });

            migrationBuilder.CreateTable(
                name: "Profesores",
                columns: table => new
                {
                    idProfesor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesores", x => x.idProfesor);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    idTurno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.idTurno);
                });

            migrationBuilder.CreateTable(
                name: "Secciones",
                columns: table => new
                {
                    idSeccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grado = table.Column<int>(type: "int", nullable: false),
                    idProfesor = table.Column<int>(type: "int", nullable: true),
                    idTurno = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secciones", x => x.idSeccion);
                    table.ForeignKey(
                        name: "FK_Secciones_Profesores_idProfesor",
                        column: x => x.idProfesor,
                        principalTable: "Profesores",
                        principalColumn: "idProfesor");
                    table.ForeignKey(
                        name: "FK_Secciones_Turnos_idTurno",
                        column: x => x.idTurno,
                        principalTable: "Turnos",
                        principalColumn: "idTurno");
                });

            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    idEstudiante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    Repitente = table.Column<bool>(type: "bit", nullable: false),
                    Discapacidad = table.Column<bool>(type: "bit", nullable: false),
                    imgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idSeccion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.idEstudiante);
                    table.ForeignKey(
                        name: "FK_Estudiantes_Secciones_idSeccion",
                        column: x => x.idSeccion,
                        principalTable: "Secciones",
                        principalColumn: "idSeccion");
                });

            migrationBuilder.CreateTable(
                name: "ApoderadoEstudiante",
                columns: table => new
                {
                    ApoderadosidApoderado = table.Column<int>(type: "int", nullable: false),
                    menoresACargoidEstudiante = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApoderadoEstudiante", x => new { x.ApoderadosidApoderado, x.menoresACargoidEstudiante });
                    table.ForeignKey(
                        name: "FK_ApoderadoEstudiante_Apoderados_ApoderadosidApoderado",
                        column: x => x.ApoderadosidApoderado,
                        principalTable: "Apoderados",
                        principalColumn: "idApoderado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApoderadoEstudiante_Estudiantes_menoresACargoidEstudiante",
                        column: x => x.menoresACargoidEstudiante,
                        principalTable: "Estudiantes",
                        principalColumn: "idEstudiante",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApoderadoEstudiante_menoresACargoidEstudiante",
                table: "ApoderadoEstudiante",
                column: "menoresACargoidEstudiante");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_idSeccion",
                table: "Estudiantes",
                column: "idSeccion");

            migrationBuilder.CreateIndex(
                name: "IX_Secciones_idProfesor",
                table: "Secciones",
                column: "idProfesor");

            migrationBuilder.CreateIndex(
                name: "IX_Secciones_idTurno",
                table: "Secciones",
                column: "idTurno");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApoderadoEstudiante");

            migrationBuilder.DropTable(
                name: "Apoderados");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Secciones");

            migrationBuilder.DropTable(
                name: "Profesores");

            migrationBuilder.DropTable(
                name: "Turnos");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscuelaPrimariaAPI.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
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
                    idSeccion = table.Column<int>(type: "int", nullable: true),
                    idProfesor = table.Column<int>(type: "int", nullable: true),
                    TurnoidTurno = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.idEstudiante);
                    table.ForeignKey(
                        name: "FK_Estudiantes_Turnos_TurnoidTurno",
                        column: x => x.TurnoidTurno,
                        principalTable: "Turnos",
                        principalColumn: "idTurno");
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
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    idTurno = table.Column<int>(type: "int", nullable: true),
                    idSeccion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesores", x => x.idProfesor);
                    table.ForeignKey(
                        name: "FK_Profesores_Turnos_idTurno",
                        column: x => x.idTurno,
                        principalTable: "Turnos",
                        principalColumn: "idTurno");
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

            migrationBuilder.CreateIndex(
                name: "IX_ApoderadoEstudiante_menoresACargoidEstudiante",
                table: "ApoderadoEstudiante",
                column: "menoresACargoidEstudiante");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_idProfesor",
                table: "Estudiantes",
                column: "idProfesor");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_idSeccion",
                table: "Estudiantes",
                column: "idSeccion");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_TurnoidTurno",
                table: "Estudiantes",
                column: "TurnoidTurno");

            migrationBuilder.CreateIndex(
                name: "IX_Profesores_idSeccion",
                table: "Profesores",
                column: "idSeccion");

            migrationBuilder.CreateIndex(
                name: "IX_Profesores_idTurno",
                table: "Profesores",
                column: "idTurno");

            migrationBuilder.CreateIndex(
                name: "IX_Secciones_idProfesor",
                table: "Secciones",
                column: "idProfesor");

            migrationBuilder.CreateIndex(
                name: "IX_Secciones_idTurno",
                table: "Secciones",
                column: "idTurno");

            migrationBuilder.AddForeignKey(
                name: "FK_ApoderadoEstudiante_Estudiantes_menoresACargoidEstudiante",
                table: "ApoderadoEstudiante",
                column: "menoresACargoidEstudiante",
                principalTable: "Estudiantes",
                principalColumn: "idEstudiante",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Profesores_idProfesor",
                table: "Estudiantes",
                column: "idProfesor",
                principalTable: "Profesores",
                principalColumn: "idProfesor");

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Secciones_idSeccion",
                table: "Estudiantes",
                column: "idSeccion",
                principalTable: "Secciones",
                principalColumn: "idSeccion");

            migrationBuilder.AddForeignKey(
                name: "FK_Profesores_Secciones_idSeccion",
                table: "Profesores",
                column: "idSeccion",
                principalTable: "Secciones",
                principalColumn: "idSeccion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Secciones_Profesores_idProfesor",
                table: "Secciones");

            migrationBuilder.DropTable(
                name: "ApoderadoEstudiante");

            migrationBuilder.DropTable(
                name: "Apoderados");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Profesores");

            migrationBuilder.DropTable(
                name: "Secciones");

            migrationBuilder.DropTable(
                name: "Turnos");
        }
    }
}

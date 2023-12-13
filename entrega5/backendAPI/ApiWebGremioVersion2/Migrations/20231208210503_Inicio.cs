using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiWebGremioVersion2.Migrations
{
    /// <inheritdoc />
    public partial class Inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MontoAnual",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    año = table.Column<int>(type: "int", nullable: false),
                    monto = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MontoAnual", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Odontologo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dni = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odontologo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Provincia",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincia", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Agremiación",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estadoOdontologo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nroMatricula = table.Column<int>(type: "int", nullable: false),
                    idOdontologo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agremiación", x => x.id);
                    table.ForeignKey(
                        name: "FK_Agremiación_Odontologo_idOdontologo",
                        column: x => x.idOdontologo,
                        principalTable: "Odontologo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Localidad",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigoPostal = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idProvincia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localidad", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Localidad_Provincia_idProvincia",
                        column: x => x.idProvincia,
                        principalTable: "Provincia",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cuota",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    monto = table.Column<double>(type: "float", nullable: false),
                    periodo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estadoCouta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idAgremiacion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuota", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cuota_Agremiación_idAgremiacion",
                        column: x => x.idAgremiacion,
                        principalTable: "Agremiación",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consultorio",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero = table.Column<int>(type: "int", nullable: false),
                    calle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idLocalidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultorio", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Consultorio_Localidad_idLocalidad",
                        column: x => x.idLocalidad,
                        principalTable: "Localidad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsultorioOdontologo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idOdontologo = table.Column<int>(type: "int", nullable: false),
                    idConsultorio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultorioOdontologo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConsultorioOdontologo_Consultorio_idConsultorio",
                        column: x => x.idConsultorio,
                        principalTable: "Consultorio",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsultorioOdontologo_Odontologo_idOdontologo",
                        column: x => x.idOdontologo,
                        principalTable: "Odontologo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agremiación_idOdontologo",
                table: "Agremiación",
                column: "idOdontologo");

            migrationBuilder.CreateIndex(
                name: "IX_Consultorio_idLocalidad",
                table: "Consultorio",
                column: "idLocalidad");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultorioOdontologo_idConsultorio",
                table: "ConsultorioOdontologo",
                column: "idConsultorio");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultorioOdontologo_idOdontologo",
                table: "ConsultorioOdontologo",
                column: "idOdontologo");

            migrationBuilder.CreateIndex(
                name: "IX_Cuota_idAgremiacion",
                table: "Cuota",
                column: "idAgremiacion");

            migrationBuilder.CreateIndex(
                name: "IX_Localidad_idProvincia",
                table: "Localidad",
                column: "idProvincia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsultorioOdontologo");

            migrationBuilder.DropTable(
                name: "Cuota");

            migrationBuilder.DropTable(
                name: "MontoAnual");

            migrationBuilder.DropTable(
                name: "Consultorio");

            migrationBuilder.DropTable(
                name: "Agremiación");

            migrationBuilder.DropTable(
                name: "Localidad");

            migrationBuilder.DropTable(
                name: "Odontologo");

            migrationBuilder.DropTable(
                name: "Provincia");
        }
    }
}

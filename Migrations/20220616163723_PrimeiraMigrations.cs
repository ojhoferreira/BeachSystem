using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProvaDS.Migrations
{
    public partial class PrimeiraMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Armario",
                columns: table => new
                {
                    ArmarioId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    PontoX = table.Column<int>(nullable: false),
                    PontoY = table.Column<int>(nullable: false),
                    AdministradorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Armario", x => x.ArmarioId);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    PessoaId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    CPF = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.PessoaId);
                });

            migrationBuilder.CreateTable(
                name: "Compartimento",
                columns: table => new
                {
                    NumeroId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tamanho = table.Column<string>(nullable: false),
                    ArmarioId = table.Column<int>(nullable: false),
                    Disponivel = table.Column<bool>(nullable: false),
                    PessoaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compartimento", x => x.NumeroId);
                    table.ForeignKey(
                        name: "FK_Compartimento_Armario_ArmarioId",
                        column: x => x.ArmarioId,
                        principalTable: "Armario",
                        principalColumn: "ArmarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Compartimento_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "PessoaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compartimento_ArmarioId",
                table: "Compartimento",
                column: "ArmarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Compartimento_PessoaId",
                table: "Compartimento",
                column: "PessoaId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compartimento");

            migrationBuilder.DropTable(
                name: "Armario");

            migrationBuilder.DropTable(
                name: "Pessoa");
        }
    }
}

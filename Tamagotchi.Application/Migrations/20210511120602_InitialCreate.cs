using Microsoft.EntityFrameworkCore.Migrations;

namespace Tamagotchi.Application.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tamagotchi");

            migrationBuilder.CreateSequence(
                name: "dragonsseq",
                schema: "tamagotchi",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "lifestages",
                schema: "tamagotchi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lifestages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "dragons",
                schema: "tamagotchi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Happiness = table.Column<int>(type: "int", nullable: false),
                    Hunger = table.Column<int>(type: "int", nullable: false),
                    LifeStageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dragons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dragons_lifestages_LifeStageId",
                        column: x => x.LifeStageId,
                        principalSchema: "tamagotchi",
                        principalTable: "lifestages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dragons_LifeStageId",
                schema: "tamagotchi",
                table: "dragons",
                column: "LifeStageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dragons",
                schema: "tamagotchi");

            migrationBuilder.DropTable(
                name: "lifestages",
                schema: "tamagotchi");

            migrationBuilder.DropSequence(
                name: "dragonsseq",
                schema: "tamagotchi");
        }
    }
}

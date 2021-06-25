using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace mfiles.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "core_category",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_core_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "core_bid",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    applicant_id = table.Column<int>(nullable: false),
                    category_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_core_bid", x => x.id);
                    table.ForeignKey(
                        name: "FK_core_bid_core_category_category_id",
                        column: x => x.category_id,
                        principalTable: "core_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "core_category",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "supply of boots" },
                    { 2, "repair of forklifts" },
                    { 3, "containers" }
                });

            migrationBuilder.InsertData(
                table: "core_bid",
                columns: new[] { "id", "applicant_id", "category_id" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 3, 3, 1 },
                    { 6, 8, 1 },
                    { 2, 2, 2 },
                    { 4, 3, 2 },
                    { 7, 8, 2 },
                    { 5, 2, 3 },
                    { 8, 8, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_core_bid_category_id",
                table: "core_bid",
                column: "category_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "core_bid");

            migrationBuilder.DropTable(
                name: "core_category");
        }
    }
}

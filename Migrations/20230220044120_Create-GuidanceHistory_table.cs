using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace icounselvault.Migrations
{
    /// <inheritdoc />
    public partial class CreateGuidanceHistory_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CLIENT_GUIDANCE_HISTORY",
                columns: table => new
                {
                    CLIENT_GUIDANCE_HISTORY_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Client = table.Column<int>(type: "int", nullable: false),
                    Counselor = table.Column<int>(type: "int", nullable: true),
                    EXTERNAL_GUIDANCE_SOURCE = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GUIDANCE_ADVICE = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CLIENT_SATISFACTION = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HISTORY_STATUS = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENT_GUIDANCE_HISTORY", x => x.CLIENT_GUIDANCE_HISTORY_ID);
                    table.ForeignKey(
                        name: "FK_CLIENT_GUIDANCE_HISTORY_CLIENT_Client",
                        column: x => x.Client,
                        principalTable: "CLIENT",
                        principalColumn: "CLIENT_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CLIENT_GUIDANCE_HISTORY_COUNSELOR_Counselor",
                        column: x => x.Counselor,
                        principalTable: "COUNSELOR",
                        principalColumn: "COUNSELOR_ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENT_GUIDANCE_HISTORY_Client",
                table: "CLIENT_GUIDANCE_HISTORY",
                column: "Client");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENT_GUIDANCE_HISTORY_Counselor",
                table: "CLIENT_GUIDANCE_HISTORY",
                column: "Counselor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLIENT_GUIDANCE_HISTORY");
        }
    }
}

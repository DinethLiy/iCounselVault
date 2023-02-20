using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace icounselvault.Migrations
{
    /// <inheritdoc />
    public partial class CreateInsertRequest_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COUNSEL_DATA_INSERT_REQUEST",
                columns: table => new
                {
                    COUNSEL_DATA_INSERT_REQUEST_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CLIENT_GUIDANCE_HISTORY_ID = table.Column<int>(type: "int", nullable: false),
                    CLIENT_ID = table.Column<int>(type: "int", nullable: false),
                    COUNSELOR_ID = table.Column<int>(type: "int", nullable: true),
                    INSERT_REQUEST_STATUS = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    INSERT_REQUEST_REMARK = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COUNSEL_DATA_INSERT_REQUEST", x => x.COUNSEL_DATA_INSERT_REQUEST_ID);
                    table.ForeignKey(
                        name: "FK_COUNSEL_DATA_INSERT_REQUEST_CLIENT_CLIENT_ID",
                        column: x => x.CLIENT_ID,
                        principalTable: "CLIENT",
                        principalColumn: "CLIENT_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_COUNSEL_DATA_INSERT_REQUEST_CLIENT_GUIDANCE_HISTORY_clientGu~",
                        column: x => x.CLIENT_GUIDANCE_HISTORY_ID,
                        principalTable: "CLIENT_GUIDANCE_HISTORY",
                        principalColumn: "CLIENT_GUIDANCE_HISTORY_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_COUNSEL_DATA_INSERT_REQUEST_COUNSELOR_COUNSELOR_ID",
                        column: x => x.COUNSELOR_ID,
                        principalTable: "COUNSELOR",
                        principalColumn: "COUNSELOR_ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_COUNSEL_DATA_INSERT_REQUEST_CLIENT_ID",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                column: "CLIENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COUNSEL_DATA_INSERT_REQUEST_CLIENT_GUID~",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                column: "CLIENT_GUIDANCE_HISTORY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COUNSEL_DATA_INSERT_REQUEST_COUNSELOR_ID",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                column: "COUNSELOR_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COUNSEL_DATA_INSERT_REQUEST");
        }
    }
}

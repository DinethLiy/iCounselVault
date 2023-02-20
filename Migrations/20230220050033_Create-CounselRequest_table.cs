using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace icounselvault.Migrations
{
    /// <inheritdoc />
    public partial class CreateCounselRequest_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CLIENT_GUIDANCE_HISTORY_CLIENT_Client",
                table: "CLIENT_GUIDANCE_HISTORY");

            migrationBuilder.DropForeignKey(
                name: "FK_CLIENT_GUIDANCE_HISTORY_COUNSELOR_Counselor",
                table: "CLIENT_GUIDANCE_HISTORY");

            migrationBuilder.RenameColumn(
                name: "Counselor",
                table: "CLIENT_GUIDANCE_HISTORY",
                newName: "COUNSELOR_ID");

            migrationBuilder.RenameColumn(
                name: "Client",
                table: "CLIENT_GUIDANCE_HISTORY",
                newName: "CLIENT_ID");

            migrationBuilder.RenameIndex(
                name: "IX_CLIENT_GUIDANCE_HISTORY_Counselor",
                table: "CLIENT_GUIDANCE_HISTORY",
                newName: "IX_CLIENT_GUIDANCE_HISTORY_COUNSELOR_ID");

            migrationBuilder.RenameIndex(
                name: "IX_CLIENT_GUIDANCE_HISTORY_Client",
                table: "CLIENT_GUIDANCE_HISTORY",
                newName: "IX_CLIENT_GUIDANCE_HISTORY_CLIENT_ID");

            migrationBuilder.CreateTable(
                name: "COUNSEL_REQUEST",
                columns: table => new
                {
                    COUNSEL_REQUEST_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CLIENT_ID = table.Column<int>(type: "int", nullable: false),
                    COUNSELOR_ID = table.Column<int>(type: "int", nullable: false),
                    FROM_DATE = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TO_DATE = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    COUNSEL_REQUEST_STATUS = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    COUNSEL_REQUEST_REMARK = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COUNSEL_REQUEST", x => x.COUNSEL_REQUEST_ID);
                    table.ForeignKey(
                        name: "FK_COUNSEL_REQUEST_CLIENT_CLIENT_ID",
                        column: x => x.CLIENT_ID,
                        principalTable: "CLIENT",
                        principalColumn: "CLIENT_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_COUNSEL_REQUEST_COUNSELOR_COUNSELOR_ID",
                        column: x => x.COUNSELOR_ID,
                        principalTable: "COUNSELOR",
                        principalColumn: "COUNSELOR_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_COUNSEL_REQUEST_CLIENT_ID",
                table: "COUNSEL_REQUEST",
                column: "CLIENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COUNSEL_REQUEST_COUNSELOR_ID",
                table: "COUNSEL_REQUEST",
                column: "COUNSELOR_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENT_GUIDANCE_HISTORY_CLIENT_CLIENT_ID",
                table: "CLIENT_GUIDANCE_HISTORY",
                column: "CLIENT_ID",
                principalTable: "CLIENT",
                principalColumn: "CLIENT_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENT_GUIDANCE_HISTORY_COUNSELOR_COUNSELOR_ID",
                table: "CLIENT_GUIDANCE_HISTORY",
                column: "COUNSELOR_ID",
                principalTable: "COUNSELOR",
                principalColumn: "COUNSELOR_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CLIENT_GUIDANCE_HISTORY_CLIENT_CLIENT_ID",
                table: "CLIENT_GUIDANCE_HISTORY");

            migrationBuilder.DropForeignKey(
                name: "FK_CLIENT_GUIDANCE_HISTORY_COUNSELOR_COUNSELOR_ID",
                table: "CLIENT_GUIDANCE_HISTORY");

            migrationBuilder.DropTable(
                name: "COUNSEL_REQUEST");

            migrationBuilder.RenameColumn(
                name: "COUNSELOR_ID",
                table: "CLIENT_GUIDANCE_HISTORY",
                newName: "Counselor");

            migrationBuilder.RenameColumn(
                name: "CLIENT_ID",
                table: "CLIENT_GUIDANCE_HISTORY",
                newName: "Client");

            migrationBuilder.RenameIndex(
                name: "IX_CLIENT_GUIDANCE_HISTORY_COUNSELOR_ID",
                table: "CLIENT_GUIDANCE_HISTORY",
                newName: "IX_CLIENT_GUIDANCE_HISTORY_Counselor");

            migrationBuilder.RenameIndex(
                name: "IX_CLIENT_GUIDANCE_HISTORY_CLIENT_ID",
                table: "CLIENT_GUIDANCE_HISTORY",
                newName: "IX_CLIENT_GUIDANCE_HISTORY_Client");

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENT_GUIDANCE_HISTORY_CLIENT_Client",
                table: "CLIENT_GUIDANCE_HISTORY",
                column: "Client",
                principalTable: "CLIENT",
                principalColumn: "CLIENT_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CLIENT_GUIDANCE_HISTORY_COUNSELOR_Counselor",
                table: "CLIENT_GUIDANCE_HISTORY",
                column: "Counselor",
                principalTable: "COUNSELOR",
                principalColumn: "COUNSELOR_ID");
        }
    }
}

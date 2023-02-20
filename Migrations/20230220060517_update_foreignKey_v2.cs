using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace icounselvault.Migrations
{
    /// <inheritdoc />
    public partial class update_foreignKey_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CLIENT_GUIDANCE_HISTORY_CLIENT_CLIENT_ID",
                table: "CLIENT_GUIDANCE_HISTORY");

            migrationBuilder.DropForeignKey(
                name: "FK_CLIENT_GUIDANCE_HISTORY_COUNSELOR_COUNSELOR_ID",
                table: "CLIENT_GUIDANCE_HISTORY");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}

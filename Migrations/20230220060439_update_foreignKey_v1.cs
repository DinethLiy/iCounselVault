using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace icounselvault.Migrations
{
    /// <inheritdoc />
    public partial class update_foreignKey_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_COUNSEL_REQUEST_CLIENT_CLIENT_ID",
                table: "COUNSEL_REQUEST");

            migrationBuilder.DropForeignKey(
                name: "FK_COUNSEL_REQUEST_COUNSELOR_COUNSELOR_ID",
                table: "COUNSEL_REQUEST");

            migrationBuilder.RenameColumn(
                name: "COUNSELOR_ID",
                table: "COUNSEL_REQUEST",
                newName: "Counselor");

            migrationBuilder.RenameColumn(
                name: "CLIENT_ID",
                table: "COUNSEL_REQUEST",
                newName: "Client");

            migrationBuilder.RenameIndex(
                name: "IX_COUNSEL_REQUEST_COUNSELOR_ID",
                table: "COUNSEL_REQUEST",
                newName: "IX_COUNSEL_REQUEST_Counselor");

            migrationBuilder.RenameIndex(
                name: "IX_COUNSEL_REQUEST_CLIENT_ID",
                table: "COUNSEL_REQUEST",
                newName: "IX_COUNSEL_REQUEST_Client");

            migrationBuilder.AddForeignKey(
                name: "FK_COUNSEL_REQUEST_CLIENT_Client",
                table: "COUNSEL_REQUEST",
                column: "Client",
                principalTable: "CLIENT",
                principalColumn: "CLIENT_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_COUNSEL_REQUEST_COUNSELOR_Counselor",
                table: "COUNSEL_REQUEST",
                column: "Counselor",
                principalTable: "COUNSELOR",
                principalColumn: "COUNSELOR_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_COUNSEL_REQUEST_CLIENT_Client",
                table: "COUNSEL_REQUEST");

            migrationBuilder.DropForeignKey(
                name: "FK_COUNSEL_REQUEST_COUNSELOR_Counselor",
                table: "COUNSEL_REQUEST");

            migrationBuilder.RenameColumn(
                name: "Counselor",
                table: "COUNSEL_REQUEST",
                newName: "COUNSELOR_ID");

            migrationBuilder.RenameColumn(
                name: "Client",
                table: "COUNSEL_REQUEST",
                newName: "CLIENT_ID");

            migrationBuilder.RenameIndex(
                name: "IX_COUNSEL_REQUEST_Counselor",
                table: "COUNSEL_REQUEST",
                newName: "IX_COUNSEL_REQUEST_COUNSELOR_ID");

            migrationBuilder.RenameIndex(
                name: "IX_COUNSEL_REQUEST_Client",
                table: "COUNSEL_REQUEST",
                newName: "IX_COUNSEL_REQUEST_CLIENT_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_COUNSEL_REQUEST_CLIENT_CLIENT_ID",
                table: "COUNSEL_REQUEST",
                column: "CLIENT_ID",
                principalTable: "CLIENT",
                principalColumn: "CLIENT_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_COUNSEL_REQUEST_COUNSELOR_COUNSELOR_ID",
                table: "COUNSEL_REQUEST",
                column: "COUNSELOR_ID",
                principalTable: "COUNSELOR",
                principalColumn: "COUNSELOR_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

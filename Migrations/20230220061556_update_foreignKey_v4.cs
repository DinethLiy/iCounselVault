using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace icounselvault.Migrations
{
    /// <inheritdoc />
    public partial class update_foreignKey_v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_COUNSEL_DATA_INSERT_REQUEST_CLIENT_CLIENT_ID",
                table: "COUNSEL_DATA_INSERT_REQUEST");

            migrationBuilder.DropForeignKey(
                name: "FK_COUNSEL_DATA_INSERT_REQUEST_CLIENT_GUIDANCE_HISTORY_clientGu~",
                table: "COUNSEL_DATA_INSERT_REQUEST");

            migrationBuilder.DropForeignKey(
                name: "FK_COUNSEL_DATA_INSERT_REQUEST_COUNSELOR_COUNSELOR_ID",
                table: "COUNSEL_DATA_INSERT_REQUEST");*/

            migrationBuilder.RenameColumn(
                name: "ClientGuidanceHistory",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                newName: "ClientGuidanceHistory");

            migrationBuilder.RenameColumn(
                name: "Counselor",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                newName: "Counselor");

            migrationBuilder.RenameColumn(
                name: "Client",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                newName: "Client");

            /*migrationBuilder.RenameIndex(
                name: "IX_COUNSEL_DATA_INSERT_REQUEST_COUNSELOR_ID",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                newName: "IX_COUNSEL_DATA_INSERT_REQUEST_Counselor");*/

            /*migrationBuilder.RenameIndex(
                name: "IX_COUNSEL_DATA_INSERT_REQUEST_clientGuidanceHistoryCLIENT_GUID~",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                newName: "IX_COUNSEL_DATA_INSERT_REQUEST_ClientGuidanceHistory");*/

            /*migrationBuilder.RenameIndex(
                name: "IX_COUNSEL_DATA_INSERT_REQUEST_CLIENT_ID",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                newName: "IX_COUNSEL_DATA_INSERT_REQUEST_Client");*/

            migrationBuilder.AddForeignKey(
                name: "FK_COUNSEL_DATA_INSERT_REQUEST_CLIENT_Client",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                column: "Client",
                principalTable: "CLIENT",
                principalColumn: "CLIENT_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_COUNSEL_DATA_INSERT_REQUEST_CLIENT_GUIDANCE_HISTORY_ClientGu~",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                column: "ClientGuidanceHistory",
                principalTable: "CLIENT_GUIDANCE_HISTORY",
                principalColumn: "CLIENT_GUIDANCE_HISTORY_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_COUNSEL_DATA_INSERT_REQUEST_COUNSELOR_Counselor",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                column: "Counselor",
                principalTable: "COUNSELOR",
                principalColumn: "COUNSELOR_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_COUNSEL_DATA_INSERT_REQUEST_CLIENT_Client",
                table: "COUNSEL_DATA_INSERT_REQUEST");

            migrationBuilder.DropForeignKey(
                name: "FK_COUNSEL_DATA_INSERT_REQUEST_CLIENT_GUIDANCE_HISTORY_ClientGu~",
                table: "COUNSEL_DATA_INSERT_REQUEST");

            migrationBuilder.DropForeignKey(
                name: "FK_COUNSEL_DATA_INSERT_REQUEST_COUNSELOR_Counselor",
                table: "COUNSEL_DATA_INSERT_REQUEST");

            migrationBuilder.RenameColumn(
                name: "Counselor",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                newName: "COUNSELOR_ID");

            migrationBuilder.RenameColumn(
                name: "ClientGuidanceHistory",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                newName: "CLIENT_GUIDANCE_HISTORY_ID");

            migrationBuilder.RenameColumn(
                name: "Client",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                newName: "CLIENT_ID");

            migrationBuilder.RenameIndex(
                name: "IX_COUNSEL_DATA_INSERT_REQUEST_Counselor",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                newName: "IX_COUNSEL_DATA_INSERT_REQUEST_COUNSELOR_ID");

            migrationBuilder.RenameIndex(
                name: "IX_COUNSEL_DATA_INSERT_REQUEST_ClientGuidanceHistory",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                newName: "IX_COUNSEL_DATA_INSERT_REQUEST_clientGuidanceHistoryCLIENT_GUID~");

            migrationBuilder.RenameIndex(
                name: "IX_COUNSEL_DATA_INSERT_REQUEST_Client",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                newName: "IX_COUNSEL_DATA_INSERT_REQUEST_CLIENT_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_COUNSEL_DATA_INSERT_REQUEST_CLIENT_CLIENT_ID",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                column: "CLIENT_ID",
                principalTable: "CLIENT",
                principalColumn: "CLIENT_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_COUNSEL_DATA_INSERT_REQUEST_CLIENT_GUIDANCE_HISTORY_clientGu~",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                column: "CLIENT_GUIDANCE_HISTORY_ID",
                principalTable: "CLIENT_GUIDANCE_HISTORY",
                principalColumn: "CLIENT_GUIDANCE_HISTORY_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_COUNSEL_DATA_INSERT_REQUEST_COUNSELOR_COUNSELOR_ID",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                column: "COUNSELOR_ID",
                principalTable: "COUNSELOR",
                principalColumn: "COUNSELOR_ID");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace icounselvault.Migrations
{
    /// <inheritdoc />
    public partial class Add_CreatedDate_columns_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CREATED_DATE",
                table: "COUNSEL_REQUEST",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CREATED_DATE",
                table: "COUNSEL_DATA_INSERT_REQUEST",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CREATED_DATE",
                table: "CLIENT_GUIDANCE_HISTORY",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CREATED_DATE",
                table: "COUNSEL_REQUEST");

            migrationBuilder.DropColumn(
                name: "CREATED_DATE",
                table: "COUNSEL_DATA_INSERT_REQUEST");

            migrationBuilder.DropColumn(
                name: "CREATED_DATE",
                table: "CLIENT_GUIDANCE_HISTORY");
        }
    }
}

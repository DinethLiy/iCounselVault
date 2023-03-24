using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace icounselvault.Migrations
{
    /// <inheritdoc />
    public partial class add_SurveyCount_column_to_clientExperience : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SURVEY_TAKEN_AMOUNT",
                table: "CLIENT_EXPERIENCE",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SURVEY_TAKEN_AMOUNT",
                table: "CLIENT_EXPERIENCE");
        }
    }
}

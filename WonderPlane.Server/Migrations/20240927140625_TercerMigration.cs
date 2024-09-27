using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WonderPlane.Server.Migrations
{
    /// <inheritdoc />
    public partial class TercerMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                schema: "WonderPlane",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                schema: "WonderPlane",
                table: "Users");
        }
    }
}

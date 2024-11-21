using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WonderPlane.Server.Migrations
{
    /// <inheritdoc />
    public partial class PromotionAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                schema: "WonderPlane",
                table: "Users",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                schema: "WonderPlane",
                table: "Cards",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                schema: "WonderPlane",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Balance",
                schema: "WonderPlane",
                table: "Cards");
        }
    }
}

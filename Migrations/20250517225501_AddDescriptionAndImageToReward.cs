using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameArena.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionAndImageToReward : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Rewards",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "Rewards",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Rewards");

            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "Rewards");
        }
    }
}

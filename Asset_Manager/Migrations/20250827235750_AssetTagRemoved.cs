using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asset_Manager.Migrations
{
    /// <inheritdoc />
    public partial class AssetTagRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetTag",
                table: "AssetAssignment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssetTag",
                table: "AssetAssignment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

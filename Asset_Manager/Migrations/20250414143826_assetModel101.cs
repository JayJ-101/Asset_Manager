using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asset_Manager.Migrations
{
    /// <inheritdoc />
    public partial class assetModel101 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_SupplierId",
                table: "Assets",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Supplier_SupplierId",
                table: "Assets",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Supplier_SupplierId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_SupplierId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Assets");
        }
    }
}

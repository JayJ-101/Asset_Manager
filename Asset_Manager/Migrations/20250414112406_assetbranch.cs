using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asset_Manager.Migrations
{
    /// <inheritdoc />
    public partial class assetbranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Branch_BranchId",
                table: "Assets");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "Assets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Branch_BranchId",
                table: "Assets",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Branch_BranchId",
                table: "Assets");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Branch_BranchId",
                table: "Assets",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

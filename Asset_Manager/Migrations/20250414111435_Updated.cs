using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asset_Manager.Migrations
{
    /// <inheritdoc />
    public partial class Updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_PurchaseOrders_PurchaseOrderId",
                table: "Assets");

            migrationBuilder.DropTable(
                name: "PurchaseOrderItem");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_Assets_PurchaseOrderId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderId",
                table: "Assets");

            migrationBuilder.CreateTable(
                name: "AssetAssignment",
                columns: table => new
                {
                    AssingmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<int>(type: "int", nullable: false),
                    Employee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    AssginedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetAssignment", x => x.AssingmentId);
                    table.ForeignKey(
                        name: "FK_AssetAssignment_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "AssetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetAssignment_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetAssignment_AssetId",
                table: "AssetAssignment",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetAssignment_BranchId",
                table: "AssetAssignment",
                column: "BranchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetAssignment");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseOrderId",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.PurchaseOrderId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderItem",
                columns: table => new
                {
                    OrderITemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderItem", x => x.OrderITemId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItem_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItem_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "PurchaseOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_PurchaseOrderId",
                table: "Assets",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItem_CategoryId",
                table: "PurchaseOrderItem",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItem_PurchaseOrderId",
                table: "PurchaseOrderItem",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_SupplierId",
                table: "PurchaseOrders",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_PurchaseOrders_PurchaseOrderId",
                table: "Assets",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "PurchaseOrderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BundleTree.Migrations
{
    /// <inheritdoc />
    public partial class Initial_create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bundles",
                columns: table => new
                {
                    BundleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BundleName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bundles", x => x.BundleId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "BundleParts",
                columns: table => new
                {
                    BundlePartId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BundleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: true),
                    SubBundleId = table.Column<Guid>(type: "TEXT", nullable: true),
                    RequiredUnits = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BundleParts", x => x.BundlePartId);
                    table.ForeignKey(
                        name: "FK_BundleParts_Bundles_BundleId",
                        column: x => x.BundleId,
                        principalTable: "Bundles",
                        principalColumn: "BundleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BundleParts_Bundles_SubBundleId",
                        column: x => x.SubBundleId,
                        principalTable: "Bundles",
                        principalColumn: "BundleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BundleParts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BundleParts_BundleId",
                table: "BundleParts",
                column: "BundleId");

            migrationBuilder.CreateIndex(
                name: "IX_BundleParts_ProductId",
                table: "BundleParts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BundleParts_SubBundleId",
                table: "BundleParts",
                column: "SubBundleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BundleParts");

            migrationBuilder.DropTable(
                name: "Bundles");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}

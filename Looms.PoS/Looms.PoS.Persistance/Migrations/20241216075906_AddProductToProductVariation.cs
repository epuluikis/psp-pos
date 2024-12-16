using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Looms.PoS.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddProductToProductVariation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductVariations_ProductId",
                table: "ProductVariations",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariations_Products_ProductId",
                table: "ProductVariations",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariations_Products_ProductId",
                table: "ProductVariations");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariations_ProductId",
                table: "ProductVariations");
        }
    }
}

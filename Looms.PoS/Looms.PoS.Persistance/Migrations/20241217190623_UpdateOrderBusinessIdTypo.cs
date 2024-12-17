using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Looms.PoS.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderBusinessIdTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Businesses_BussinessId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "BussinessId",
                table: "Orders",
                newName: "BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_BussinessId",
                table: "Orders",
                newName: "IX_Orders_BusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Businesses_BusinessId",
                table: "Orders",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Businesses_BusinessId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "Orders",
                newName: "BussinessId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_BusinessId",
                table: "Orders",
                newName: "IX_Orders_BussinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Businesses_BussinessId",
                table: "Orders",
                column: "BussinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Looms.PoS.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePersistanceLayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Discounts_DiscountId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductVariations_ProductVariationId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Services_ServiceId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Discounts_DiscountId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Refunds_Orders_OrderId",
                table: "Refunds");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ProductVariationId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "IssuedBy",
                table: "GiftCards",
                newName: "IssuedById");

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "Taxes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "DiscountId",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "Discounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Taxes_BusinessId",
                table: "Taxes",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Refunds_PaymentId",
                table: "Refunds",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Refunds_UserId",
                table: "Refunds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftCards_IssuedById",
                table: "GiftCards",
                column: "IssuedById");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_BusinessId",
                table: "Discounts",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_ProductId",
                table: "Discounts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Businesses_BusinessId",
                table: "Discounts",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Products_ProductId",
                table: "Discounts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GiftCards_Users_IssuedById",
                table: "GiftCards",
                column: "IssuedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Discounts_DiscountId",
                table: "OrderItems",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductVariations_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "ProductVariations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Services_ServiceId",
                table: "OrderItems",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Discounts_DiscountId",
                table: "Orders",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Refunds_Orders_OrderId",
                table: "Refunds",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Refunds_Payments_PaymentId",
                table: "Refunds",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Refunds_Users_UserId",
                table: "Refunds",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Taxes_Businesses_BusinessId",
                table: "Taxes",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Businesses_BusinessId",
                table: "Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Products_ProductId",
                table: "Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_GiftCards_Users_IssuedById",
                table: "GiftCards");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Discounts_DiscountId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductVariations_ProductId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Services_ServiceId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Discounts_DiscountId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Refunds_Orders_OrderId",
                table: "Refunds");

            migrationBuilder.DropForeignKey(
                name: "FK_Refunds_Payments_PaymentId",
                table: "Refunds");

            migrationBuilder.DropForeignKey(
                name: "FK_Refunds_Users_UserId",
                table: "Refunds");

            migrationBuilder.DropForeignKey(
                name: "FK_Taxes_Businesses_BusinessId",
                table: "Taxes");

            migrationBuilder.DropIndex(
                name: "IX_Taxes_BusinessId",
                table: "Taxes");

            migrationBuilder.DropIndex(
                name: "IX_Refunds_PaymentId",
                table: "Refunds");

            migrationBuilder.DropIndex(
                name: "IX_Refunds_UserId",
                table: "Refunds");

            migrationBuilder.DropIndex(
                name: "IX_GiftCards_IssuedById",
                table: "GiftCards");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_BusinessId",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_ProductId",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Taxes");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Discounts");

            migrationBuilder.RenameColumn(
                name: "IssuedById",
                table: "GiftCards",
                newName: "IssuedBy");

            migrationBuilder.AlterColumn<Guid>(
                name: "DiscountId",
                table: "Orders",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductVariationId",
                table: "OrderItems",
                column: "ProductVariationId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Discounts_DiscountId",
                table: "OrderItems",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductVariations_ProductVariationId",
                table: "OrderItems",
                column: "ProductVariationId",
                principalTable: "ProductVariations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Services_ServiceId",
                table: "OrderItems",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Discounts_DiscountId",
                table: "Orders",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Refunds_Orders_OrderId",
                table: "Refunds",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}

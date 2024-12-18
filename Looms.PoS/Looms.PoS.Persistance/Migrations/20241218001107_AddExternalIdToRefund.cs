using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Looms.PoS.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddExternalIdToRefund : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Refunds",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Refunds");
        }
    }
}

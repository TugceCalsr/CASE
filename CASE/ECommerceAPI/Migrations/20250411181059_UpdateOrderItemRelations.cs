using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderItemRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "OrderItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Order",
                table: "OrderItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

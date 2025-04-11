using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixDecimalPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Orders",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Customers",
                newName: "Name");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Orders",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Customers",
                newName: "FullName");
        }
    }
}

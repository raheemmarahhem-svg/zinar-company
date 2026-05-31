using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZinarCompany.Migrations
{
    /// <inheritdoc />
    public partial class AddQuoteItemQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "QuoteItems");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "QuoteItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "QuoteItems");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "QuoteItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

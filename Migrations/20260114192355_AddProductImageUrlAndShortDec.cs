using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZinarCompany.Migrations
{
    /// <inheritdoc />
    public partial class AddProductImageUrlAndShortDec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDec",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDec",
                table: "Products");
        }
    }
}

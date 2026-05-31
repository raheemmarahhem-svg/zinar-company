using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZinarCompany.Migrations
{
    /// <inheritdoc />
    public partial class AddQuoteRequestItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteItems_QuoteRequests_QuoteRequestId",
                table: "QuoteItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuoteRequests",
                table: "QuoteRequests");

            migrationBuilder.RenameTable(
                name: "QuoteRequests",
                newName: "QuoteRequest");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuoteRequest",
                table: "QuoteRequest",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "QuoteRequestItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteRequestId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteRequestItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuoteRequestItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuoteRequestItem_QuoteRequest_QuoteRequestId",
                        column: x => x.QuoteRequestId,
                        principalTable: "QuoteRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestItem_ProductId",
                table: "QuoteRequestItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestItem_QuoteRequestId",
                table: "QuoteRequestItem",
                column: "QuoteRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteItems_QuoteRequest_QuoteRequestId",
                table: "QuoteItems",
                column: "QuoteRequestId",
                principalTable: "QuoteRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteItems_QuoteRequest_QuoteRequestId",
                table: "QuoteItems");

            migrationBuilder.DropTable(
                name: "QuoteRequestItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuoteRequest",
                table: "QuoteRequest");

            migrationBuilder.RenameTable(
                name: "QuoteRequest",
                newName: "QuoteRequests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuoteRequests",
                table: "QuoteRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteItems_QuoteRequests_QuoteRequestId",
                table: "QuoteItems",
                column: "QuoteRequestId",
                principalTable: "QuoteRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZinarCompany.Migrations
{
    /// <inheritdoc />
    public partial class AddQuoteRequestTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteItems_QuoteRequest_QuoteRequestId",
                table: "QuoteItems");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestItem_Products_ProductId",
                table: "QuoteRequestItem");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestItem_QuoteRequest_QuoteRequestId",
                table: "QuoteRequestItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuoteRequestItem",
                table: "QuoteRequestItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuoteRequest",
                table: "QuoteRequest");

            migrationBuilder.RenameTable(
                name: "QuoteRequestItem",
                newName: "QuoteRequestItems");

            migrationBuilder.RenameTable(
                name: "QuoteRequest",
                newName: "QuoteRequests");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteRequestItem_QuoteRequestId",
                table: "QuoteRequestItems",
                newName: "IX_QuoteRequestItems_QuoteRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteRequestItem_ProductId",
                table: "QuoteRequestItems",
                newName: "IX_QuoteRequestItems_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuoteRequestItems",
                table: "QuoteRequestItems",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestItems_Products_ProductId",
                table: "QuoteRequestItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestItems_QuoteRequests_QuoteRequestId",
                table: "QuoteRequestItems",
                column: "QuoteRequestId",
                principalTable: "QuoteRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteItems_QuoteRequests_QuoteRequestId",
                table: "QuoteItems");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestItems_Products_ProductId",
                table: "QuoteRequestItems");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRequestItems_QuoteRequests_QuoteRequestId",
                table: "QuoteRequestItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuoteRequests",
                table: "QuoteRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuoteRequestItems",
                table: "QuoteRequestItems");

            migrationBuilder.RenameTable(
                name: "QuoteRequests",
                newName: "QuoteRequest");

            migrationBuilder.RenameTable(
                name: "QuoteRequestItems",
                newName: "QuoteRequestItem");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteRequestItems_QuoteRequestId",
                table: "QuoteRequestItem",
                newName: "IX_QuoteRequestItem_QuoteRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_QuoteRequestItems_ProductId",
                table: "QuoteRequestItem",
                newName: "IX_QuoteRequestItem_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuoteRequest",
                table: "QuoteRequest",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuoteRequestItem",
                table: "QuoteRequestItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteItems_QuoteRequest_QuoteRequestId",
                table: "QuoteItems",
                column: "QuoteRequestId",
                principalTable: "QuoteRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestItem_Products_ProductId",
                table: "QuoteRequestItem",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRequestItem_QuoteRequest_QuoteRequestId",
                table: "QuoteRequestItem",
                column: "QuoteRequestId",
                principalTable: "QuoteRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

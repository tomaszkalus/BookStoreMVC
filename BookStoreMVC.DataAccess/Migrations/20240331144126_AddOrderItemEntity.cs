using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreMVC.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderItemEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProductShoppingCarts_Orders_OrderId",
                table: "UserProductShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_UserProductShoppingCarts_OrderId",
                table: "UserProductShoppingCarts");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "UserProductShoppingCarts");

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "UserProductShoppingCarts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProductShoppingCarts_OrderId",
                table: "UserProductShoppingCarts",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProductShoppingCarts_Orders_OrderId",
                table: "UserProductShoppingCarts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}

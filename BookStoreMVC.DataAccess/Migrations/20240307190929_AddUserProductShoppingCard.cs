using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreMVC.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProductShoppingCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProductShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProductShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProductShoppingCarts_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProductShoppingCarts_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProductShoppingCarts_productId",
                table: "UserProductShoppingCarts",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProductShoppingCarts_userId",
                table: "UserProductShoppingCarts",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProductShoppingCarts");
        }
    }
}

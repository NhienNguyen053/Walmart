using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Walmart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Customer_Id",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_Category_Id",
                table: "SubCategories",
                column: "Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_ProductId",
                table: "ShoppingCarts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategory_Id",
                table: "Products",
                column: "SubCategory_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPhotos_Product_Id",
                table: "ProductPhotos",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Customer_Id",
                table: "Orders",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_Order_Id",
                table: "OrderDetails",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_Product_Id",
                table: "OrderDetails",
                column: "Product_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_Order_Id",
                table: "OrderDetails",
                column: "Order_Id",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_Product_Id",
                table: "OrderDetails",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ApplicationUsers_Customer_Id",
                table: "Orders",
                column: "Customer_Id",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPhotos_Products_Product_Id",
                table: "ProductPhotos",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategories_SubCategory_Id",
                table: "Products",
                column: "SubCategory_Id",
                principalTable: "SubCategories",
                principalColumn: "SubCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_ApplicationUsers_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Products_ProductId",
                table: "ShoppingCarts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Categories_Category_Id",
                table: "SubCategories",
                column: "Category_Id",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_Order_Id",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_Product_Id",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ApplicationUsers_Customer_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPhotos_Products_Product_Id",
                table: "ProductPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategories_SubCategory_Id",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_ApplicationUsers_UserId",
                table: "ShoppingCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Products_ProductId",
                table: "ShoppingCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Categories_Category_Id",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_SubCategories_Category_Id",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_ProductId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubCategory_Id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductPhotos_Product_Id",
                table: "ProductPhotos");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Customer_Id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_Order_Id",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_Product_Id",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<string>(
                name: "Customer_Id",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}

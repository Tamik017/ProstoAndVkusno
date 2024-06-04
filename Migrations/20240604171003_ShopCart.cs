using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProstoAndVkusno.Migrations
{
    /// <inheritdoc />
    public partial class ShopCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_shopCartItems",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productID = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    ShopCartId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__shopCartItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK__shopCartItems__products_productID",
                        column: x => x.productID,
                        principalTable: "_products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX__shopCartItems_productID",
                table: "_shopCartItems",
                column: "productID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_shopCartItems");
        }
    }
}

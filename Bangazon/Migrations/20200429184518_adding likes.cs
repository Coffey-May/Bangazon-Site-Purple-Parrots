using Microsoft.EntityFrameworkCore.Migrations;

namespace Bangazon.Migrations
{
    public partial class addinglikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "ProductType",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.CreateTable(
                name: "LikeProduct",
                columns: table => new
                {
                    LikeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    Like = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeProduct", x => x.LikeId);
                    table.ForeignKey(
                        name: "FK_LikeProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeProduct_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8e4e6291-70d4-4363-afb3-e6388c7d13f2", "AQAAAAEAACcQAAAAENY8o0YvSlVOqnWhFq7BIcqUx243MavfBcxK+xZdWWuQc0r/HxlicMJVS4mmB9E9ZQ==" });

            migrationBuilder.InsertData(
                table: "LikeProduct",
                columns: new[] { "LikeId", "Like", "ProductId", "UserId" },
                values: new object[] { 1, true, 2, "00000000-ffff-ffff-ffff-ffffffffffff" });

            migrationBuilder.CreateIndex(
                name: "IX_LikeProduct_ProductId",
                table: "LikeProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeProduct_UserId",
                table: "LikeProduct",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeProduct");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "ProductType",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a1e37600-c7bf-49e0-95c9-bfbc5496e298", "AQAAAAEAACcQAAAAEMiqi2c0rATEilZzGL+dCdxySJEMOFZwqWLOYbDOKZZro+KNfjzrKW3i+E4SZYEToQ==" });
        }
    }
}

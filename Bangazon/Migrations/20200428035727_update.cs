using Microsoft.EntityFrameworkCore.Migrations;

namespace Bangazon.Migrations
{
    public partial class update : Migration
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

            migrationBuilder.AddColumn<int>(
                name: "ProductListId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ProductCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductList", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "35db65d9-8db4-4b18-83b8-708a56e86d1b", "AQAAAAEAACcQAAAAEFbSfe3BvEpV42X7QjFWNRUuaHedT7Rk8/Q2wrkBDbOP0aZCKqbMbKlExaBnk4UcMw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductListId",
                table: "Product",
                column: "ProductListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductList_ProductListId",
                table: "Product",
                column: "ProductListId",
                principalTable: "ProductList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductList_ProductListId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "ProductList");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductListId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductListId",
                table: "Product");

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
                values: new object[] { "d3a3f89f-1d18-41d1-84a6-989a7ae90dce", "AQAAAAEAACcQAAAAELPuCbLoTcpmFfuifZ9uzZsTpMZOm61V1gXuPG/P9ksZLR7bs8NuQf5122rGEzh7sA==" });
        }
    }
}

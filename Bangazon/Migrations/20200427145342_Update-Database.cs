using Microsoft.EntityFrameworkCore.Migrations;

namespace Bangazon.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "64cb41ca-b07c-4a02-a9f0-da5c76e7ed3b", "AQAAAAEAACcQAAAAEEmMQkI2rbyYkwQLlvgRyVzf+wK/vjdeck5x42wFC9GySk9z/852yPNtHj+q4tWf/A==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d3a3f89f-1d18-41d1-84a6-989a7ae90dce", "AQAAAAEAACcQAAAAELPuCbLoTcpmFfuifZ9uzZsTpMZOm61V1gXuPG/P9ksZLR7bs8NuQf5122rGEzh7sA==" });
        }
    }
}

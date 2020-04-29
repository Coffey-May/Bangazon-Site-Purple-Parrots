using Microsoft.EntityFrameworkCore.Migrations;

namespace Bangazon.Migrations
{
    public partial class BangazonTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bf2d8674-3941-4f4e-b006-2f6006f3bdaa", "AQAAAAEAACcQAAAAEFPLvQfZsNDauTk6td53UDAnTt0O9KxhO24f6kjdTnbCcpWUV302/mMqxUbcyS2Glw==" });
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

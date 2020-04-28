using Microsoft.EntityFrameworkCore.Migrations;

namespace Bangazon.Migrations
{
    public partial class initiaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a1e37600-c7bf-49e0-95c9-bfbc5496e298", "AQAAAAEAACcQAAAAEMiqi2c0rATEilZzGL+dCdxySJEMOFZwqWLOYbDOKZZro+KNfjzrKW3i+E4SZYEToQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "64cb41ca-b07c-4a02-a9f0-da5c76e7ed3b", "AQAAAAEAACcQAAAAEEmMQkI2rbyYkwQLlvgRyVzf+wK/vjdeck5x42wFC9GySk9z/852yPNtHj+q4tWf/A==" });
        }
    }
}

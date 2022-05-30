using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lottery.Migrations
{
    public partial class order1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Draws",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Draws");
        }
    }
}

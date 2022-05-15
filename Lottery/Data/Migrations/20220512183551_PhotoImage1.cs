using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lottery.Data.Migrations
{
    public partial class PhotoImage1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoPath",
                table: "Photos",
                newName: "Name");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Photos",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Photos",
                newName: "PhotoPath");
        }
    }
}

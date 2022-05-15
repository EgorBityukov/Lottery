using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lottery.Data.Migrations
{
    public partial class CreateDatabaseSchema1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Building = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    PhotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.PhotoId);
                });

            migrationBuilder.CreateTable(
                name: "Lots",
                columns: table => new
                {
                    LotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TicketCount = table.Column<int>(type: "int", nullable: true),
                    TicketPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PhotoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lots", x => x.LotId);
                    table.ForeignKey(
                        name: "FK_Lots_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "PhotoId");
                });

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PhotoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInfos_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId");
                    table.ForeignKey(
                        name: "FK_UserInfos_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "PhotoId");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LotId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Lots_LotId",
                        column: x => x.LotId,
                        principalTable: "Lots",
                        principalColumn: "LotId");
                    table.ForeignKey(
                        name: "FK_Tickets_UserInfos_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Draws",
                columns: table => new
                {
                    DrawId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WinnerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LotId = table.Column<int>(type: "int", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Draws", x => x.DrawId);
                    table.ForeignKey(
                        name: "FK_Draws_Lots_LotId",
                        column: x => x.LotId,
                        principalTable: "Lots",
                        principalColumn: "LotId");
                    table.ForeignKey(
                        name: "FK_Draws_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId");
                    table.ForeignKey(
                        name: "FK_Draws_UserInfos_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "UserInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Draws_LotId",
                table: "Draws",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_Draws_TicketId",
                table: "Draws",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Draws_WinnerId",
                table: "Draws",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_PhotoId",
                table: "Lots",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_LotId",
                table: "Tickets",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_AddressId",
                table: "UserInfos",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_PhotoId",
                table: "UserInfos",
                column: "PhotoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Draws");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Lots");

            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}

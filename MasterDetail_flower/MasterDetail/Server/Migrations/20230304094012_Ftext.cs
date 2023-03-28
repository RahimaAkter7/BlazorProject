using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterDetail.Server.Migrations
{
    public partial class Ftext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BouquetTypes",
                columns: table => new
                {
                    BouquetTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BouquetTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BouquetTypes", x => x.BouquetTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Flowers",
                columns: table => new
                {
                    FlowerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoreDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreAvaible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flowers", x => x.FlowerId);
                });

            migrationBuilder.CreateTable(
                name: "StoreEntries",
                columns: table => new
                {
                    StoreEntryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowerId = table.Column<int>(type: "int", nullable: false),
                    BouquetTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreEntries", x => x.StoreEntryId);
                    table.ForeignKey(
                        name: "FK_StoreEntries_BouquetTypes_BouquetTypeId",
                        column: x => x.BouquetTypeId,
                        principalTable: "BouquetTypes",
                        principalColumn: "BouquetTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreEntries_Flowers_FlowerId",
                        column: x => x.FlowerId,
                        principalTable: "Flowers",
                        principalColumn: "FlowerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreEntries_BouquetTypeId",
                table: "StoreEntries",
                column: "BouquetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreEntries_FlowerId",
                table: "StoreEntries",
                column: "FlowerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreEntries");

            migrationBuilder.DropTable(
                name: "BouquetTypes");

            migrationBuilder.DropTable(
                name: "Flowers");
        }
    }
}

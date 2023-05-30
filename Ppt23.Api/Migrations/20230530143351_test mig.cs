using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ppt23.Api.Migrations
{
    /// <inheritdoc />
    public partial class testmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "pracovnikId",
                table: "Ukons",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Pracovniks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Job = table.Column<string>(type: "TEXT", nullable: false),
                    VybaveniId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracovniks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pracovniks_Vybavenis_VybaveniId",
                        column: x => x.VybaveniId,
                        principalTable: "Vybavenis",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ukons_pracovnikId",
                table: "Ukons",
                column: "pracovnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Pracovniks_VybaveniId",
                table: "Pracovniks",
                column: "VybaveniId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ukons_Pracovniks_pracovnikId",
                table: "Ukons",
                column: "pracovnikId",
                principalTable: "Pracovniks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ukons_Pracovniks_pracovnikId",
                table: "Ukons");

            migrationBuilder.DropTable(
                name: "Pracovniks");

            migrationBuilder.DropIndex(
                name: "IX_Ukons_pracovnikId",
                table: "Ukons");

            migrationBuilder.DropColumn(
                name: "pracovnikId",
                table: "Ukons");
        }
    }
}

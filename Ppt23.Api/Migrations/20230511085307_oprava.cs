using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ppt23.Api.Migrations
{
    /// <inheritdoc />
    public partial class oprava : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Vybavenis",
                table: "Vybavenis");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vybavenis",
                table: "Vybavenis",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Vybavenis",
                table: "Vybavenis");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vybavenis",
                table: "Vybavenis",
                column: "Name");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomBackApi.Migrations
{
    /// <inheritdoc />
    public partial class V6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_objstate_userId",
                schema: "Diplom",
                table: "objstate");

            migrationBuilder.DropColumn(
                name: "userId",
                schema: "Diplom",
                table: "objstate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                schema: "Diplom",
                table: "objstate",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_objstate_userId",
                schema: "Diplom",
                table: "objstate",
                column: "userId");
        }
    }
}

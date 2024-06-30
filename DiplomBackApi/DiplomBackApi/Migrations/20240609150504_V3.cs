using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiplomBackApi.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_favoriteobj",
                schema: "Diplom",
                table: "favoriteobj");

            migrationBuilder.DropIndex(
                name: "IX_favoriteobj_UserId",
                schema: "Diplom",
                table: "favoriteobj");

            migrationBuilder.DropColumn(
                name: "id",
                schema: "Diplom",
                table: "favoriteobj");

            migrationBuilder.AddPrimaryKey(
                name: "PK_favoriteobj",
                schema: "Diplom",
                table: "favoriteobj",
                columns: new[] { "UserId", "ObjId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_favoriteobj",
                schema: "Diplom",
                table: "favoriteobj");

            migrationBuilder.AddColumn<int>(
                name: "id",
                schema: "Diplom",
                table: "favoriteobj",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_favoriteobj",
                schema: "Diplom",
                table: "favoriteobj",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_favoriteobj_UserId",
                schema: "Diplom",
                table: "favoriteobj",
                column: "UserId");
        }
    }
}

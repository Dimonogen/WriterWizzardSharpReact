using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomBackApi.Migrations
{
    /// <inheritdoc />
    public partial class V7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userSettings",
                schema: "Diplom",
                columns: table => new
                {
                    userId = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userSettings", x => new { x.userId, x.code });
                });

            migrationBuilder.CreateIndex(
                name: "IX_userSettings_userId_code",
                schema: "Diplom",
                table: "userSettings",
                columns: new[] { "userId", "code" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userSettings",
                schema: "Diplom");
        }
    }
}

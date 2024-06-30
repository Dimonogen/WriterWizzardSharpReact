using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomBackApi.Migrations
{
    /// <inheritdoc />
    public partial class V4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "linkobj",
                schema: "Diplom",
                columns: table => new
                {
                    ObjParentId = table.Column<int>(type: "integer", nullable: false),
                    ObjChildId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_linkobj", x => new { x.ObjParentId, x.ObjChildId });
                    table.ForeignKey(
                        name: "FK_linkobj_obj_ObjChildId",
                        column: x => x.ObjChildId,
                        principalSchema: "Diplom",
                        principalTable: "obj",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_linkobj_obj_ObjParentId",
                        column: x => x.ObjParentId,
                        principalSchema: "Diplom",
                        principalTable: "obj",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_linkobj_ObjChildId",
                schema: "Diplom",
                table: "linkobj",
                column: "ObjChildId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "linkobj",
                schema: "Diplom");
        }
    }
}

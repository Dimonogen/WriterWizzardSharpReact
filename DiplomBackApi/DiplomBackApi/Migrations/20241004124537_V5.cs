using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiplomBackApi.Migrations
{
    /// <inheritdoc />
    public partial class V5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_menuelement_right_rightId",
                schema: "Diplom",
                table: "menuelement");

            migrationBuilder.DropForeignKey(
                name: "FK_objstatetransition_right_right",
                schema: "Diplom",
                table: "objstatetransition");

            migrationBuilder.DropTable(
                name: "rightrole",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "userrole",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "right",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "role",
                schema: "Diplom");

            migrationBuilder.RenameColumn(
                name: "right",
                schema: "Diplom",
                table: "objstatetransition",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_objstatetransition_right",
                schema: "Diplom",
                table: "objstatetransition",
                newName: "IX_objstatetransition_userId");

            migrationBuilder.RenameColumn(
                name: "rightId",
                schema: "Diplom",
                table: "menuelement",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_menuelement_rightId",
                schema: "Diplom",
                table: "menuelement",
                newName: "IX_menuelement_userId");

            migrationBuilder.AddColumn<string>(
                name: "project",
                schema: "Diplom",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "userId",
                schema: "Diplom",
                table: "objtypeattribute",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                schema: "Diplom",
                table: "objtype",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                schema: "Diplom",
                table: "objstate",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                schema: "Diplom",
                table: "objattribute",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                schema: "Diplom",
                table: "objadditionalattribute",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                schema: "Diplom",
                table: "obj",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                schema: "Diplom",
                table: "linkobj",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                schema: "Diplom",
                table: "attributetype",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_objtypeattribute_userId",
                schema: "Diplom",
                table: "objtypeattribute",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_objtype_userId",
                schema: "Diplom",
                table: "objtype",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_objstate_userId",
                schema: "Diplom",
                table: "objstate",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_objattribute_userId",
                schema: "Diplom",
                table: "objattribute",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_objadditionalattribute_userId",
                schema: "Diplom",
                table: "objadditionalattribute",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_obj_userId",
                schema: "Diplom",
                table: "obj",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_linkobj_userId",
                schema: "Diplom",
                table: "linkobj",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_favoriteobj_UserId",
                schema: "Diplom",
                table: "favoriteobj",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_attributetype_userId",
                schema: "Diplom",
                table: "attributetype",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_objtypeattribute_userId",
                schema: "Diplom",
                table: "objtypeattribute");

            migrationBuilder.DropIndex(
                name: "IX_objtype_userId",
                schema: "Diplom",
                table: "objtype");

            migrationBuilder.DropIndex(
                name: "IX_objstate_userId",
                schema: "Diplom",
                table: "objstate");

            migrationBuilder.DropIndex(
                name: "IX_objattribute_userId",
                schema: "Diplom",
                table: "objattribute");

            migrationBuilder.DropIndex(
                name: "IX_objadditionalattribute_userId",
                schema: "Diplom",
                table: "objadditionalattribute");

            migrationBuilder.DropIndex(
                name: "IX_obj_userId",
                schema: "Diplom",
                table: "obj");

            migrationBuilder.DropIndex(
                name: "IX_linkobj_userId",
                schema: "Diplom",
                table: "linkobj");

            migrationBuilder.DropIndex(
                name: "IX_favoriteobj_UserId",
                schema: "Diplom",
                table: "favoriteobj");

            migrationBuilder.DropIndex(
                name: "IX_attributetype_userId",
                schema: "Diplom",
                table: "attributetype");

            migrationBuilder.DropColumn(
                name: "project",
                schema: "Diplom",
                table: "user");

            migrationBuilder.DropColumn(
                name: "userId",
                schema: "Diplom",
                table: "objtypeattribute");

            migrationBuilder.DropColumn(
                name: "userId",
                schema: "Diplom",
                table: "objtype");

            migrationBuilder.DropColumn(
                name: "userId",
                schema: "Diplom",
                table: "objstate");

            migrationBuilder.DropColumn(
                name: "userId",
                schema: "Diplom",
                table: "objattribute");

            migrationBuilder.DropColumn(
                name: "userId",
                schema: "Diplom",
                table: "objadditionalattribute");

            migrationBuilder.DropColumn(
                name: "userId",
                schema: "Diplom",
                table: "obj");

            migrationBuilder.DropColumn(
                name: "userId",
                schema: "Diplom",
                table: "linkobj");

            migrationBuilder.DropColumn(
                name: "userId",
                schema: "Diplom",
                table: "attributetype");

            migrationBuilder.RenameColumn(
                name: "userId",
                schema: "Diplom",
                table: "objstatetransition",
                newName: "right");

            migrationBuilder.RenameIndex(
                name: "IX_objstatetransition_userId",
                schema: "Diplom",
                table: "objstatetransition",
                newName: "IX_objstatetransition_right");

            migrationBuilder.RenameColumn(
                name: "userId",
                schema: "Diplom",
                table: "menuelement",
                newName: "rightId");

            migrationBuilder.RenameIndex(
                name: "IX_menuelement_userId",
                schema: "Diplom",
                table: "menuelement",
                newName: "IX_menuelement_rightId");

            migrationBuilder.CreateTable(
                name: "right",
                schema: "Diplom",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_right", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                schema: "Diplom",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rightrole",
                schema: "Diplom",
                columns: table => new
                {
                    rightId = table.Column<int>(type: "integer", nullable: false),
                    roleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rightrole", x => new { x.rightId, x.roleId });
                    table.ForeignKey(
                        name: "FK_rightrole_right_rightId",
                        column: x => x.rightId,
                        principalSchema: "Diplom",
                        principalTable: "right",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rightrole_role_roleId",
                        column: x => x.roleId,
                        principalSchema: "Diplom",
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userrole",
                schema: "Diplom",
                columns: table => new
                {
                    userId = table.Column<int>(type: "integer", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userrole", x => new { x.userId, x.role });
                    table.ForeignKey(
                        name: "FK_userrole_role_role",
                        column: x => x.role,
                        principalSchema: "Diplom",
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userrole_user_userId",
                        column: x => x.userId,
                        principalSchema: "Diplom",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_right_code",
                schema: "Diplom",
                table: "right",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "IX_rightrole_roleId",
                schema: "Diplom",
                table: "rightrole",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "IX_userrole_role",
                schema: "Diplom",
                table: "userrole",
                column: "role");

            migrationBuilder.AddForeignKey(
                name: "FK_menuelement_right_rightId",
                schema: "Diplom",
                table: "menuelement",
                column: "rightId",
                principalSchema: "Diplom",
                principalTable: "right",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_objstatetransition_right_right",
                schema: "Diplom",
                table: "objstatetransition",
                column: "right",
                principalSchema: "Diplom",
                principalTable: "right",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

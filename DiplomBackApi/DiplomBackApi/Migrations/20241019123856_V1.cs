using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Litbase.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Diplom");

            migrationBuilder.CreateTable(
                name: "attributetype",
                schema: "Diplom",
                columns: table => new
                {
                    userId = table.Column<int>(type: "integer", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    is_complex = table.Column<bool>(type: "boolean", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    validation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attributetype", x => new { x.id, x.userId });
                });

            migrationBuilder.CreateTable(
                name: "objstate",
                schema: "Diplom",
                columns: table => new
                {
                    userId = table.Column<int>(type: "integer", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_objstate", x => new { x.id, x.userId });
                });

            migrationBuilder.CreateTable(
                name: "objtype",
                schema: "Diplom",
                columns: table => new
                {
                    userId = table.Column<int>(type: "integer", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_objtype", x => new { x.id, x.userId });
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "Diplom",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false),
                    icon = table.Column<string>(type: "text", nullable: false),
                    lastauth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    project = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

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

            migrationBuilder.CreateTable(
                name: "menuelement",
                schema: "Diplom",
                columns: table => new
                {
                    userId = table.Column<int>(type: "integer", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    parentMenuId = table.Column<int>(type: "integer", nullable: true),
                    objTypeId = table.Column<int>(type: "integer", nullable: true),
                    filters = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menuelement", x => new { x.id, x.userId });
                    table.ForeignKey(
                        name: "FK_menuelement_menuelement_parentMenuId_userId",
                        columns: x => new { x.parentMenuId, x.userId },
                        principalSchema: "Diplom",
                        principalTable: "menuelement",
                        principalColumns: new[] { "id", "userId" });
                    table.ForeignKey(
                        name: "FK_menuelement_objtype_objTypeId_userId",
                        columns: x => new { x.objTypeId, x.userId },
                        principalSchema: "Diplom",
                        principalTable: "objtype",
                        principalColumns: new[] { "id", "userId" });
                });

            migrationBuilder.CreateTable(
                name: "obj",
                schema: "Diplom",
                columns: table => new
                {
                    userId = table.Column<int>(type: "integer", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    stateId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_obj", x => new { x.id, x.userId });
                    table.ForeignKey(
                        name: "FK_obj_objstate_stateId_userId",
                        columns: x => new { x.stateId, x.userId },
                        principalSchema: "Diplom",
                        principalTable: "objstate",
                        principalColumns: new[] { "id", "userId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_obj_objtype_TypeId_userId",
                        columns: x => new { x.TypeId, x.userId },
                        principalSchema: "Diplom",
                        principalTable: "objtype",
                        principalColumns: new[] { "id", "userId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "objtypeattribute",
                schema: "Diplom",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    AttributeTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_objtypeattribute", x => new { x.id, x.userId });
                    table.ForeignKey(
                        name: "FK_objtypeattribute_attributetype_AttributeTypeId_userId",
                        columns: x => new { x.AttributeTypeId, x.userId },
                        principalSchema: "Diplom",
                        principalTable: "attributetype",
                        principalColumns: new[] { "id", "userId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_objtypeattribute_objtype_TypeId_userId",
                        columns: x => new { x.TypeId, x.userId },
                        principalSchema: "Diplom",
                        principalTable: "objtype",
                        principalColumns: new[] { "id", "userId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "favoriteobj",
                schema: "Diplom",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ObjId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favoriteobj", x => new { x.ObjId, x.UserId });
                    table.ForeignKey(
                        name: "FK_favoriteobj_obj_ObjId_UserId",
                        columns: x => new { x.ObjId, x.UserId },
                        principalSchema: "Diplom",
                        principalTable: "obj",
                        principalColumns: new[] { "id", "userId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_favoriteobj_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "Diplom",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "linkobj",
                schema: "Diplom",
                columns: table => new
                {
                    ObjParentId = table.Column<int>(type: "integer", nullable: false),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    ObjChildId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_linkobj", x => new { x.ObjParentId, x.userId });
                    table.ForeignKey(
                        name: "FK_linkobj_obj_ObjChildId_userId",
                        columns: x => new { x.ObjChildId, x.userId },
                        principalSchema: "Diplom",
                        principalTable: "obj",
                        principalColumns: new[] { "id", "userId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_linkobj_obj_ObjParentId_userId",
                        columns: x => new { x.ObjParentId, x.userId },
                        principalSchema: "Diplom",
                        principalTable: "obj",
                        principalColumns: new[] { "id", "userId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "objadditionalattribute",
                schema: "Diplom",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    ObjId = table.Column<int>(type: "integer", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    AttributeTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_objadditionalattribute", x => new { x.id, x.userId });
                    table.ForeignKey(
                        name: "FK_objadditionalattribute_attributetype_AttributeTypeId_userId",
                        columns: x => new { x.AttributeTypeId, x.userId },
                        principalSchema: "Diplom",
                        principalTable: "attributetype",
                        principalColumns: new[] { "id", "userId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_objadditionalattribute_obj_ObjId_userId",
                        columns: x => new { x.ObjId, x.userId },
                        principalSchema: "Diplom",
                        principalTable: "obj",
                        principalColumns: new[] { "id", "userId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "objattribute",
                schema: "Diplom",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    ObjId = table.Column<int>(type: "integer", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_objattribute", x => new { x.id, x.userId });
                    table.ForeignKey(
                        name: "FK_objattribute_obj_ObjId_userId",
                        columns: x => new { x.ObjId, x.userId },
                        principalSchema: "Diplom",
                        principalTable: "obj",
                        principalColumns: new[] { "id", "userId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_attributetype_userId",
                schema: "Diplom",
                table: "attributetype",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_favoriteobj_UserId",
                schema: "Diplom",
                table: "favoriteobj",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_linkobj_ObjChildId_userId",
                schema: "Diplom",
                table: "linkobj",
                columns: new[] { "ObjChildId", "userId" });

            migrationBuilder.CreateIndex(
                name: "IX_linkobj_userId",
                schema: "Diplom",
                table: "linkobj",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_menuelement_objTypeId_userId",
                schema: "Diplom",
                table: "menuelement",
                columns: new[] { "objTypeId", "userId" });

            migrationBuilder.CreateIndex(
                name: "IX_menuelement_parentMenuId_userId",
                schema: "Diplom",
                table: "menuelement",
                columns: new[] { "parentMenuId", "userId" });

            migrationBuilder.CreateIndex(
                name: "IX_menuelement_userId",
                schema: "Diplom",
                table: "menuelement",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_obj_TypeId_userId",
                schema: "Diplom",
                table: "obj",
                columns: new[] { "TypeId", "userId" });

            migrationBuilder.CreateIndex(
                name: "IX_obj_stateId_userId",
                schema: "Diplom",
                table: "obj",
                columns: new[] { "stateId", "userId" });

            migrationBuilder.CreateIndex(
                name: "IX_obj_userId",
                schema: "Diplom",
                table: "obj",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_objadditionalattribute_AttributeTypeId_userId",
                schema: "Diplom",
                table: "objadditionalattribute",
                columns: new[] { "AttributeTypeId", "userId" });

            migrationBuilder.CreateIndex(
                name: "IX_objadditionalattribute_ObjId_userId",
                schema: "Diplom",
                table: "objadditionalattribute",
                columns: new[] { "ObjId", "userId" });

            migrationBuilder.CreateIndex(
                name: "IX_objadditionalattribute_userId",
                schema: "Diplom",
                table: "objadditionalattribute",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_objattribute_ObjId_userId",
                schema: "Diplom",
                table: "objattribute",
                columns: new[] { "ObjId", "userId" });

            migrationBuilder.CreateIndex(
                name: "IX_objattribute_userId",
                schema: "Diplom",
                table: "objattribute",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_objstate_userId",
                schema: "Diplom",
                table: "objstate",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_objtype_userId",
                schema: "Diplom",
                table: "objtype",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_objtypeattribute_AttributeTypeId_userId",
                schema: "Diplom",
                table: "objtypeattribute",
                columns: new[] { "AttributeTypeId", "userId" });

            migrationBuilder.CreateIndex(
                name: "IX_objtypeattribute_TypeId_userId",
                schema: "Diplom",
                table: "objtypeattribute",
                columns: new[] { "TypeId", "userId" });

            migrationBuilder.CreateIndex(
                name: "IX_objtypeattribute_userId",
                schema: "Diplom",
                table: "objtypeattribute",
                column: "userId");

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
                name: "favoriteobj",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "linkobj",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "menuelement",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "objadditionalattribute",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "objattribute",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "objtypeattribute",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "userSettings",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "user",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "obj",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "attributetype",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "objstate",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "objtype",
                schema: "Diplom");
        }
    }
}

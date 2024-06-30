using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiplomBackApi.Migrations
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
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    is_complex = table.Column<bool>(type: "boolean", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    validation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attributetype", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "objstate",
                schema: "Diplom",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_objstate", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "objtype",
                schema: "Diplom",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_objtype", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "right",
                schema: "Diplom",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
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
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
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
                    lastauth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "obj",
                schema: "Diplom",
                columns: table => new
                {
                    stateId = table.Column<int>(type: "integer", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_obj", x => x.id);
                    table.ForeignKey(
                        name: "FK_obj_objstate_stateId",
                        column: x => x.stateId,
                        principalSchema: "Diplom",
                        principalTable: "objstate",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_obj_objtype_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "Diplom",
                        principalTable: "objtype",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "objtypeattribute",
                schema: "Diplom",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    AttributeTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_objtypeattribute", x => x.id);
                    table.ForeignKey(
                        name: "FK_objtypeattribute_attributetype_AttributeTypeId",
                        column: x => x.AttributeTypeId,
                        principalSchema: "Diplom",
                        principalTable: "attributetype",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_objtypeattribute_objtype_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "Diplom",
                        principalTable: "objtype",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "menuelement",
                schema: "Diplom",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    rightId = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    parentMenuId = table.Column<int>(type: "integer", nullable: true),
                    objTypeId = table.Column<int>(type: "integer", nullable: true),
                    filters = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menuelement", x => x.id);
                    table.ForeignKey(
                        name: "FK_menuelement_menuelement_parentMenuId",
                        column: x => x.parentMenuId,
                        principalSchema: "Diplom",
                        principalTable: "menuelement",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_menuelement_objtype_objTypeId",
                        column: x => x.objTypeId,
                        principalSchema: "Diplom",
                        principalTable: "objtype",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_menuelement_right_rightId",
                        column: x => x.rightId,
                        principalSchema: "Diplom",
                        principalTable: "right",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "objstatetransition",
                schema: "Diplom",
                columns: table => new
                {
                    stateFromId = table.Column<int>(type: "integer", nullable: false),
                    stateToId = table.Column<int>(type: "integer", nullable: false),
                    right = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_objstatetransition", x => new { x.stateFromId, x.stateToId });
                    table.ForeignKey(
                        name: "FK_objstatetransition_objstate_stateFromId",
                        column: x => x.stateFromId,
                        principalSchema: "Diplom",
                        principalTable: "objstate",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_objstatetransition_objstate_stateToId",
                        column: x => x.stateToId,
                        principalSchema: "Diplom",
                        principalTable: "objstate",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_objstatetransition_right_right",
                        column: x => x.right,
                        principalSchema: "Diplom",
                        principalTable: "right",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "favoriteobj",
                schema: "Diplom",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ObjId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favoriteobj", x => x.id);
                    table.ForeignKey(
                        name: "FK_favoriteobj_obj_ObjId",
                        column: x => x.ObjId,
                        principalSchema: "Diplom",
                        principalTable: "obj",
                        principalColumn: "id",
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
                name: "notification",
                schema: "Diplom",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    objId = table.Column<int>(type: "integer", nullable: false),
                    message = table.Column<string>(type: "text", nullable: false),
                    isRead = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification", x => x.id);
                    table.ForeignKey(
                        name: "FK_notification_obj_objId",
                        column: x => x.objId,
                        principalSchema: "Diplom",
                        principalTable: "obj",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notification_user_userId",
                        column: x => x.userId,
                        principalSchema: "Diplom",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "objadditionalattribute",
                schema: "Diplom",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ObjId = table.Column<int>(type: "integer", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    AttributeTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_objadditionalattribute", x => x.id);
                    table.ForeignKey(
                        name: "FK_objadditionalattribute_attributetype_AttributeTypeId",
                        column: x => x.AttributeTypeId,
                        principalSchema: "Diplom",
                        principalTable: "attributetype",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_objadditionalattribute_obj_ObjId",
                        column: x => x.ObjId,
                        principalSchema: "Diplom",
                        principalTable: "obj",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "objattribute",
                schema: "Diplom",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ObjId = table.Column<int>(type: "integer", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_objattribute", x => x.id);
                    table.ForeignKey(
                        name: "FK_objattribute_obj_ObjId",
                        column: x => x.ObjId,
                        principalSchema: "Diplom",
                        principalTable: "obj",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_favoriteobj_ObjId",
                schema: "Diplom",
                table: "favoriteobj",
                column: "ObjId");

            migrationBuilder.CreateIndex(
                name: "IX_favoriteobj_UserId",
                schema: "Diplom",
                table: "favoriteobj",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_menuelement_objTypeId",
                schema: "Diplom",
                table: "menuelement",
                column: "objTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_menuelement_parentMenuId",
                schema: "Diplom",
                table: "menuelement",
                column: "parentMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_menuelement_rightId",
                schema: "Diplom",
                table: "menuelement",
                column: "rightId");

            migrationBuilder.CreateIndex(
                name: "IX_notification_objId",
                schema: "Diplom",
                table: "notification",
                column: "objId");

            migrationBuilder.CreateIndex(
                name: "IX_notification_userId",
                schema: "Diplom",
                table: "notification",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_obj_TypeId",
                schema: "Diplom",
                table: "obj",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_obj_stateId",
                schema: "Diplom",
                table: "obj",
                column: "stateId");

            migrationBuilder.CreateIndex(
                name: "IX_objadditionalattribute_AttributeTypeId",
                schema: "Diplom",
                table: "objadditionalattribute",
                column: "AttributeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_objadditionalattribute_ObjId",
                schema: "Diplom",
                table: "objadditionalattribute",
                column: "ObjId");

            migrationBuilder.CreateIndex(
                name: "IX_objattribute_ObjId",
                schema: "Diplom",
                table: "objattribute",
                column: "ObjId");

            migrationBuilder.CreateIndex(
                name: "IX_objstatetransition_right",
                schema: "Diplom",
                table: "objstatetransition",
                column: "right");

            migrationBuilder.CreateIndex(
                name: "IX_objstatetransition_stateToId",
                schema: "Diplom",
                table: "objstatetransition",
                column: "stateToId");

            migrationBuilder.CreateIndex(
                name: "IX_objtypeattribute_AttributeTypeId",
                schema: "Diplom",
                table: "objtypeattribute",
                column: "AttributeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_objtypeattribute_TypeId",
                schema: "Diplom",
                table: "objtypeattribute",
                column: "TypeId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "favoriteobj",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "menuelement",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "notification",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "objadditionalattribute",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "objattribute",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "objstatetransition",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "objtypeattribute",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "rightrole",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "userrole",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "obj",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "attributetype",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "right",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "role",
                schema: "Diplom");

            migrationBuilder.DropTable(
                name: "user",
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

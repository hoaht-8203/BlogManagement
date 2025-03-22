using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace blog_server.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    refresh_token = table.Column<string>(type: "text", nullable: true),
                    refresh_token_expiry_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    create_by = table.Column<Guid>(type: "uuid", nullable: true),
                    update_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    join_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "FK_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 1, "Administrator role", "ADMIN" },
                    { 2, "User role", "USER" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "create_by", "create_date", "email", "password_hash", "refresh_token", "refresh_token_expiry_time", "status", "update_by", "update_date", "username" },
                values: new object[,]
                {
                    { new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"), null, new DateTime(2025, 3, 22, 18, 11, 52, 31, DateTimeKind.Utc).AddTicks(9710), "user@example.com", "0SaoEabSNpmHBSGCystG6o6Fk8NEomQ6MeZDDdFGvBjMugMa1w3YQ6nVKcM52n2K6/C8XnLJSAGib4Q7de3V9xOjAXzUOQsCvu0v25qlrg0=", null, null, 1, null, new DateTime(2025, 3, 22, 18, 11, 52, 31, DateTimeKind.Utc).AddTicks(9710), "user" },
                    { new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"), null, new DateTime(2025, 3, 22, 18, 11, 52, 31, DateTimeKind.Utc).AddTicks(9680), "admin@example.com", "xynG9F5ttn78RTmNvWlXGQxhwXOV2+6TrKlW111+ZSM8oUXDAT9XTubtljlzSUhGltHbecf3q6d/oi2T/QGsT/KeDQ0rN1Dmuf2entVnD7M=", null, null, 1, null, new DateTime(2025, 3, 22, 18, 11, 52, 31, DateTimeKind.Utc).AddTicks(9680), "admin" }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "role_id", "user_id", "join_date" },
                values: new object[,]
                {
                    { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"), new DateTime(2025, 3, 22, 18, 11, 52, 31, DateTimeKind.Utc).AddTicks(9730) },
                    { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"), new DateTime(2025, 3, 22, 18, 11, 52, 31, DateTimeKind.Utc).AddTicks(9730) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_role_id",
                table: "user_roles",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}

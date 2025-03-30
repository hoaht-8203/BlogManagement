using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace blog_server.Migrations
{
    /// <inheritdoc />
    public partial class FixTokenModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "password_resets");

            migrationBuilder.AddColumn<bool>(
                name: "is_email_verified",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "user_tokens",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: false),
                    token_type = table.Column<string>(type: "text", nullable: false),
                    token = table.Column<string>(type: "text", nullable: false),
                    expiry_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_used = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_tokens", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 30, 8, 1, 36, 757, DateTimeKind.Utc).AddTicks(1200), new DateTime(2025, 3, 30, 8, 1, 36, 757, DateTimeKind.Utc).AddTicks(1200) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 30, 8, 1, 36, 757, DateTimeKind.Utc).AddTicks(1200), new DateTime(2025, 3, 30, 8, 1, 36, 757, DateTimeKind.Utc).AddTicks(1200) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") },
                column: "join_date",
                value: new DateTime(2025, 3, 30, 8, 1, 36, 757, DateTimeKind.Utc).AddTicks(1200));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") },
                column: "join_date",
                value: new DateTime(2025, 3, 30, 8, 1, 36, 757, DateTimeKind.Utc).AddTicks(1200));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"),
                columns: new[] { "create_date", "is_email_verified", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 30, 8, 1, 36, 757, DateTimeKind.Utc).AddTicks(1200), false, "5cdutg5Ha7d4hykoY10530V3NjrfxQAipW/2kjn2KXmw3hgXhNaagJeBz64/pjeqV3hiFE3W756kutESw7DcnlnafZh9bY1/hlsIcfU8eiA=", new DateTime(2025, 3, 30, 8, 1, 36, 757, DateTimeKind.Utc).AddTicks(1200) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "create_date", "is_email_verified", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 30, 8, 1, 36, 757, DateTimeKind.Utc).AddTicks(1200), false, "AIbUZUfOvNhpIrkhKaAirCFNm9yPeVlhz+HICut1a69na5FNpbbqHmf5hQMi804v/zJrlNFxgvXm8MIKgzdpSl47ywbw+CCzOLb5W0ODRgU=", new DateTime(2025, 3, 30, 8, 1, 36, 757, DateTimeKind.Utc).AddTicks(1200) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_tokens");

            migrationBuilder.DropColumn(
                name: "is_email_verified",
                table: "users");

            migrationBuilder.CreateTable(
                name: "password_resets",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    expiry_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_used = table.Column<bool>(type: "boolean", nullable: false),
                    token = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_password_resets", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 29, 13, 53, 30, 510, DateTimeKind.Utc).AddTicks(1980), new DateTime(2025, 3, 29, 13, 53, 30, 510, DateTimeKind.Utc).AddTicks(1980) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 29, 13, 53, 30, 510, DateTimeKind.Utc).AddTicks(1980), new DateTime(2025, 3, 29, 13, 53, 30, 510, DateTimeKind.Utc).AddTicks(1980) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") },
                column: "join_date",
                value: new DateTime(2025, 3, 29, 13, 53, 30, 510, DateTimeKind.Utc).AddTicks(1980));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") },
                column: "join_date",
                value: new DateTime(2025, 3, 29, 13, 53, 30, 510, DateTimeKind.Utc).AddTicks(1980));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 29, 13, 53, 30, 510, DateTimeKind.Utc).AddTicks(1980), "32F79tEHmagp3VFMpJsGIr9o4kJuIxk3S7Yc2RfxWz4iObPYg15VXzShTzO5e61zisMgtEqUhGRtSW6yHyre+zFeknGHe5HALrMWZ0wIwj0=", new DateTime(2025, 3, 29, 13, 53, 30, 510, DateTimeKind.Utc).AddTicks(1980) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 29, 13, 53, 30, 510, DateTimeKind.Utc).AddTicks(1980), "y8H44K0t4xESt3LQx5fZWgJgoxq4tNxUnDGNYRFzLwlFtVgqhyZEiPInMa1c/GdvsUJbh8+a6CvdziWkZ1mkwHca7hqBChLMHHL8LBAagH4=", new DateTime(2025, 3, 29, 13, 53, 30, 510, DateTimeKind.Utc).AddTicks(1980) });
        }
    }
}

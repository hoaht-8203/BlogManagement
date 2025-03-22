using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog_server.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "create_by",
                table: "roles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "create_date",
                table: "roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "update_by",
                table: "roles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "update_date",
                table: "roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_by", "create_date", "update_by", "update_date" },
                values: new object[] { null, new DateTime(2025, 3, 22, 18, 14, 10, 219, DateTimeKind.Utc).AddTicks(1670), null, new DateTime(2025, 3, 22, 18, 14, 10, 219, DateTimeKind.Utc).AddTicks(1670) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_by", "create_date", "update_by", "update_date" },
                values: new object[] { null, new DateTime(2025, 3, 22, 18, 14, 10, 219, DateTimeKind.Utc).AddTicks(1670), null, new DateTime(2025, 3, 22, 18, 14, 10, 219, DateTimeKind.Utc).AddTicks(1670) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") },
                column: "join_date",
                value: new DateTime(2025, 3, 22, 18, 14, 10, 219, DateTimeKind.Utc).AddTicks(2180));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") },
                column: "join_date",
                value: new DateTime(2025, 3, 22, 18, 14, 10, 219, DateTimeKind.Utc).AddTicks(2180));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 18, 14, 10, 219, DateTimeKind.Utc).AddTicks(2160), "Bwh9DhUbzVcvjM7/ECw/LyrOItE2F4hbQ0Ond6WJAbwrWqKzUPHhRLLMw4EJxhS7W15gAIXdLgYbk2tac2zEiE3oBbcujK1+LaC/tEjftGs=", new DateTime(2025, 3, 22, 18, 14, 10, 219, DateTimeKind.Utc).AddTicks(2160) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 18, 14, 10, 219, DateTimeKind.Utc).AddTicks(2130), "6tBqc/UePRvaz9W2ORvEZfkQjIBc3JHrQ/Mxj/EGkO293a178/dTWvE01iswTYnrFGGAePGGJs2wxTrS40xK564iZUR5qpcekHwKDsZ4w08=", new DateTime(2025, 3, 22, 18, 14, 10, 219, DateTimeKind.Utc).AddTicks(2130) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "create_by",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "create_date",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "update_by",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "update_date",
                table: "roles");

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") },
                column: "join_date",
                value: new DateTime(2025, 3, 22, 18, 11, 52, 31, DateTimeKind.Utc).AddTicks(9730));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") },
                column: "join_date",
                value: new DateTime(2025, 3, 22, 18, 11, 52, 31, DateTimeKind.Utc).AddTicks(9730));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 18, 11, 52, 31, DateTimeKind.Utc).AddTicks(9710), "0SaoEabSNpmHBSGCystG6o6Fk8NEomQ6MeZDDdFGvBjMugMa1w3YQ6nVKcM52n2K6/C8XnLJSAGib4Q7de3V9xOjAXzUOQsCvu0v25qlrg0=", new DateTime(2025, 3, 22, 18, 11, 52, 31, DateTimeKind.Utc).AddTicks(9710) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 18, 11, 52, 31, DateTimeKind.Utc).AddTicks(9680), "xynG9F5ttn78RTmNvWlXGQxhwXOV2+6TrKlW111+ZSM8oUXDAT9XTubtljlzSUhGltHbecf3q6d/oi2T/QGsT/KeDQ0rN1Dmuf2entVnD7M=", new DateTime(2025, 3, 22, 18, 11, 52, 31, DateTimeKind.Utc).AddTicks(9680) });
        }
    }
}
